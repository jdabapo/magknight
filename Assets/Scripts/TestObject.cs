using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    private void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(point.x, point.y, 0);

    }
}
