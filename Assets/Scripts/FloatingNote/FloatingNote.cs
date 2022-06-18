using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNote : MonoBehaviour
{

    [SerializeField]
    private string Note;

    [SerializeField]
    private Color NoteColor;
    private float Speed;

    public void setNote(string _Note)
    {
        this.Note = _Note;
    }
    public string getNote()
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
}
