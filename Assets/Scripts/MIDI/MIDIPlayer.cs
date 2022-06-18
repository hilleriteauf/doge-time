using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSharpSynth.Effects;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;
using Assets.Scripts.MIDI;

[RequireComponent(typeof(AudioSource))]
public class MIDIPlayer : MonoBehaviour
{
    public float Gain = 1f;
    
    //Try also: "FM Bank/fm" or "Analog Bank/analog" for some different sounds
    private string BankFilePath = "GM Bank/gm";

    private int BufferSize = 1024;
    private float[] SampleBuffer;
    private MidiSequencer MidiSequencer;
    private StreamSynthesizer MidiStreamSynthesizer;

    private float _startTime = -1;
    public float StartTime { get { return _startTime; } }

    public PlayableNote[] PlayableNotes { get { return MidiSequencer.PlayableNotes; } }

    public void LoadSong(string midiFileName, float tempoMultiplier)
    {
        MidiStreamSynthesizer = new StreamSynthesizer(44100, 2, BufferSize, 40);
        SampleBuffer = new float[MidiStreamSynthesizer.BufferSize];

        MidiStreamSynthesizer.LoadBank(BankFilePath);
        MidiSequencer = new MidiSequencer(MidiStreamSynthesizer, tempoMultiplier);

        MidiSequencer.LoadMidi("Midis/" + midiFileName, false);
    }

    public void PlaySong()
    {
        MidiSequencer.Play();
        _startTime = Time.time;
    }


    // See http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.OnAudioFilterRead.html for reference code
    //	If OnAudioFilterRead is implemented, Unity will insert a custom filter into the audio DSP chain.
    //
    //	The filter is inserted in the same order as the MonoBehaviour script is shown in the inspector. 	
    //	OnAudioFilterRead is called everytime a chunk of audio is routed thru the filter (this happens frequently, every ~20ms depending on the samplerate and platform). 
    //	The audio data is an array of floats ranging from [-1.0f;1.0f] and contains audio from the previous filter in the chain or the AudioClip on the AudioSource. 
    //	If this is the first filter in the chain and a clip isn't attached to the audio source this filter will be 'played'. 
    //	That way you can use the filter as the audio clip, procedurally generating audio.
    //
    //	If OnAudioFilterRead is implemented a VU meter will show up in the inspector showing the outgoing samples level. 
    //	The process time of the filter is also measured and the spent milliseconds will show up next to the VU Meter 
    //	(it turns red if the filter is taking up too much time, so the mixer will starv audio data). 
    //	Also note, that OnAudioFilterRead is called on a different thread from the main thread (namely the audio thread) 
    //	so calling into many Unity functions from this function is not allowed ( a warning will show up ). 	
    private void OnAudioFilterRead(float[] data, int channels)
    {
        //This uses the Unity specific float method we added to get the buffer
        MidiStreamSynthesizer.GetNext(SampleBuffer);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = SampleBuffer[i] * Gain;
        }
    }
}
