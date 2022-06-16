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
        foreach (GameObject Note in GeneratedFloatingNote)
        {
            Debug.Log(Note.ToString());
            //Note.GetComponent<FloatingNote>().Move(Time.deltaTime);
        }
    }

    private GameObject GenerateNote() 
    {
        GameObject toGenerate = FloatingNotePrefab;

        toGenerate.GetComponent<Transform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);

        List<string> AvailableNote = NotSoRandomlyPicked();
        Note = AvailableNote.Count > 1 ? AvailableNote[Random.Range(0,AvailableNote.Count)] : AvailableNote[0];
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
            NoteDispersion.Add("Do", 0);
            NoteDispersion.Add("Re", 0);
            NoteDispersion.Add("Mi", 0);
            NoteDispersion.Add("Fa", 0);
            NoteDispersion.Add("Sol", 0);
            NoteDispersion.Add("La", 0);
            NoteDispersion.Add("Si", 0);
        }
        catch (System.Exception)
        {
            Debug.Log("NoteDispersion initialization failed !");
            throw;
        }

        Debug.Log("NoteDispersion initialized !");
    }

    private List<string> NotSoRandomlyPicked() 
    {
        List<string> TempAvailableNote = new List<string>();

        foreach (var item in NoteDispersion)
        {
            if (item.Value <= 2)
            {
                NoteDispersion[item.Key] += 1;
                if (!TempAvailableNote.Contains(item.Key))
                {
                    TempAvailableNote.Add(item.Key);
                }
                Debug.Log(item.ToString());
            } else {
                TempAvailableNote.Remove(item.Key);
            }
        }

        return TempAvailableNote;
    }

    private void SpawnNote(GameObject toInstanciate) {
        Vector3 objpos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-4.5f, 4.5f));
        
        toInstanciate.GetComponent<Transform>().position = objpos;
        toInstanciate.GetComponent<FloatingNote>().setDirection(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));

        Instantiate(toInstanciate);
    }

}
