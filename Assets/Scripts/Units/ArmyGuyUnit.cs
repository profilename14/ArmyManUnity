using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyGuyUnit : MonoBehaviour
{
    Camera cam;
    public LayerMask ground;

    public Transform target;
    public GameObject pellet;
    public float pelletVelocity = 100;
    public Transform pelletOrigin;
    public GameObject selectedMarker;

    [Header("Sounds")]
    public AudioSource speaker;
    public AudioClip[] shootSFX;
    public AudioClip[] moveSFX;

    [Header("Stats")]
    public float health = 1;
    public float turnSpeed = 200;
    public float moveSpeed = 10;
    public float reloadTime = .5f;

    private float reloadTick;
    private Transform targetMovePosition;
    public bool isSelected;

    private void Awake()
    {
        isSelected = false;
        reloadTick = 0f;
        cam = Camera.main;
        speaker = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Switch depending on situation
        // If selected

        if (isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {         
                // We need some way to disable this if the camera is being rotated.

                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    // Set destination
                    GetComponent<AIDestinationSetter>().target.position = hit.point;
                }
            }
        }


        // Move to Target Position
        // If Moving, don't fire

        // Find Nearest Target
        foreach (EnemyUnit e in FindObjectsOfType<EnemyUnit>())
        {
            if (Vector3.Distance(e.transform.position, transform.position) < 20f)
            {
                // An enemy is in your radius
                //e.GetHurt(); // Hit the enemy for example
                target = e.transform;
            }
        }
        // Turn Towards Target
        // Determine which direction to rotate towards
        var direction = (target.transform.position - transform.position).normalized;

        var targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        reloadTick += Time.deltaTime;

        // Fire Bullet
        if (reloadTick >= reloadTime)
        {
            reloadTick = 0f;

            GameObject bullet = Instantiate(pellet, pelletOrigin.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, pelletVelocity));
            AudioClip randomClip = shootSFX[Random.Range(0, shootSFX.Length)];
            speaker.PlayOneShot(randomClip);
        }
    }

    public void TakeDamage(int amount = 1)
    {
        health -= amount;

        if (health <= 0)
        {
            // Die
            Destroy(gameObject);
        }
    }
}
