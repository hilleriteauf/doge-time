using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomGeneration : MonoBehaviour
{
    public GameObject FloatingNotePrefab;
    private static List<GameObject> GeneratedFloatingNote = new List<GameObject>();
    private static Dictionary<string, int> NoteDispersion = new Dictionary<string, int>();
    public static int MaxNotesCount = 21;
    public enum NoteList
    {
        Do,
        Re,
        Mi,
        Fa,
        Sol,
        La,
        Si
    }

    private string Note;
    private Color Color;

    private GameObject Temp;
    
    // Start is called before the first frame update
    void Start()
    {
        InitNoteDispersionTable();
        for (int i = 1; i < MaxNotesCount; i++)
        {   
            GameObject Temp = GenerateNote();
            GeneratedFloatingNote.Add(Temp);
            SpawnNote(Temp);
            Temp = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GeneratedFloatingNote.Count < MaxNotesCount)
        {
            GameObject Temp = GenerateNote();
            GeneratedFloatingNote.Add(Temp);
            SpawnNote(Temp);
            Temp = null;
        }
        /*foreach (GameObject Note in GeneratedFloatingNote)
        {
            Debug.Log(Note.ToString());
            //Note.GetComponent<FloatingNote>().Move(Time.deltaTime);
        }*/
    }

    private GameObject GenerateNote() 
    {
        GameObject toGenerate = FloatingNotePrefab;

        toGenerate.GetComponent<Transform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);

        Note = NotSoRandomlyPicked();
        switch (Note)
        {
            case "Do":
            Color = Color.red;
            break;
            case "Re":
            Color = Color.magenta;
            break;
            case "Mi":
            Color = Color.yellow;
            break;
            case "Fa":
            Color = Color.green;
            break;
            case "Sol":
            Color = Color.cyan;
            break;
            case "La":
            Color = Color.blue;
            break;
            case "Si":
            Color = Color.white;
            break;


            default:
            Color = Color.black;
            break;
        }

        toGenerate.GetComponent<FloatingNote>().setNote(Note);
        toGenerate.GetComponent<FloatingNote>().setNoteColor(Color);

        return toGenerate;
    }

    private void InitNoteDispersionTable()
    {
        try
        {
            NoteDispersion.Add(NoteList.Do.ToString(), 0);
            NoteDispersion.Add(NoteList.Re.ToString(), 0);
            NoteDispersion.Add(NoteList.Mi.ToString(), 0);
            NoteDispersion.Add(NoteList.Fa.ToString(), 0);
            NoteDispersion.Add(NoteList.Sol.ToString(), 0);
            NoteDispersion.Add(NoteList.La.ToString(), 0);
            NoteDispersion.Add(NoteList.Si.ToString(), 0);
        }
        catch (System.Exception)
        {
            Debug.Log("NoteDispersion initialization failed !");
            throw;
        }

        Debug.Log("NoteDispersion initialized !");
    }

    private string NotSoRandomlyPicked() 
    {
        string PickedNote = null;
        NoteList RandomPickingNote = (NoteList)Random.Range(0,5);
        int PreventInfinite = 0;


        while (PickedNote == null)
        {
            if (NoteDispersion.ContainsValue(0)) {
                foreach (string item in NoteDispersion.Keys)
                {
                    if (NoteDispersion[item] == 0)
                    {
                        NoteDispersion[item] += 1;
                        PickedNote = item;
                    }
                }
            } else
            {
                RandomPickingNote =  (NoteList)Random.Range(0,7);
                if (NoteDispersion[RandomPickingNote.ToString()] <= 2 || PreventInfinite > 500)
                {
                    NoteDispersion[RandomPickingNote.ToString()] += 1;
                    PickedNote = RandomPickingNote.ToString();
                    PreventInfinite = 0;
                }
                PreventInfinite++;
            }
        }
        return PickedNote;
    }

    private void SpawnNote(GameObject toInstanciate) {
        Vector3 objpos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-4.5f, 4.5f));
        
        toInstanciate.GetComponent<Transform>().position = objpos;
        toInstanciate.GetComponent<FloatingNote>().setDirection(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));

        Instantiate(toInstanciate);
    }

}