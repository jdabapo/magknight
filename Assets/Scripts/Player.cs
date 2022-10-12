using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof(Rigidbody2D))]
public class Player : Humanoid
{

    public float msBetweenShots;
    public Projectile projectile;
    public float muzzleVelocity = 0;
    public Camera viewCamera;
    public enum MagneticState { Still, Attract, Repel };

    [HideInInspector]
    public List<GameObject> magnetList = new List<GameObject>();

    private MagneticState mState;
    private float nextShotTime;
    private Vector2 moveDirection;
    private PlayerController controller;
    private Vector2 mousePoint;

    private List<Projectile> connectedProjectiles = new List<Projectile>();


    protected override void Start()
    {
        base.Start();
		controller = GetComponent<PlayerController> ();
    }

    void Update()
    {
        mousePoint = viewCamera.ScreenToWorldPoint(Input.mousePosition);
        ProcessInput();
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


        //shooting projectile
        if (Input.GetMouseButton(1))
        {
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + msBetweenShots / 1000;
                Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as Projectile;
                newProjectile.SetMotion(gameObject, muzzleVelocity, transform.position, mousePoint);
                connectedProjectiles.Add(newProjectile);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mState != MagneticState.Attract)
            {
                mState = MagneticState.Attract;
            }
            else if (mState != MagneticState.Repel)
            {
                mState = MagneticState.Repel;
            }
            foreach (GameObject mag in magnetList)
            {
                Magnet magComp = mag.GetComponent<Magnet>();
                if (magComp != null)
                {
                    magComp.polarity = magDirFromState();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (magnetList != null)
            {
                foreach (GameObject mag in magnetList)
                {
                    Destroy(mag);
                }
                magnetList.Clear();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
            // check if other is a magnet; if it is a magnet, then two cases
            // 1. if it is thrown by player,
            // 2. or repulsed/attracted
            // check if other is another enemy.
            // 3 cases this way; enemies are attracted to one another, and smash and do dmage
            
    }

    int magDirFromState()
    {
        if (mState == MagneticState.Still)
        {
            return 0;
        }
        else if (mState == MagneticState.Attract)
        {
            return 1;
        }
        else if (mState == MagneticState.Repel)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
