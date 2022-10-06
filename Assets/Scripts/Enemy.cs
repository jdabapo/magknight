using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class Enemy : Humanoid
{

    private bool facingRight;
    private float distance;
    public float distanceBetween = 4;
    public float attackRange;
    public SpriteRenderer sprite;
    Rigidbody2D rb;
    Transform target; 
    protected override void Start(){
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 0;
        sprite = GetComponentInChildren<SpriteRenderer> ();
        //StartCoroutine(UpdatePath());
    }


    void Update(){ 
        distance = Vector2.Distance(target.position,transform.position);
        Vector2 targetDirection = (target.transform.position - transform.position);
        targetDirection.Normalize();
        float angle = Mathf.Atan2(targetDirection.x,targetDirection.y) * Mathf.Rad2Deg;
        if (distance < distanceBetween){
            
            transform.position = Vector2.MoveTowards(transform.position,target.transform.position,moveSpeed * Time.deltaTime);
        }
        // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        if (angle < 0){
            // face left
            sprite.flipX = true;
        }
        else {
            sprite.flipX = false;
        }
    }
    public void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        facingRight = !facingRight;
    }
    // TODO: make this work
    private void onTriggerEnter2D(Collider2D other){
        // check if other is a magnet; if it is a magnet, then two cases
        // 1. if it is thrown by player,
        // 2. or repulsed/attracted
        Debug.Log("hit detected");
        // check if other is another enemy.
        // 3 cases this way; enemies are attracted to one another, and smash and do dmage
        
    }
}
