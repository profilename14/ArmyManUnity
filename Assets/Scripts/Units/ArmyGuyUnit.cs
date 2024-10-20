using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyGuyUnit : MonoBehaviour
{
    public Transform target;
    public GameObject pellet;
    public float pelletVelocity = 100;
    public Transform pelletOrigin;

    [Header("Stats")]
    public float health = 1;
    public float turnSpeed = 200;
    public float moveSpeed = 10;
    public float reloadTime = .5f;

    private float reloadTick;
    private Transform targetMovePosition;

    private void Awake()
    {
        reloadTick = 0f;
    }
    void Update()
    {
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
        }
    }
}
