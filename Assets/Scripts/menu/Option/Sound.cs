using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static float sound = 0.5f;

    public static void Initialisation(GameObject go)
    {
        go.GetComponent<Scrollbar>().value = sound;
    }

    public static float GetSound()
    {
        return sound;
    }

    public void SetSound()
    {
        sound = GetComponent<Scrollbar>().value;
    }

    public void UpdateTexte()
    {
        GetComponent<TextMeshProUGUI>().text = System.Convert.ToString((int)(sound * 100)) + " %";
    }
}
