using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkController : MonoBehaviour
{
    public Transform TargetPosition;
    public float BonkRotationDuration = 0.1f;
    public float BonkRotationAmplitude = 45;

    private Vector3 InitialScale;
    private float LastBonkTime = -1f;

    // Start is called before the first frame update
    void Start()
    {
        InitialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (LastBonkTime != -1f && Time.time < LastBonkTime + BonkRotationDuration)
        {
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, BonkRotationAmplitude * Mathf.Sign(transform.localScale.x), (Time.time - LastBonkTime) / BonkRotationDuration));
        }
    }

    public void Bonk(Vector3 BallPosition, Vector3 DestinationPosition)
    {
        transform.localScale = Vector3.Scale(InitialScale, new Vector3(DestinationPosition.x < BallPosition.x ? 1f : -1f, 1, 1));
        transform.position = BallPosition - new Vector3(TargetPosition.localPosition.x * transform.localScale.x, TargetPosition.localPosition.y * transform.localScale.y, 0);
        LastBonkTime = Time.time;
    }
}
