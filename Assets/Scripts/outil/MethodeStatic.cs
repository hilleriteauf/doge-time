using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MethodeStatic : MonoBehaviour
{
    public static Vector2 GetPositionRect(TextMeshProUGUI gObject)
    {
        return gObject.GetComponent<RectTransform>().position;
    }

    public static Vector2 GetSizeRect(TextMeshProUGUI text)
    {
        return text.GetComponent<RectTransform>().sizeDelta;
    }

    public static Vector2 GetScaleRect(GameObject gObject)
    {
        return gObject.GetComponent<RectTransform>().localScale;
    }

    public static Vector2 MultiplicationVector2(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    /*
     * GESTION DES SCENES
     */
    public static void BackToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public static void DetectionBackToMenu()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            BackToMenu();
    }
    public static void EchapBackToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackToMenu();
    }

    public static void ActiveScene(string scene)
    {
        if (scene == "Quit")
            Application.Quit();
        else
            SceneManager.LoadScene(scene);
    }
}
