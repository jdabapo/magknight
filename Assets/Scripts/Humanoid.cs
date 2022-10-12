using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// parent class for things that take damage
// VIRTUAL: call for being overwritten
public class Humanoid : MonoBehaviour, IDamageable
{

    [SerializeField]
    protected int startingHealth = 10;
    public int currentHealth;
    [SerializeField]
    protected int moveSpeed = 5;
    protected bool dead;
    [SerializeField]
    protected bool magnetizable;

    protected virtual void Start()
    {
        dead = false;
        currentHealth = startingHealth;
    }

    //
    public void takeHit(float damage){

    }

    //
    public void takeDamage(float damage){

    }

    public bool getMagnetizable()
    {
        return magnetizable;
    }

    public void Die(){
        dead = true;
        // do animations 
        GameObject.Destroy(gameObject);
    }
}
