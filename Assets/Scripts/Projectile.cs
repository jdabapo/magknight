using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2;
    public float damage = 0;
    public bool canShootThrough;

    protected Vector2 startPosition;
    protected Vector2 direction;
    protected float speed = 0;
    protected GameObject projOrigin; //gameobject that shot projectile

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        moveTowardPoint();
    }

    public void SetMotion(GameObject newOrigin, float newSpeed, Vector2 newStartPosition, Vector2 newDirection)
    {
        speed = newSpeed;
        startPosition = newStartPosition;
        direction = newDirection;
        projOrigin = newOrigin;

        Transform graphic = transform.GetChild(0); //Assumes graphic is the first child of the object
        if (graphic && graphic.tag == "Graphic")
        {
            graphic.up = (direction - startPosition); //Assumes orientation of the projectile's graphic is up
        }
    }

    public void moveTowardPoint()
    {
        transform.Translate((direction-startPosition).normalized * Time.deltaTime * speed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != projOrigin) //canShootThrough goes here but i cant think rn .-.
        {
            Destroy(gameObject);
        }
    }
}
