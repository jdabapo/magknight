using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5;	
    private Vector2 moveDirection;
	PlayerController controller;
    void Start(){
		controller = GetComponent<PlayerController> ();
    }
    void ProcessInput(){
        // movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX,moveY).normalized;
        controller.Move(moveDirection,moveSpeed);

        // mouse pos
        // https://www.youtube.com/watch?v=Geb_PnF1wOk&ab_channel=IndieNuggets
        Vector3 dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)); 
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }

    void Update(){
        ProcessInput();
    }
}
