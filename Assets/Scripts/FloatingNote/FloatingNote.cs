using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNote : MonoBehaviour
{

    [SerializeField]
    private MusicNote Note;

    [SerializeField]
    private Color NoteColor;
    private float Speed;

    // Modèle de note à suivre une fois placé
    private GameObject NoteGuide = null;
    private Vector3 StartPosition;
    private float PlacedTime;
    private float AnimationSpeed = 30f;
    private float AnimationDuration;
    private Vector3 InitialScale;

    public void setNote(MusicNote _Note)
    {
        this.Note = _Note;
    }
    public MusicNote getNote()
    {
        return this.Note;
    }
    public void setNoteColor(Color _NoteColor)
    {
        this.NoteColor = _NoteColor;
        this.GetComponent<SpriteRenderer>().color = this.NoteColor;
    }
    public Color getNoteColor()
    {
        return this.NoteColor;
    }

    public void setSpeed(float _Speed)
    {
        this.Speed = _Speed;
    }
    public float getSpeed()
    {
        return this.Speed;
    }

    public void Place(GameObject NoteGuide)
    {
        this.NoteGuide = NoteGuide;
        StartPosition = transform.position;
        PlacedTime = Time.time;
        AnimationDuration = Vector2.Distance(StartPosition, NoteGuide.transform.position) / AnimationSpeed;
        InitialScale = transform.localScale;
    }

    private void Update()
    {
        if (this.NoteGuide != null)
        {
            transform.position = Vector3.Lerp(StartPosition, NoteGuide.transform.position, (Time.time - PlacedTime) / AnimationDuration) + Vector3.back;
            transform.localScale = InitialScale * NoteGuide.GetComponent<NoteGuideController>().CurrentSize;
        }
    }
}
