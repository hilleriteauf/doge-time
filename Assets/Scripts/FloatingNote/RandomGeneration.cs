using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomGeneration : MonoBehaviour
{
    public GameObject FloatingNotePrefab;
    private static List<GameObject> GeneratedFloatingNote = new List<GameObject>();
    private static Dictionary<string, int> NoteDispersion = new Dictionary<string, int>();
    public static int MaxNotesCount = 150;
    public static GameObject SpawnPoint;
    private float SpawnCoor;
    private float DespawnCoor;
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

        if (Camera.main==null) {Debug.LogError("Camera.main not found, failed to create edge colliders"); return;}

        var cam = Camera.main;
        if (!cam.orthographic) {Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return;}

        SpawnCoor = ((Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane))).x;
        DespawnCoor =  ((Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane))).x;
        Debug.Log(SpawnCoor);

        InitNoteDispersionTable();
        for (int i = 1; i < MaxNotesCount; i++)
        {   
            GeneratedFloatingNote.Add(GenerateNote(FloatingNotePrefab));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GeneratedFloatingNote.Count < MaxNotesCount)
        {
            GeneratedFloatingNote.Add(GenerateNote(FloatingNotePrefab));
        }
        
        List<GameObject> NotesToDelete = new List<GameObject>();

        foreach (GameObject Note in GeneratedFloatingNote)
        {
            Note.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 3 * Note.GetComponent<FloatingNote>().getSpeed());

            if (Note.transform.position.x > DespawnCoor + Note.GetComponent<SpriteRenderer>().transform.localScale.x )
            {
                NotesToDelete.Add(Note);
            }
        }

        foreach (GameObject Note in NotesToDelete)
        {
            GeneratedFloatingNote.Remove(Note);
            NoteDispersion[Note.GetComponent<FloatingNote>().getNote()] -= 1;
            Destroy(Note);
        }
    }

    private GameObject GenerateNote(GameObject toInstanciate) 
    {
        Vector3 objpos = new Vector3((SpawnCoor - toInstanciate.GetComponent<SpriteRenderer>().transform.localScale.x - Random.Range(0f, 2f)), Random.Range(-4.5f, 4.5f));


        GameObject toGenerate = Instantiate(toInstanciate);

        toGenerate.GetComponent<Transform>().position = objpos;
        toGenerate.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Note = NotSoRandomlyPicked();
        switch (Note)
        {
            case "Do":
                Color = new Color(1f, 0f, 0f);
                break;
            case "Re":
                Color =  new Color(0.7333f, 0.4470f, 0.0039f);
                break;
            case "Mi":
                Color =  new Color(1f, 0.7490f, 0.3686f);
                break;
            case "Fa":
                Color =  new Color(0.6117f, 0.9333f, 0.01960f);
                break;
            case "Sol":
                Color =  new Color(0.0196f, 0.6941f, 0.9921f);
                break;
            case "La":
                Color =  new Color(0.4274f, 0.0039f, 0.5843f);
                break;
            case "Si":
                Color =  new Color(1f, 0f, 0.9960f);
                break;

            default:
                Color =  Color.white;
                break;
        }

        toGenerate.GetComponent<FloatingNote>().setNote(Note);
        toGenerate.GetComponent<FloatingNote>().setNoteColor(Color);
        toGenerate.GetComponent<FloatingNote>().setSpeed(Random.Range(1f, 5f));

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
                        PickedNote = item;
                        break;
                    }
                }
                NoteDispersion[PickedNote] += 1;
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
}
