using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSUnitTest : MonoBehaviour
{
    public Transform target;
    private float reloadTime = 5f;
    private float reloadTick = 0f;

    void Update()
    {
        foreach (EnemyScript e in FindObjectsOfType<EnemyScript>())
        {
            if (Vector3.Distance(e.transform.position, transform.position) < 20f)
            {
                // An enemy is in your radius
                //e.GetHurt(); // Hit the enemy for example
                target = e.transform;
            }
        }

        reloadTick += Time.deltaTime;

        if (reloadTick >= reloadTime)
        {
            reloadTick = 0f;
            // Fire Bullet
        }
    }
}
