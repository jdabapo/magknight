using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Controller : MonoBehaviour
{
    public GameObject magnetPrefab;
    List<GameObject> magnetList = new List<GameObject>();
    Camera viewCamera;

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        Vector2 point = viewCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
        
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            Debug.DrawLine(viewCamera.transform.position, point, Color.red);
            if (hit.collider != null) {
                //print("HIT!! " + hit.transform.name);
                hit.transform.position = point;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject newMagnet = Instantiate(magnetPrefab, point, Quaternion.identity);
            magnetList.Add(newMagnet);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (magnetList != null)
            {
                foreach(GameObject mag in magnetList)
                {
                    Destroy(mag);
                }
                magnetList.Clear();
            }
        }
        // get the movement
    }
    


}
