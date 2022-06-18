using UnityEngine;

public static class MethodeStatic
{
    public static Vector2 getPositionRect(GameObject gObject)
    {
        return gObject.GetComponent<RectTransform>().position;
    }
}
