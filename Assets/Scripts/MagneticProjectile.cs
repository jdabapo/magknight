using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticProjectile : Projectile
{


    //wtf does virtual mean??

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        GameObject e = collision.gameObject; // broken >:(
        if (e != null)
        {
            print(e.GetComponent<Enemy>().getMagnetizable());
            if (e.GetComponent<Enemy>().getMagnetizable()) //if the object is magnetizable. idk if this is the best way of doing this...
            {
                Magnet mag = collision.GetComponent<Magnet>();
                if (mag)
                {
                    mag.enabled = true;
                    collision.gameObject.layer = 6; //set to magnet layer

                    //this is such a dodgy way of doing this 0_0 because it assumes a lot that can potentially cause bugs
                    GameObject player = GameObject.FindWithTag("Player");
                    player.GetComponent<Player>().magnetList.Add(collision.gameObject);
                }
            }
        }
    }
}
