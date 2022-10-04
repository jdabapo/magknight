using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // handles the movement
    
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private float moveSpeed;
    void Start(){
        rb = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate(){
        rb.velocity = new Vector2(moveDirection.x * moveSpeed,moveDirection.y * moveSpeed);
    }
    public void Move(Vector2 _moveDirection,float _moveSpeed){
        moveSpeed = _moveSpeed;
        moveDirection = _moveDirection;
    }
}
