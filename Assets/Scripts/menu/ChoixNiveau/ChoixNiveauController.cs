using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoixNiveauController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject prefab;

    private const int nbNivParLig = 3;
    private Vector2 tailleText;
    private float espaceEntreTexte;

    private List<string> listText;

    // Start is called before the first frame update
    void Start()
    {
        tailleText = new Vector2(600, 90);

        listText = new List<string>();

        espaceEntreTexte = (Screen.width - nbNivParLig * tailleText.x) / (nbNivParLig + 1);

        Object[] tabFileAux = Resources.LoadAll("Midis", typeof(TextAsset));
        
        for (int i = 0; i < tabFileAux.Length; i++)
            listText.Add(tabFileAux[i].name);

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
