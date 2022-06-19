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

    // Mod�le de note � suivre une fois plac�
    private GameObject NoteGuide = null;
    private Vector3 StartPosition;
    private float PlacedTime = -1f;
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
        if (PlacedTime != -1f)
        {
            if (NoteGuide == null)
            {
                Destroy(gameObject);
                return;
            }

            float LivedTime = Time.time - PlacedTime;
            Vector3 NewPosition;
            if (LivedTime > ShootAnimationDuration + OvershootAnimationDuration)
            {
                NewPosition = NoteGuide.transform.position;
                transform.localScale = Vector3.one * 0.5f * NoteGuide.GetComponent<NoteGuideController>().CurrentSize;
            }
            else if (LivedTime > ShootAnimationDuration)
            {
                float AnimationProgression = (LivedTime - ShootAnimationDuration) / OvershootAnimationDuration;
                float EasedProgression = Mathf.Sqrt(1 - Mathf.Pow(2f * AnimationProgression - 1f, 2));
                NewPosition = Vector3.Lerp(NoteGuide.transform.position, NoteGuide.transform.position + Vector3.down * OvershootDistance, EasedProgression);
            }
            else
            {
                transform.localScale = Vector3.Lerp(new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.5f, 0.5f, 0.5f), LivedTime / ShootAnimationDuration);
                NewPosition = Vector3.Lerp(StartPosition, NoteGuide.transform.position, LivedTime / ShootAnimationDuration) + Vector3.back;
            }
            NewPosition.z = -1f;
            transform.position = NewPosition;
        }
    }
}
