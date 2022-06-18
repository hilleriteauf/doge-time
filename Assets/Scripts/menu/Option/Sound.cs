using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static float sound = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float GetSound()
    {
        return sound;
    }

    public void SetSound()
    {
        sound = GetComponent<Scrollbar>().value;
    }
}
