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

    public float PortalLeftMargin = 1f;

    public float TravelTime { get { return _travelTime; } }


    private float Distance;

    private float _travelTime;
    
    private float FadeOutDuration;
    private float DisabledAfterSpawnDuration;

    private PlayableNote[] PlayableNotes;
    private int PlayableNoteIndex;
    private float MusicStartTime;
    private bool started = false;

    private List<NoteGuideController> NoteGuides = new List<NoteGuideController>();

    private GameObject ChainObject;

    public void Initialize()
    {
        float GuideSpriteWidth = NoteGuidePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float PortalSpriteWidth = Portal.GetComponent<SpriteRenderer>().bounds.size.x;

        FadeOutDuration = (PortalSpriteWidth * 0.5f) / ChainSpeed;

        float RightScreenEdgeWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float LeftScreenEdgeWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;

        Portal.transform.position = new Vector3(LeftScreenEdgeWorldPos + PortalSpriteWidth * 0.5f + PortalLeftMargin, Portal.transform.position.y, Portal.transform.position.z);

        SpawnPoint.transform.position = new Vector3(RightScreenEdgeWorldPos + GuideSpriteWidth * 0.5f, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z);
        DisabledAfterSpawnDuration = GuideSpriteWidth * 0.75f / ChainSpeed;

        ChainObject = new GameObject("Chain");
        ChainObject.transform.SetParent(transform);

        Distance = Vector2.Distance(Portal.transform.position, SpawnPoint.transform.position);
        _travelTime = Distance / ChainSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (started && PlayableNoteIndex < PlayableNotes.Length && Time.time >= PlayableNotes[PlayableNoteIndex].OnTime + MusicStartTime - TravelTime)
        {
            InstantiateNoteGuide(PlayableNotes[PlayableNoteIndex]);
            PlayableNoteIndex++;
        }
    }

    private void InstantiateNoteGuide(PlayableNote PlayableNote)
    {
        GameObject NewNoteGuide = Instantiate(NoteGuidePrefab, SpawnPoint.transform.position, Quaternion.identity, ChainObject.transform);
        NoteGuideController NoteGuideController = NewNoteGuide.GetComponent<NoteGuideController>();
        NoteGuideController.SetColor(MusicNoteHelper.GetMusicNoteColor(PlayableNote.ExpectedNote));
        NoteGuideController.StartMoving(PlayableNote, SpawnPoint.transform.position, Portal.transform.position, MusicStartTime, TravelTime, FadeOutDuration, DisabledAfterSpawnDuration);
        NoteGuides.Add(NoteGuideController);
    }

    public void StartChainGeneration(PlayableNote[] PlayableNotes, float MusicStartTime)
    {
        this.PlayableNotes = PlayableNotes;
        this.MusicStartTime = MusicStartTime;
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
}
