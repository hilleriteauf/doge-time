using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainObjectController : MonoBehaviour
{

    private GameObject TargetA;
    private GameObject TargetB;

    private float ExpandAnimationDuration = 0.5f;
    private float ExpandAnimationStartTime = -1;

    private SpriteRenderer SpriteRenderer;
    float InversedScale;

    public void StartAnimation(GameObject TargetA, GameObject TargetB)
    {
        this.TargetA = TargetA;
        this.TargetB = TargetB;
        ExpandAnimationStartTime = Time.time;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        InversedScale = 1 / transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (ExpandAnimationStartTime != -1f)
        {
            if (TargetB == null)
            {
                Destroy(gameObject);
                return;
            }

            float AnimationProgression = Mathf.Min((Time.time - ExpandAnimationStartTime) / ExpandAnimationDuration, 1f);
            float EasedProgression = Mathf.Sin((AnimationProgression * Mathf.PI) / 2f);

            Vector3 newPosition = Vector3.Lerp(TargetA.transform.position, TargetB.transform.position, EasedProgression / 2f);
            newPosition.z = -0.5f;
            transform.position = newPosition;

            SpriteRenderer.size = new Vector2(Mathf.Lerp(0, Vector3.Distance(TargetA.transform.position, TargetB.transform.position) * InversedScale, AnimationProgression), SpriteRenderer.size.y);
            //transform.LookAt(TargetB.transform.position);
            transform.right = new Vector3(TargetB.transform.position.x, TargetB.transform.position.y, -0.5f) - transform.position;
        }
    }
}
