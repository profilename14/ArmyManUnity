using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private Camera m_Camera;
    private bool hasObject = false;
    private CanBePickedUp carriedGameObject = null;

    public bool canCarryObjects = true;
    void Awake()
    {
        m_Camera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasObject && canCarryObjects)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                print(hit);
                GameObject hitGameObject = hit.transform.gameObject;
                CanBePickedUp hitCarriable = hitGameObject.GetComponent<CanBePickedUp>();
                if (hitCarriable != null && !hasObject)
                {
                    carriedGameObject = hitCarriable;
                    hasObject = true;
                    carriedGameObject.pickup(); // Disables stuff like hitboxes and rotation
                    print("Now carrying " + hitGameObject);
                }

            }
        } else if (hasObject)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 newPosition;

                if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
                {
                    float newX = Mathf.Round(hit.point.x * 1.0f) * 1f;
                    float newZ = Mathf.Round(hit.point.z * 1.0f) * 1f;
                    newPosition = new Vector3(newX, hit.point.y + 3, newZ);
                }
                else 
                {
                    newPosition = new Vector3(hit.point.x, hit.point.y + 3, hit.point.z);
                }
                carriedGameObject.transform.position = newPosition;
            }

            if (Input.GetKeyDown(KeyCode.Period))
            {
                carriedGameObject.transform.Rotate(0, 45, 0);
            }
            else if (Input.GetKeyDown(KeyCode.Comma))
            {
                carriedGameObject.transform.Rotate(0, -45, 0);
            }

            if ( (Input.GetMouseButtonDown(0) || !canCarryObjects) && hasObject)
            {
                carriedGameObject.release();
                hasObject = false;
                carriedGameObject = null;
            }
        }
    }
}
