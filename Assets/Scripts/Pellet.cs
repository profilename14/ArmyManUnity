using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int pelletDamage = 4;
    public float pelletLifetime = 5;

    void Start()
    {
        Destroy(gameObject, pelletLifetime);
    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;

        if (other.CompareTag("Monster"))
        {
            Debug.Log("Enemy Hit!");
            // Do Damage
            other.GetComponent<EnemyUnit>().TakeDamage(pelletDamage);
        }
    }
    
}
