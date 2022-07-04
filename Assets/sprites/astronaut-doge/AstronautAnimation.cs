using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautAnimation : MonoBehaviour
{
    public float rotationSpeed;
    public Transform target;
    public float MaxSpeed = 0.5f;

    private bool Bonked = false;

    Rigidbody2D Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.angularVelocity = -10f;
    }

    public void BonkBonk(Vector3 direction)
    {
        Bonked = true;
        Debug.Log("Bonk Bonk !");
        Rigidbody2D.AddForce(direction * 100f);
        Rigidbody2D.AddTorque(50f);
    }

    private void FixedUpdate()
    {
        if (!Bonked)
        {
            Rigidbody2D.AddForce((target.position - transform.position) * 0.01f);
            if (Rigidbody2D.velocity.magnitude > MaxSpeed)
            {
                Rigidbody2D.velocity = Rigidbody2D.velocity.normalized * MaxSpeed;
            }
        }

        if (Bonked && Mathf.Abs(transform.position.y) > 14)
        {
            Bonked = false;
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.angularVelocity = -10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.Rotate(0, 0, rotationSpeed);
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
