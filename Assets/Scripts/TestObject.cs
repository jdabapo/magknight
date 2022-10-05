using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    public float magnetForce = 100;

    List<Rigidbody2D> caughtRigidbodies = new List<Rigidbody2D>();

    void FixedUpdate()
    {
        for (int i = 0; i < caughtRigidbodies.Count; i++)
        {
            Vector2 otherRbPos = new Vector2(caughtRigidbodies[i].transform.position.x, caughtRigidbodies[i].transform.position.y);
            Vector2 transformPos = new Vector2(transform.position.x, transform.position.y);
            caughtRigidbodies[i].velocity = (transformPos - (otherRbPos + caughtRigidbodies[i].centerOfMass)) * magnetForce * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D r = other.GetComponent<Rigidbody2D>();

            if (!caughtRigidbodies.Contains(r))
            {
                //Add Rigidbody2D
                caughtRigidbodies.Add(r);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D r = other.GetComponent<Rigidbody2D>();

            if (caughtRigidbodies.Contains(r))
            {
                //Remove Rigidbody2D
                caughtRigidbodies.Remove(r);
            }
        }
    }
}
