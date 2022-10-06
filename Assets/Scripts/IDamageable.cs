using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// damage interface; header file kind of like c++
public interface IDamageable
{
    void takeHit(float damage);
    void takeDamage(float damage);

    void Die();
}
