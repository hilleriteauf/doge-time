using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : MonoBehaviour
{

    public GameObject Portal;
    public GameObject SpawnPoint;

    public float ChainSpeed = 1f;

    public GameObject NoteGuidePrefab;
    public GameObject ChainObjectPrefab;

    public float PortalLeftMargin = 1f;

    public float TravelTime { get { return _travelTime; } }

    public Vector3 StartPosition { get { return SpawnPoint.transform.position; } }
    public Vector3 DestinationPosition { get { return Portal.transform.position; } }

    public float FadeOutDuration { get { return _fadeOutDuration; } }

    public float MusicStartTime { get { return _musicStartTime; } }

    public float DisabledAfterSpawnDuration { get { return _disabledAfterSpawnDuration; } }

    public GameObject ChainObject { get { return _chainObject; } }

    public Sprite SSprite;
    public Sprite DSprite;
    public Sprite FSprite;
    public Sprite SpaceSprite;
    public Sprite JSprite;
    public Sprite KSprite;
    public Sprite LSprite;


    private float Distance;

    private float _travelTime;
    
    private float _fadeOutDuration;
    private float _disabledAfterSpawnDuration;

    private PlayableNote[] PlayableNotes;
    private int PlayableNoteIndex;

    private float _musicStartTime;
    private bool started = false;

    private List<NoteGuideController> NoteGuides = new List<NoteGuideController>();

    private GameObject _chainObject;

    public void Initialize()
    {
        float GuideSpriteWidth = NoteGuidePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float PortalSpriteWidth = Portal.GetComponent<SpriteRenderer>().bounds.size.x;

        _fadeOutDuration = (PortalSpriteWidth * 0.5f) / ChainSpeed;

        float RightScreenEdgeWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float LeftScreenEdgeWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;

        Portal.transform.position = new Vector3(LeftScreenEdgeWorldPos + PortalSpriteWidth * 0.5f + PortalLeftMargin, Portal.transform.position.y, Portal.transform.position.z);

        SpawnPoint.transform.position = new Vector3(RightScreenEdgeWorldPos + GuideSpriteWidth * 0.5f, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z);
        _disabledAfterSpawnDuration = GuideSpriteWidth * 0.75f / ChainSpeed;

        _chainObject = new GameObject("Chain");
        _chainObject.transform.SetParent(transform);

        Distance = Vector2.Distance(Portal.transform.position, SpawnPoint.transform.position);
        _travelTime = Distance / ChainSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (started && PlayableNoteIndex < PlayableNotes.Length && Time.time >= PlayableNotes[PlayableNoteIndex].OnTime + _musicStartTime - TravelTime)
        {
            InstantiateNoteGuide(PlayableNotes[PlayableNoteIndex]);
            PlayableNoteIndex++;
        }
    }

    private void InstantiateNoteGuide(PlayableNote PlayableNote)
    {
        GameObject NewNoteGuide = Instantiate(NoteGuidePrefab, SpawnPoint.transform.position, Quaternion.identity, _chainObject.transform);
        NoteGuideController NoteGuideController = NewNoteGuide.GetComponent<NoteGuideController>();
        NoteGuideController.SetColor(MusicNoteHelper.GetMusicNoteColor(PlayableNote.ExpectedNote));
        NoteGuideController.StartMoving(PlayableNote, this);
        NoteGuides.Add(NoteGuideController);
    }

    public void StartChainGeneration(PlayableNote[] PlayableNotes, float MusicStartTime)
    {
        this.PlayableNotes = PlayableNotes;
        this._musicStartTime = MusicStartTime;
        started = true;
    }

    public GameObject getNextEmptyNoteGuide()
    {
        for (int i = 0; i < NoteGuides.Count; i++)
        {
            if (NoteGuides[i].FadingOut == false && NoteGuides[i].PlayableNote.PlacedNote == MusicNote.Null && !NoteGuides[i].DisabledAfterSpawn)
            {
                return NoteGuides[i].gameObject;
            }
        }

        return null;
    }

    public Sprite GetSpriteFromMusicNote(MusicNote MusicNote)
    {
        switch ((int)MusicNote / 10)
        {
            case 0:
                return SSprite;
            case 1:
                return DSprite;
            case 2:
                return FSprite;
            case 3:
                return SpaceSprite;
            case 4:
                return JSprite;
            case 5:
                return KSprite;
            case 6:
                return LSprite;
            default:
                return null;
        }
    }

    public NoteGuideController GetPreviousNoteGuide(NoteGuideController noteGuideController)
    {
        int index = NoteGuides.IndexOf(noteGuideController);
        if (index == 0)
        {
            return null;
        }
        else return NoteGuides[index - 1];
    }
}
