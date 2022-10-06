using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Camera viewCamera;

    public Vector3 worldPosition;
    void Start()
    {
        viewCamera = Camera.main;
    }
    void Update()
    {
       MousePosition();
    }
   public void MousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        worldPosition  = viewCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPosition;
        Debug.Log(worldPosition);
    }
}
