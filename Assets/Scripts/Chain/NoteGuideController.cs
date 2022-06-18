using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGuideController : MonoBehaviour
{
    public PlayableNote PlayableNote { get { return _playableNote; } }
    public bool FadingOut { get { return _fadingOut; } }
    public bool DisabledAfterSpawn { get { return _disabledAfterSpawn; } }

    private Vector3 StartPosition;
    private Vector3 DestinationPosition;

    private float MusicStartTime;
    private float TravelTime;
    private float EndTime;

    private float DisabledAfterSpawnDuration;
    private float FadeOutDuration;
    private Vector3 InitialScale;

    private PlayableNote _playableNote;
    
    private bool Moving = false;

    private bool _fadingOut = false;

    private bool _disabledAfterSpawn = false;

    public void StartMoving(PlayableNote PlayableNote, Vector3 StartPosition, Vector3 DestinationPosition, float MusicStartTime, float TravelTime, float FadeOutDuration, float DisabledAfterSpawnDuration)
    {
        this.StartPosition = StartPosition;
        this.DestinationPosition = DestinationPosition;

        this.MusicStartTime = MusicStartTime;
        this.TravelTime = TravelTime;
        
        _playableNote = PlayableNote;
        SetColor(MusicNoteHelper.GetMusicNoteColor(PlayableNote.ExpectedNote));
        this.EndTime = PlayableNote.OnTime + MusicStartTime;

        this.FadeOutDuration = FadeOutDuration;
        this.DisabledAfterSpawnDuration = DisabledAfterSpawnDuration;
        this.InitialScale = transform.localScale;

        this.Moving = true;
    }

    public void SetColor(Color Color)
    {
        GetComponent<SpriteRenderer>().color = Color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
            if (DisabledAfterSpawn && Time.time - (EndTime - TravelTime) >= DisabledAfterSpawnDuration)
            {
                _disabledAfterSpawn = false;
            }

            float TimeLeft = EndTime - Time.time;

            if (TimeLeft <= 0) enabled = false;

            transform.position = Vector3.Lerp(DestinationPosition, StartPosition, TimeLeft / TravelTime);
            
            if (TimeLeft <= FadeOutDuration)
            {
                _fadingOut = true;
                float sizeMultiplicator = Mathf.Lerp(0f, 1f, TimeLeft / FadeOutDuration);
                transform.transform.localScale = InitialScale * sizeMultiplicator;
            }
        }
    }
}
