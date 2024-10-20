using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] private int health = 3;

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
            // Die
            Destroy(gameObject);
        }
    }

}
