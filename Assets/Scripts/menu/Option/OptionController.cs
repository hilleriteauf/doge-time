using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    public GameObject titre;
    public GameObject scrollbar;

    // Start is called before the first frame update
    void Start()
    {
        titre.GetComponent<RectTransform>().localScale = MethodeStatic.getScale();
        titre.GetComponent<RectTransform>().localScale = MethodeStatic.getScale();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
