using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public MeshRenderer dudePaper;
    public Material[] dudeMaterials;
    
    [Header("VFX")]
    public ParticleSystem deathVFX;

    // Moving
    private AIDestinationSetter destinationSetter;
    public Transform walkPoint;
    public bool walkPointSet;

    // States
    public GameObject[] unitObjects;
    public LayerMask soldierLayer;

    public float sightRange, attackRange;
    public bool unitInSightRange, unitInAttackRange;

    [SerializeField] private int health = 3;

    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        Material randomMaterial = dudeMaterials[Random.Range(0, dudeMaterials.Length)];
        dudePaper.material = randomMaterial;
    }

    private void Update()
    {
        // Check if any units are in sight range
        unitInSightRange = Physics.CheckSphere(transform.position, sightRange, soldierLayer);
        unitInAttackRange = Physics.CheckSphere(transform.position, attackRange, soldierLayer);

        // If no units are in range, move towards center and search for units
        if (!unitInSightRange && !unitInAttackRange) { ApproachCenter(); }

        if (unitInSightRange && !unitInAttackRange) { ChaseUnit(); }

        if (unitInAttackRange) { AttackUnit(); }

        walkPoint = FindNearestUnit();
        destinationSetter.target = walkPoint;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            Debug.Log("Om Nom Nom!");
            other.gameObject.GetComponent<ArmyGuyUnit>().TakeDamage();
        }
    }

    private void ApproachCenter()
    {

    }

    private Transform FindNearestUnit()
    {
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag("Unit");

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < unitObjects.Length; i++)
        {
            Vector3 directionToTarget = unitObjects[i].transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = unitObjects[i].transform;
            }
        }

        return bestTarget;
    }


    private void ChaseUnit()
    {

    }

    private void AttackUnit()
    {

    }


    public void TakeDamage(int amount = 1)
    {
        health -= amount;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Instantiate(deathVFX, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
