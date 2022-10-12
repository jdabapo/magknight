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
        magneticObjects = LayerMask.GetMask("Magnet", "Magnetic");
    }


    void FixedUpdate()
    {
        rbMagnet();
    }

    void rbMagnet()
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

                    Magnet otherMag = mCollider.GetComponent<Magnet>();

                    rb.AddForce(coolFloat * magnetStrength * polarity * (otherMagPos - rb.position).normalized, ForceMode2D.Force);
                }
            }
        }
    }
}
