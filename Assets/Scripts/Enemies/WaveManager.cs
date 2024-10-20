using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> enemiesRemaining;

    public List<EnemyWave> waves;

    public bool isBuildMode = true;
    [SerializeField] private PickupController pickupController;
    // Start is called before the first frame update
    void Start()
    {
        pickupController.canCarryObjects = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon) && isBuildMode)
        {
            isBuildMode = false;
            pickupController.canCarryObjects = false;
            beginNextWave();
        }
        
        if (!isBuildMode && enemiesRemaining.Count < 1) {
            isBuildMode = true;
            pickupController.canCarryObjects = true;

        }
    }

    void beginNextWave()
    {
        



        waves.RemoveAt(0);
    }
}
