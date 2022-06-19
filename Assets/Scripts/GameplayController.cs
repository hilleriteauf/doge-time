using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private PlayableNote[] PlayableNotes;
    private int PlayableNotesIndex;

    private float MusicStartTime;

    private int Combo = 0;
    private int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void HandlePlayedNotesAnimations()
    {
        if (PlayableNotesIndex < PlayableNotes.Length && Time.time >= PlayableNotes[PlayableNotesIndex].OnTime + MusicStartTime)
        {
            ComboController.MakePublicDanse();
            PlayableNotesIndex++;
        }
    }

    void PlaceNote(MusicNote MusicNote)
    {

        GameObject EmptyNoteGuide = ChainManager.getNextEmptyNoteGuide();
        
        if (EmptyNoteGuide == null)
        {
            return;
        }

        UpdateScore(EmptyNoteGuide.GetComponent<NoteGuideController>().PlaceNote(MusicNote));

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

    void UpdateScore(bool WellPlaced)
    {
        Debug.Log(WellPlaced);

        Score += (int)(ScoreByGoodPlacing * (1 + (float)Combo / (float)ComboDivider));

        if (WellPlaced)
        {
            Combo++;
        }
        else
        {
            Combo = 0;
        }

        ScoreController.SetScore(Score);
        ComboController.SetCombo(Combo);
    }
}
