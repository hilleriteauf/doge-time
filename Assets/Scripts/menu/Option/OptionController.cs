using UnityEngine;

public class OptionController : MonoBehaviour
{
    public GameObject scrollbarVolume;
    public GameObject scrollbarVitesse;
    public GameObject titreBackMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            MethodeStatic.BackToMenu();
    }
}
