using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // handles the movement
    private bool facingRight;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public SpriteRenderer sprite;
    private float moveSpeed;
    void Start(){
        rb = GetComponent<Rigidbody2D> ();
        sprite = GetComponent<SpriteRenderer> ();
    }

    void FixedUpdate(){
        rb.velocity = new Vector2(moveDirection.x * moveSpeed,moveDirection.y * moveSpeed);
    }
    public void Move(Vector2 _moveDirection,float _moveSpeed){
        moveSpeed = _moveSpeed;
        moveDirection = _moveDirection;
        float moveX = moveDirection.x;
        float moveY = moveDirection.y;
        if(moveX < 0){
            sprite.flipX = true;
        }
        if (moveX > 0){
            sprite.flipX = false;
        }
    }


}
