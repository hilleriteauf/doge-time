using System.IO;
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

    public static Vector2 GetScale()
    {
        return new Vector2(Screen.width / 1920f, Screen.height / 1080f);
    }

    public static Vector2 MultiplicationVector2(Vector2 a, Vector2 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y);
    }

    public static void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
