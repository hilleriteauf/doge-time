using UnityEngine;

public class CreditController : MonoBehaviour
{
    public GameObject titre;

    // Start is called before the first frame update
    void Start()
    {
        titre.GetComponent<RectTransform>().localScale = MethodeStatic.getScale();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
