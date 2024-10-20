using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;

        if (other.CompareTag("Monster"))
        {
            Debug.Log("Enemy Hit!");
            // Do Damage
            other.GetComponent<EnemyUnit>().TakeDamage(4);
        }
    }
}
