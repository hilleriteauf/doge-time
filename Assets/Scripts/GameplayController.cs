using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{

    public MIDIPlayer MIDIPlayer;
    public string MidiFileName = "Debut-Jerk-It-Out.mid";
    public float TempoMultiplier = 1f;

    public ChainManager ChainManager;
    public RandomGeneration FloatingBallManager;
    public BonkController BonkController;
    public bool CheatMode = false;

    private PlayableNote[] PlayableNotes;

    private float MusicStartTime;

    // Start is called before the first frame update
    void Start()
    {
        MIDIPlayer.LoadSong(MidiFileName, TempoMultiplier);
        PlayableNotes = MIDIPlayer.PlayableNotes;

        Debug.Log($"Playable note length : {PlayableNotes.Length}");

        ChainManager.Initialize();
        float NoteGuideTravelTime = ChainManager.TravelTime;
        Debug.Log($"Starting music in {NoteGuideTravelTime} seconds");
        StartCoroutine(PlayMusicAfterDelay(NoteGuideTravelTime - 0.05f));

        MusicStartTime = Time.time + NoteGuideTravelTime;

        ChainManager.StartChainGeneration(PlayableNotes, NoteGuideTravelTime);
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
            if (Ball != null)
            {
                PlaceNote((MusicNote)(((int)Ball.GetComponent<NoteGuideController>().PlayableNote.ExpectedNote / 10) * 10));
            }
        }

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
            BonkController.Bonk(Ball.transform.position, EmptyNoteGuide.transform.position);
        }
        else
        {
            Debug.LogWarning($"Not enouth ball for note {MusicNote} !");
        }
    }
}
