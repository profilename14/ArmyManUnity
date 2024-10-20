using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupController : MonoBehaviour
{
    private Camera m_Camera;
    private bool hasObject = false;
    private CanBePickedUp carriedGameObject = null;

    private AudioSource speaker;
    public AudioClip[] rotateSFX;
    public AudioClip[] pickupSFX;

    public bool canCarryObjects = true;
    private bool movementLock = false;
    void Awake()
    {
        m_Camera = Camera.main;
        speaker = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (movementLock)
            {
                movementLock = false;
            }
            else
            {
                movementLock = true;
            }
        }


        if (Input.GetMouseButtonDown(0) && !hasObject && canCarryObjects)
        {
            AudioClip randomClip = pickupSFX[Random.Range(0, pickupSFX.Length)];
            speaker.PlayOneShot(randomClip);

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

                if (movementLock)
                {
                    float newX = Mathf.Round(hit.point.x * 1.0f) * 1f;
                    float newZ = Mathf.Round(hit.point.z * 1.0f) * 1f;
                    newPosition = new Vector3(newX, hit.point.y + 3, newZ);
                }
                else 
                {
                    newPosition = new Vector3(hit.point.x, hit.point.y + 3, hit.point.z);
                }

                if (hit.transform.gameObject.layer == 6) {
                    carriedGameObject.transform.position = newPosition;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                rotateObject(45);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                rotateObject(-45);
            }

            if ( (Input.GetMouseButtonDown(0) || !canCarryObjects) && hasObject)
            {
                AudioClip randomClip = pickupSFX[Random.Range(0, pickupSFX.Length)];
                speaker.PlayOneShot(randomClip);

                carriedGameObject.release();
                hasObject = false;
                carriedGameObject = null;
            }
        }
    }

    private void rotateObject(float degrees)
    {
        AudioClip randomClip = rotateSFX[Random.Range(0, rotateSFX.Length)];
        speaker.PlayOneShot(randomClip);

        if (carriedGameObject.rotateAboutSlopeAxis)
        {
            if (carriedGameObject.transform.eulerAngles.z % 45 > 1 && carriedGameObject.transform.eulerAngles.z % 45 < 44)
            {
                carriedGameObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            }
            else
            {
                carriedGameObject.transform.Rotate(0, 0, degrees);
            }
        }
        else
        {
            if (carriedGameObject.transform.eulerAngles.y % 45 > 1 && carriedGameObject.transform.eulerAngles.y % 45 < 44)
            {
                carriedGameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                carriedGameObject.transform.Rotate(0, degrees, 0);
            }
        }


    }
}
