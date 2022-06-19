using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnvoiNiveau : MonoBehaviour
{
    public static string niveau;

    private const int choixSceneJeu = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string GetNiveau()
    {
        return niveau;
    }

    public void SetNiveau()
    {
        niveau = GetComponent<TextMeshProUGUI>().text + ".mid";
        SceneManager.LoadScene(choixSceneJeu);
    }
}
