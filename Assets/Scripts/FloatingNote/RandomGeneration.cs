using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomGeneration : MonoBehaviour
{
    public GameObject FloatingNotePrefab;

    public Transform UpperSpawnEdge;
    public Transform LowerSpawnEdge;

    private static List<GameObject> GeneratedFloatingNote = new List<GameObject>();
    private static int[] NoteDispersion = new int[7];
    public static int MaxNotesCount = 150;
    public static GameObject SpawnPoint;
    private float SpawnCoor;
    private float DespawnCoor;

    private MusicNote Note;
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
            GeneratedFloatingNote.Add(GenerateNote(FloatingNotePrefab, true));
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
            NoteDispersion[(int)Note.GetComponent<FloatingNote>().getNote() / 10] -= 1;
            Destroy(Note);
        }
    }

    private GameObject GenerateNote(GameObject toInstanciate, bool ToStart = false) 
    {
        Vector3 objpos;
        if (ToStart)
        {
            objpos = new Vector3(Random.Range(SpawnCoor, 9.5f), Random.Range(LowerSpawnEdge.position.y, UpperSpawnEdge.position.y), -1);
        } else
        {
            objpos = new Vector3((SpawnCoor - toInstanciate.GetComponent<SpriteRenderer>().transform.localScale.x - Random.Range(0f, 2f)), Random.Range(LowerSpawnEdge.position.y, UpperSpawnEdge.position.y), -1);
        }


        GameObject toGenerate = Instantiate(toInstanciate, objpos, Quaternion.identity, transform);

        toGenerate.GetComponent<Transform>().localScale = new Vector3(0.2f, 0.2f, 0.2f);

        Note = NotSoRandomlyPicked();
        Color = MusicNoteHelper.GetMusicNoteColor(Note);

        toGenerate.GetComponent<FloatingNote>().setNote(Note);
        toGenerate.GetComponent<FloatingNote>().setNoteColor(Color);
        toGenerate.GetComponent<FloatingNote>().setSpeed(Random.Range(1f, 5f));

        return toGenerate;
    }

    private void InitNoteDispersionTable()
    {

        Debug.Log("NoteDispersion initialized !");
    }

    private MusicNote NotSoRandomlyPicked() 
    {
        int RandomPickingNote = (Random.Range(0, 7));
        int PreventInfinite = 0;


        while (true)
        {
            for (int i = 0; i < NoteDispersion.Length; i++)
            {
                if (NoteDispersion[i] == 0)
                {
                    NoteDispersion[i] += 1;
                    return (MusicNote)(i * 10);
                }
            }
            
            RandomPickingNote = Random.Range(0,7);
            if (NoteDispersion[RandomPickingNote] <= 2 || PreventInfinite > 500)
            {
                NoteDispersion[RandomPickingNote] += 1;
                return (MusicNote)(RandomPickingNote * 10);
            }
            PreventInfinite++;
        }
    }

    public GameObject GetBallToPlace(MusicNote MusicNote, Vector3 GuidePosition)
    {

        GameObject Closest = null;
        float ClosestDistance = float.MaxValue;

        for (int i = 0; i < GeneratedFloatingNote.Count; i++)
        {
            if (GeneratedFloatingNote[i].GetComponent<FloatingNote>().getNote() == MusicNote)
            {
                float Distance = Mathf.Abs(GeneratedFloatingNote[i].transform.position.x - GuidePosition.x);
                if (Distance < ClosestDistance)
                {
                    Closest = GeneratedFloatingNote[i];
                    ClosestDistance = Distance;
                }
            }
        }

        if (Closest != null)
        {
            GeneratedFloatingNote.Remove(Closest);
            NoteDispersion[(int)MusicNote / 10] -= 1;
        }

        return Closest;
    }
}
