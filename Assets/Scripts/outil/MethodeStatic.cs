using System.IO;
using UnityEngine;

public static class MethodeStatic
{
    public static Vector2 getPositionRect(GameObject gObject)
    {
        return gObject.GetComponent<RectTransform>().position;
    }

    public static string getNameMidi(FileInfo fileInfo)
    {
        return Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(fileInfo.Name));
    }
}
