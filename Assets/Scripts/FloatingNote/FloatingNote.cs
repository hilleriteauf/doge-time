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
    private float ShootAnimationSpeed = 25f;
    private float ShootAnimationDuration;
    private Vector3 InitialScale;

    private float OvershootDistance = 1f;
    private float OvershootAnimationDuration = 0.3f;

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
        ShootAnimationDuration = Vector2.Distance(StartPosition, NoteGuide.transform.position) / ShootAnimationSpeed;
        InitialScale = transform.localScale;
    }

    private void Update()
    {
        if (this.NoteGuide != null)
        {
            float LivedTime = Time.time - PlacedTime;
            Vector3 NewPosition;
            if (LivedTime > ShootAnimationDuration + OvershootAnimationDuration)
            {
                NewPosition = NoteGuide.transform.position;
            }
            else if (LivedTime > ShootAnimationDuration)
            {
                float AnimationProgression = (LivedTime - ShootAnimationDuration) / OvershootAnimationDuration;
                float EasedProgression = Mathf.Sqrt(1 - Mathf.Pow(2f * AnimationProgression - 1f, 2));
                Debug.Log($"Animation Progress: {AnimationProgression}, PositionProgression: {EasedProgression}");
                NewPosition = Vector3.Lerp(NoteGuide.transform.position, NoteGuide.transform.position + Vector3.down * OvershootDistance, EasedProgression);
            }
            else
            {
                NewPosition = Vector3.Lerp(StartPosition, NoteGuide.transform.position, LivedTime / ShootAnimationDuration) + Vector3.back;
            }
            NewPosition.z = -1f;
            transform.position = NewPosition;
            transform.localScale = InitialScale * NoteGuide.GetComponent<NoteGuideController>().CurrentSize;
        }
    }
}
