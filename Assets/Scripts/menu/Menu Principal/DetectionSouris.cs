using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectionSouris : MonoBehaviour
{
    private const float textEchelle = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SourisDetecte()
    {
        GetComponent<Transform>().localScale = new Vector3(GetComponent<Transform>().localScale.x * textEchelle, GetComponent<Transform>().localScale.y * textEchelle, GetComponent<Transform>().localScale.z * textEchelle);
        GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }

    public void SourisPlusDetecte()
    {
        GetComponent<Transform>().localScale = new Vector3(GetComponent<Transform>().localScale.x / textEchelle, GetComponent<Transform>().localScale.y / textEchelle, GetComponent<Transform>().localScale.z / textEchelle);
        GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}
