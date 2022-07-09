using System;
using TMPro;
using UnityEngine;

public class ChoixNiveauController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject prefab;

    private const int nbNivParLig = 3;
    private int nbNivParCol;
    private readonly Vector2 tailleText = new(600, 90);
    private float longueurEntreTexte;
    private float hauteurEntreTexte;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Object[] tabFileAux = Resources.LoadAll("Midis", typeof(TextAsset));
        nbNivParCol = (int)Math.Ceiling(tabFileAux.Length / 3.0);
        Debug.Log(nbNivParCol);

        Vector2 tailleTextEchelle = MethodeStatic.MultiplicationVector2(tailleText, MethodeStatic.GetScaleRect(canvas));
        longueurEntreTexte = (Screen.width - nbNivParLig * tailleTextEchelle.x) / (nbNivParLig + 1);
        hauteurEntreTexte = (Screen.height - nbNivParCol * tailleTextEchelle.y) / (nbNivParCol + 1);

        Vector2 vecAux = new(0, Screen.height + tailleTextEchelle.y / 2);
        String str;
        for (int i = 0; i < tabFileAux.Length; i++)
        {
            TextMeshProUGUI text = Instantiate(prefab).GetComponent<TextMeshProUGUI>();
            text.transform.SetParent(canvas.transform);

            if (i % nbNivParLig == 0)
                vecAux = new Vector2(longueurEntreTexte + tailleTextEchelle.x / 2, vecAux.y - hauteurEntreTexte - tailleTextEchelle.y);
            else
                vecAux = new Vector2(vecAux.x + longueurEntreTexte + tailleTextEchelle.x, vecAux.y);

            str = tabFileAux[i].name;
            text.name = str;
            text.GetComponent<RectTransform>().position = vecAux;
            text.GetComponent<RectTransform>().sizeDelta = tailleText;
            text.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            str = str.Remove(str.Length - 4);//Enlève le .mid à la fin du nom de fichier
            text.text = str;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MethodeStatic.EchapBackToMenu();
    }
}
