using UnityEngine;
using UnityEngine.UI;

public class Vitesse : MonoBehaviour
{
    public static float value = 0.5f;
    public static float vitesse = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float GetVitesse()
    {
        return vitesse;
    }

    public void SetVitesse()
    {
        value = GetComponent<Scrollbar>().value;
        if (value < 0.33f)
            vitesse = 0.5f;
        else if (value > 0.66f)
            vitesse = 1f;
        else
            vitesse = 0.75f;
    }
}
