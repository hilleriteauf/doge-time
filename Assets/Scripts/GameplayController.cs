using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{

    public MIDIPlayer MIDIPlayer;
    public string MidiFileName = "Debut-Jerk-It-Out.mid";
    public float TempoMultiplier = 1f;
    public float TimeBeforeStart = 0.25f;

    public ChainManager ChainManager;
    public RandomGeneration FloatingBallManager;
    public BonkController BonkController;
    public bool CheatMode = false;

    public int ScoreByGoodPlacing = 10;
    public int ComboDivider = 100;

    public ScoreController ScoreController;
    public ComboController ComboController;

    public Transform MusicNoteSpawnPoint;
    public GameObject MusicNotePrefab;

    public EndScreenController EndScreenController;

    private PlayableNote[] PlayableNotes;
    private int PlayableNotesIndex = 0;

    private float MusicStartTime;

    private int Combo = 0;
    private int Score = 0;

    private int CorrectNoteCounter = 0;
    private int IncorrectNoteCounter = 0;
    private int MissedNoteCounter = 0;
    private int MaximumCombo = 0;

    private const int choixSceneMenu = 0;

    // Start is called before the first frame update
    void Start()
    {

        EndScreenController.Hide();

        MIDIPlayer.Gain = Sound.GetSound();
        if (EnvoiNiveau.GetNiveau() != null) MidiFileName = EnvoiNiveau.GetNiveau();
        TempoMultiplier = Vitesse.GetVitesse();
        Debug.Log("musique : " + Sound.GetSound());
        Debug.Log("niveau : " + EnvoiNiveau.GetNiveau());
        Debug.Log("vitesse : " + Vitesse.GetVitesse());

        MIDIPlayer.LoadSong(MidiFileName, TempoMultiplier);
        PlayableNotes = MIDIPlayer.PlayableNotes;

        Debug.Log($"Playable note length : {PlayableNotes.Length}");

        ChainManager.Initialize();
        float NoteGuideTravelTime = ChainManager.TravelTime;
        Debug.Log($"Starting music in {NoteGuideTravelTime} seconds");
        StartCoroutine(PlayMusicAfterDelay(NoteGuideTravelTime + TimeBeforeStart - 0.05f));

        MusicStartTime = Time.time + NoteGuideTravelTime + TimeBeforeStart;

        ChainManager.StartChainGeneration(PlayableNotes, MusicStartTime);
    }

    IEnumerator PlayMusicAfterDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        MIDIPlayer.PlaySong();
    }

    // Update is called once per frame
    void Update()
    {

        if (CheatMode)
        {
            GameObject Ball = ChainManager.getNextEmptyNoteGuide();
            if (Ball != null && Ball.transform.position.x < 0)
            {
                PlaceNote((MusicNote)(((int)Ball.GetComponent<NoteGuideController>().PlayableNote.ExpectedNote / 10) * 10));
            }
        }

        HandlePlayedNotesAnimations();

        HandleInputs();
    }

    void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            PlaceNote(MusicNote.Do);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
            PlaceNote(MusicNote.Re);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F");
            PlaceNote(MusicNote.Mi);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            PlaceNote(MusicNote.Fa);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("J");
            PlaceNote(MusicNote.Sol);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K");
            PlaceNote(MusicNote.La);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L");
            PlaceNote(MusicNote.Si);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("retour menu");
            SceneManager.LoadScene(choixSceneMenu);
        }
    }

    void HandlePlayedNotesAnimations()
    {
        if (PlayableNotesIndex < PlayableNotes.Length && Time.time >= PlayableNotes[PlayableNotesIndex].OnTime + MusicStartTime)
        {
            if (PlayableNotes[PlayableNotesIndex].PlacedNote == MusicNote.Null)
            {
                UpdateScore(false, false);
            }
            else {
                ComboController.MakePublicDanse();

                bool wellPlaced = ((int)PlayableNotes[PlayableNotesIndex].PlacedNote / 10) == ((int)PlayableNotes[PlayableNotesIndex].ExpectedNote / 10);

                UpdateScore(true, wellPlaced);

                GameObject newMusicNote = Instantiate(MusicNotePrefab, MusicNoteSpawnPoint.position, Quaternion.identity, transform);
                newMusicNote.GetComponent<MusicNoteController>().StartAnimation(PlayableNotes[PlayableNotesIndex].PlacedNote, wellPlaced);
            }

            PlayableNotesIndex++;

            if (PlayableNotesIndex == PlayableNotes.Length)
            {
                EndScreenController.Display(CorrectNoteCounter, IncorrectNoteCounter, MissedNoteCounter, MaximumCombo, TempoMultiplier, Score);
                ScoreController.Hide();
            }
        }
    }

    void PlaceNote(MusicNote MusicNote)
    {

        GameObject EmptyNoteGuide = ChainManager.getNextEmptyNoteGuide();
        
        if (EmptyNoteGuide == null)
        {
            return;
        }

        EmptyNoteGuide.GetComponent<NoteGuideController>().PlaceNote(MusicNote);

        GameObject Ball = FloatingBallManager.GetBallToPlace(MusicNote, EmptyNoteGuide.transform.position);
        if (Ball != null)
        {
            Ball.GetComponent<FloatingNote>().Place(EmptyNoteGuide);
            EmptyNoteGuide.GetComponent<NoteGuideController>().PlacedNote = Ball;
            BonkController.Bonk(Ball.transform.position, EmptyNoteGuide.transform.position);

            NoteGuideController PreviousNoteGuide = ChainManager.GetPreviousNoteGuide(EmptyNoteGuide.GetComponent<NoteGuideController>());
            GameObject PreviousBall = PreviousNoteGuide == null ? null : PreviousNoteGuide.PlacedNote;
            
            if (PreviousBall != null)
            {
                GameObject NewChainObject = Instantiate(ChainManager.ChainObjectPrefab, Ball.transform.position, Quaternion.identity, ChainManager.ChainObject.transform);
                NewChainObject.GetComponent<ChainObjectController>().StartAnimation(Ball, PreviousBall);
            }

        }
        else
        {
            Debug.LogWarning($"Not enouth ball for note {MusicNote} !");
        }
    }

    void UpdateScore(bool placed, bool WellPlaced)
    {


        if (!placed)
        {
            Combo = 0;
            MissedNoteCounter++;
        }
        else if (WellPlaced)
        {
            Score += (int)(ScoreByGoodPlacing * (1 + (float)Combo / (float)ComboDivider));
            Combo++;
            CorrectNoteCounter++;

            if (Combo >= MaximumCombo)
            {
                MaximumCombo = Combo;
            }
        }
        else
        {
            Combo = 0;
            IncorrectNoteCounter++;
        }

        ScoreController.SetScore(Score);
        ComboController.SetCombo(Combo);
    }
}
