using System.IO;
using UnityEngine;

public static class MethodeStatic
{
    public static Vector2 getPositionRect(GameObject gObject)
    {
        return gObject.GetComponent<RectTransform>().position;
    }

    public static Vector3 getScale()
    {
        return new Vector3(Screen.width / 1920f, Screen.height / 1080f, 1);
    }
}
