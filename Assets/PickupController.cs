using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    Camera m_Camera;
    bool hasObject = false;
    CanBePickedUp carriedGameObject = null;
    void Awake()
    {
        m_Camera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasObject)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                print(hit);
                GameObject hitGameObject = hit.transform.gameObject;
                if (hitGameObject.tag == "CanBePickedUp" && !hasObject)
                {
                    carriedGameObject = hitGameObject.GetComponent<CanBePickedUp>();
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
                Vector3 newPosition = new Vector3(hit.point.x, hit.point.y + 3, hit.point.z);
                carriedGameObject.transform.position = newPosition;
            }

            if (Input.GetMouseButtonDown(0) && hasObject)
            {
                carriedGameObject.release();
                hasObject = false;
                carriedGameObject = null;
            }
        }
    }
}
