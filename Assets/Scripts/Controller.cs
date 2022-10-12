using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject magnetPrefab;
    public GameObject magneticPrefab;
    public enum MagneticState {Still,Attract,Repel};

    MagneticState mState;

    List<GameObject> magnetList = new List<GameObject>();
    List<GameObject> magneticList = new List<GameObject>();

    Camera viewCamera;

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {

        Vector2 point = viewCamera.ScreenToWorldPoint(Input.mousePosition);

        //click and drag a placed magnet
        if (Input.GetMouseButton(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.collider != null) {
                if (hit.transform.tag == "Magnet") {
                    //The magnetic state is not yet coded to reset after letting go
                    //hit.transform.gameObject.GetComponent<Magnet>().polarity = 0;
                }
                hit.transform.position = point;
            }
        }

        /* Here I want to ensure that UNTIL left mouse is let go, only the object
         * that is currently selected can be moved.
         * ALSO if the object is a magnet I want to reset it back to its original
         * magnetic state when let go.
         */
        if (Input.GetMouseButtonUp(0))
        {

        }

        /*
        //add new magnet to the game
        if (Input.GetMouseButtonDown(1))
        {
            GameObject newMagnet = Instantiate(magnetPrefab, point, Quaternion.identity);
            Magnet magComp = newMagnet.GetComponent<Magnet>();
            if (magComp != null) {
                magComp.polarity = magDirFromState();
            }
            magnetList.Add(newMagnet);
        }
        */

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.collider != null)
            {
                Destroy(hit.transform.gameObject);
            }
        }

        //remove all magnets and test objects that have been placed
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
            if (magneticList != null)
            {
                foreach (GameObject mObj in magneticList)
                {
                    Destroy(mObj);
                }
                magneticList.Clear();
            }
        }

        //place test object at mouse pointer
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject newTestObject = Instantiate(magneticPrefab, point, Quaternion.identity);
            magneticList.Add(newTestObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mState != MagneticState.Attract)
            {
                mState = MagneticState.Attract;
            } else if (mState != MagneticState.Repel)
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

        //switch magnetic state of all magnets
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (mState != MagneticState.Still) {
                mState = MagneticState.Still;
                foreach (GameObject mag in magnetList) {
                    Magnet magComp = mag.GetComponent<Magnet>();
                    if (magComp != null)
                    {
                        magComp.polarity = 0;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            Camera.main.orthographicSize += 2;
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Camera.main.orthographicSize -= 2;
        }
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
