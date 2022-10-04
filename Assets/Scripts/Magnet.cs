using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Magnet : MonoBehaviour
{
    public float magnetRadius;
    public float pullSpeed;
    GameObject[] magnets;

    Collider2D c;
    Rigidbody2D rb;

    void Start()
    {
        c = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(Attract());
    }


    void Update()
    {
        magnets = GameObject.FindGameObjectsWithTag("Magnet");

        foreach (GameObject mag in magnets)
        {
            float distanceSqr = (transform.position - mag.transform.position).sqrMagnitude;
            if (distanceSqr < Mathf.Pow(magnetRadius, 2)) {
                mag.transform.right = transform.position - mag.transform.position;
                //Rigidbody2D magRb = mag.GetComponent<Rigidbody2D>();
                //if (true) {
                mag.transform.Translate((((transform.position-mag.transform.position).normalized) * Time.deltaTime * pullSpeed), Space.World);

                //}
            }
        }
    }

    IEnumerator Attract() { 
        float refreshRate = 0.25f;



        yield return new WaitForSeconds(refreshRate);
    }
}
