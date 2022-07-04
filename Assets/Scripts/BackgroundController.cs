using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float LeftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float RightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        float SpriteWidth = GetComponent<SpriteRenderer>().size.x;

        float size = (RightEdge - LeftEdge) / SpriteWidth;
        transform.localScale = Vector3.one * size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
