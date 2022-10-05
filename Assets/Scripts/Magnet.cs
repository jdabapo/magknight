using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Magnet : MonoBehaviour
{
    public float magnetRadius;
    public float magnetStrength;
    public LayerMask magneticObjects;
    public int polarity; //0 = still, 1 = attract, -1 = repel
    public bool dampen;


    GameObject[] magnets;

    CircleCollider2D cCollider;
    Rigidbody2D rb;

    void Start()
    {
        cCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        rbMagnet2();
    }

    /* 
     * 
     * 
     * 
     * 
     */
    void rbMagnet1()
    {
        if (polarity != 0)
        {
            Collider2D[] nearMagnets = Physics2D.OverlapCircleAll(transform.position, magnetRadius, magneticObjects);
            foreach (Collider2D mCollider in nearMagnets)
            {
                Vector2 otherMagPos;
                if (mCollider.GetComponent<Rigidbody2D>())
                {
                    otherMagPos = mCollider.GetComponent<Rigidbody2D>().position;
                }
                else
                {
                    otherMagPos = mCollider.transform.position;
                }

           
                // SPIN TIME
                //transform.right = otherMagPos - new Vector2(transform.position.x, transform.position.y);

                float sqrMag = (otherMagPos - rb.position).sqrMagnitude; //represents the distance between the two magnets. Not Distance function bc sqrt is expensive >:(
                //factor that makes the force large the close it is to the other magnet.
                float coolFloat = magnetStrength / (2*(sqrMag + 0.01f));
                if (!dampen) {
                    coolFloat /= coolFloat;
                }

                rb.AddForce(coolFloat * magnetStrength * polarity * (otherMagPos - rb.position).normalized, ForceMode2D.Force);
            }
        }
    }

    void rbMagnet2()
    {
        if (polarity != 0)
        {
            
            Collider2D[] nearMagnets = Physics2D.OverlapCircleAll(transform.position, magnetRadius, magneticObjects);
            foreach (Collider2D mCollider in nearMagnets)
            {
                if (mCollider != null)
                {
                    Vector2 otherMagPos;
                    if (mCollider.GetComponent<Rigidbody2D>())
                    {
                        otherMagPos = mCollider.GetComponent<Rigidbody2D>().position;
                    }
                    else
                    {
                        otherMagPos = mCollider.transform.position;
                    }

                    float sqrMag = (otherMagPos - rb.position).sqrMagnitude; //represents the distance between the two magnets. Not Distance function bc sqrt is expensive >:(
                                                                             
                    float coolFloat = magnetStrength / (2 * (sqrMag + 0.01f)); //factor that makes the force large the close it is to the other magnet.
                    if (!dampen)
                    {
                        coolFloat /= coolFloat;
                    }

                    rb.AddForce(coolFloat * magnetStrength * polarity * (otherMagPos - rb.position).normalized, ForceMode2D.Force);
                }
            }
        }
    }
}
