using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public MeshRenderer dudePaper;
    public Material[] dudeMaterials;
    
    [Header("VFX")]
    public ParticleSystem deathVFX;

    // Attacking
    private float attackCooldown;

    // Moving
    private AIDestinationSetter destinationSetter;
    public Transform walkPoint;
    public bool walkPointSet;

    // States
    public WaveManager waveManager;
    public LayerMask unitLayer;

    public float sightRange, attackRange;
    public bool unitInSightRange, unitInAttackRange;

    [SerializeField] private int health = 3;

    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        Material randomMaterial = dudeMaterials[Random.Range(0, dudeMaterials.Length)];
        dudePaper.material = randomMaterial;
        attackCooldown = 0;
    }

    private void Update()
    {
        if (attackCooldown > 0) { attackCooldown -= Time.deltaTime; }

        // Check if any units are in sight range
        unitInSightRange = Physics.CheckSphere(transform.position, sightRange, unitLayer);
        unitInAttackRange = Physics.CheckSphere(transform.position, attackRange, unitLayer);

        // If no units are in range, move towards center and search for units
        if (!unitInSightRange && !unitInAttackRange) { ApproachCenter(); }

        else if (unitInSightRange && !unitInAttackRange) { ChaseUnit(); }

        else if (unitInAttackRange) { AttackUnit(); }


    }

    private void ApproachCenter()
    {
        // Move To Center
        walkPoint = GameObject.Find("MapCenter").transform;
        destinationSetter.target = walkPoint;
    }

    private void ChaseUnit()
    {
        walkPoint = FindNearestUnit().transform;
        destinationSetter.target = walkPoint;
    }

    private void AttackUnit()
    {
        if (attackCooldown <= 0)
        {
            GameObject attackTarget = FindNearestUnit();
            Debug.Log("Om Nom Nom!");
            attackTarget.gameObject.GetComponent<ArmyGuyUnit>().TakeDamage();
            // Play Attack SFX and possibly VFX
            attackCooldown = 3f;
        }
    }

    private GameObject FindNearestUnit()
    {
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag("Unit");

        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        UnityEngine.Vector3 currentPosition = transform.position;
        for (int i = 0; i < unitObjects.Length; i++)
        {
            UnityEngine.Vector3 directionToTarget = unitObjects[i].transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;

                bestTarget = unitObjects[i];
            }
        }

        return bestTarget;
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
        waveManager.removeEnemy(gameObject);
        Destroy(gameObject);
    }
}
