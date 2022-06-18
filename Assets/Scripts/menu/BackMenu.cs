using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{
    public static void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
