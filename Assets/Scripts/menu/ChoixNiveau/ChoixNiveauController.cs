using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChoixNiveauController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject prefab;

    private static bool bloqueGeneration = false;

    private const int nbNivParLig = 3;
    private Vector2 tailleText = new Vector2(600, 90);
    private float espaceEntreTexte;

    private List<string> listText;

    // Start is called before the first frame update
    void Start()
    {
        if (!bloqueGeneration)
        {
            bloqueGeneration = true;

            listText = new List<string>();

            espaceEntreTexte = (Screen.width - nbNivParLig * tailleText.x) / (nbNivParLig + 1);

            DirectoryInfo infoFolder = new DirectoryInfo("Assets/Resources/Midis");
            FileInfo[] tabFileAux = infoFolder.GetFiles();

            for (int i = 0; i < tabFileAux.Length; i += 2)
                listText.Add(MethodeStatic.getNameMidi(tabFileAux[i]));

            Vector2 vecAux = new Vector2(0, 0);
            for (int i = 0; i < listText.Count; i++)
            {
                TextMeshProUGUI text = Instantiate(prefab).GetComponent<TextMeshProUGUI>();
                text.transform.SetParent(canvas.transform);

                if (i % nbNivParLig == 0)
                    vecAux = new Vector2(espaceEntreTexte + tailleText.x / 2, Screen.height - 200 * (i + 1) / nbNivParLig);
                else
                    vecAux = new Vector2(vecAux.x + espaceEntreTexte + tailleText.x, vecAux.y);

                text.name = i.ToString();
                text.GetComponent<RectTransform>().position = vecAux;
                text.GetComponent<RectTransform>().sizeDelta = tailleText;
                text.text = listText[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
