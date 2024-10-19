using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBePickedUp : MonoBehaviour
{
    Camera m_Camera;

    private bool isBeingPickedUp = false;
    private Rigidbody thisRigidbody;
    private Collider thisCollider;

    void Awake()
    {
        m_Camera = Camera.main;
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        thisCollider = gameObject.gameObject.GetComponent<Collider>();
    }
    void FixedUpdate()
    {
        if (isBeingPickedUp)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    public void pickup()
    {
        thisRigidbody.freezeRotation = true;
        thisRigidbody.useGravity = false;
        thisCollider.enabled = false;
        isBeingPickedUp = true;
    }

    public void release()
    {
        thisRigidbody.freezeRotation = false;
        thisRigidbody.useGravity = true;
        thisCollider.enabled = true;
        isBeingPickedUp = false;

    }
}
