using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RTSUnitMovement : MonoBehaviour
{
    Camera cam;
    public LayerMask ground;

    private void Start()
    {
        cam = Camera.main;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground)) 
            {
                // Set destination
                transform.position = hit.point;
            }

        }


    }
}
