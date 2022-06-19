using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        GameObject text = GameObject.FindGameObjectsWithTag("VolumeMusique")[0];
        text.GetComponent<TextMeshProUGUI>().text = System.Convert.ToString((int)(sound*100)) + " %";
    }
}
