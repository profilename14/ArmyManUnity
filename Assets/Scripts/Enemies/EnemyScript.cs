using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health = 3;
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
