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

    public void StartMoving(PlayableNote PlayableNote, ChainManager ChainManager)
    {
        this.StartPosition = ChainManager.StartPosition;
        this.DestinationPosition = ChainManager.DestinationPosition;

        this.MusicStartTime = ChainManager.MusicStartTime;
        this.TravelTime = ChainManager.TravelTime;
        
        _playableNote = PlayableNote;
        SetColor(MusicNoteHelper.GetMusicNoteColor(PlayableNote.ExpectedNote));
        SetLetterSprite(ChainManager.GetSpriteFromMusicNote(PlayableNote.ExpectedNote));
        this.EndTime = PlayableNote.OnTime + MusicStartTime;

        this.FadeOutDuration = ChainManager.FadeOutDuration;
        this.DisabledAfterSpawnDuration = ChainManager.DisabledAfterSpawnDuration;
        this.InitialScale = transform.localScale;

        this.Moving = true;
    }

    public void SetColor(Color Color)
    {
        GetComponent<SpriteRenderer>().color = Color;
    }

    public void SetLetterSprite(Sprite Sprite)
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Sprite;
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
                //transform.transform.localScale = InitialScale * sizeMultiplicator;
            }
        }
    }
}
