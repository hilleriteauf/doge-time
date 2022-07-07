using UnityEngine;

public class OptionController : MonoBehaviour
{
    public GameObject scrollbarVolume;
    public GameObject scrollbarVitesse;
    public GameObject titreBackMenu;

    // Start is called before the first frame update
    void Start()
    {
        Sound.Initialisation(scrollbarVolume);
        Vitesse.Initialisation(scrollbarVitesse);
    }

    // Update is called once per frame
    void Update()
    {
        MethodeStatic.DetectionBackToMenu();
    }
}
