using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{

    public MIDIPlayer MIDIPlayer;
    public string MidiFileName = "Debut-Jerk-It-Out.mid";
    public float TempoMultiplier = 1f;

    private PlayableNote[] PlayableNotes;
    private int PlayableNotesIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        MIDIPlayer.LoadSong(MidiFileName, TempoMultiplier);
        PlayableNotes = MIDIPlayer.PlayableNotes;

        Debug.Log($"Playable note length : {PlayableNotes.Length}");

        // Place les notes attendues
        for (int i = 0; i < PlayableNotes.Length; i++)
        {
            PlayableNote playableNote = PlayableNotes[i];
            //Debug.Log($"Note n�{i}, expected note: {playableNote.ExpectedNote}, octave: {playableNote.Octave}");
            playableNote.PlacedNote = playableNote.ExpectedNote;
        }

        MIDIPlayer.PlaySong();
    }

    // Update is called once per frame
    void Update()
    {
        // Affiche les notes dans la console lorsqu'elles sont jou�es
        while (MIDIPlayer.StartTime != -1 && PlayableNotesIndex < PlayableNotes.Length && PlayableNotes[PlayableNotesIndex].OnTime <= Time.time - MIDIPlayer.StartTime)
        {
            Debug.Log($"Playable Note {PlayableNotes[PlayableNotesIndex].PlacedNote} played at {PlayableNotes[PlayableNotesIndex].OnTime}");
            PlayableNotesIndex++;
        }
    }
}
