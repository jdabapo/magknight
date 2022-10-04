using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    bool hasTarget = false;
    public float mininumDistance = 0;
    public float moveSpeed = 5;
	Transform target;
    void Start(){
            hasTarget = true;
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			StartCoroutine (Follow ());
    }

    IEnumerator Follow(){
        float refreshRate = 0.5f;

                while (Vector2.Distance(transform.position,target.position) > mininumDistance) {
                    Vector2 dirToTarget = (target.position - transform.position).normalized;
                    transform.position = Vector2.MoveTowards(transform.position,target.position,moveSpeed * Time.deltaTime);
                    yield return new WaitForSeconds(refreshRate);
                }
    }
}
