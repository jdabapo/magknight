using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class Enemy : Humanoid
{
    private float distance;
    public float distanceBetween = 3;
    public float attackRange;
    public Sprite redMagnetizedSprite; //Sprite when enemy is hit by red magnet. maybe this is better if we cycle sprite between one sprite sheet rather than just one sprite.
    private enum magneticID{None,Red,Blue}; 
       

    public SpriteRenderer sprRen;
    Rigidbody2D rb;
    Transform target;
    Magnet mag;

    protected override void Start(){
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sprRen = GetComponent<SpriteRenderer> ();
        mag = GetComponent<Magnet>(); //possibility that not all enemies will be magnetizable?
    }

    private void Follow(){
        distance = Vector2.Distance(target.position,transform.position);
        Vector2 targetDirection = (target.transform.position - transform.position);
        targetDirection.Normalize();
        if (distance > distanceBetween){
            transform.position = Vector2.MoveTowards(transform.position,target.transform.position,moveSpeed * Time.deltaTime);
        }
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 90){
            // face left
            sprRen.flipX = true;
        }
        else {
            sprRen.flipX = false;
        }
    }

    private void UpdateSprite()
    {
        if (mag.enabled) {
            sprRen.sprite = redMagnetizedSprite;
        }
    }

    void Update(){ 
       Follow();
       UpdateSprite();
    }

    // TODO: make this work
    private void OnTriggerEnter2D(Collider2D other){
        // check if other is a magnet; if it is a magnet, then two cases
        // 1. if it is thrown by player,
        // 2. or repulsed/attracted
        // check if other is another enemy.
        // 3 cases this way; enemies are attracted to one another, and smash and do dmage
        
    }
}
