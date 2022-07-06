using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vitesse : MonoBehaviour
{
    private static float value = 0.5f;

    public static void Initialisation(GameObject go)
    {
        go.GetComponent<Scrollbar>().value = value;
    }

    public static float GetVitesse()
    {
        if (value < 0.33f)
            return 0.5f;
        else
        {
            if (value > 0.66f)
                return 1f;
            else
                return 0.75f;
        }
    }

    public void SetVitesse()
    {
        value = GetComponent<Scrollbar>().value;
    }

    public void UpdateTexte()
    {
        GetComponent<TextMeshProUGUI>().text = "x " + System.Convert.ToString(GetVitesse());
    }

    public float GetValue()
    {
        return value;
    }
}
