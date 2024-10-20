using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public MeshRenderer dudePaper;
    public Material[] dudeMaterials;
    
    [Header("VFX")]
    public ParticleSystem deathVFX;

    [SerializeField] private int health = 3;

    private void Awake()
    {
        Material randomMaterial = dudeMaterials[Random.Range(0, dudeMaterials.Length)];
        dudePaper.material = randomMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            Debug.Log("Om Nom Nom!");
            other.gameObject.GetComponent<ArmyGuyUnit>().TakeDamage();
        }
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
