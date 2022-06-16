using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNote : MonoBehaviour
{

    [SerializeField]
    private string Note;

    [SerializeField]
    private Color NoteColor;

    private Vector2 Direction;

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
    public void setDirection(Vector2 _Direction)
    {
        this.Direction = _Direction;
    }
    public Vector2 getDirection()
    {
        return this.Direction;
    }

    public void Move(float delta)
    {
        Vector3 _Mouvement = new Vector3(Direction.x, Direction.y, 0);
        this.GetComponent<Transform>().position += _Mouvement * delta;
    }

}
