using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnvoiNiveau : MonoBehaviour
{
    public static string niveau;

    public static string GetNiveau()
    {
        return niveau;
    }

    public void SetNiveau()
    {
        niveau = GetComponent<TextMeshProUGUI>().name;
        SceneManager.LoadScene("GameScene");
    }
}
