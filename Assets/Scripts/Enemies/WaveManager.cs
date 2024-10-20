using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> enemiesRemaining;

    public List<EnemyWave> waves;

    public List<Transform> spawnPoints;

    private float spawnTimer = 0;
    private int numEnemiesSpawnedThisWave = 0;

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
        if (Input.GetKeyDown(KeyCode.Space) && isBuildMode)
        {
            isBuildMode = false;
            pickupController.canCarryObjects = false;
            beginNextWave();
        }
       
        if (!isBuildMode)
        {
            if (spawnTimer > 0)
            {
                spawnTimer -= Time.deltaTime;
            }
            else
            {
                if (numEnemiesSpawnedThisWave < waves[0].numberOfEnemies)
                {
                    spawnTimer = waves[0].spawnCooldown;

                    int spawnIndex = Random.Range(0, spawnPoints.Count);
                    Transform SpawnTransform = spawnPoints[spawnIndex];
                    Vector3 SpawnPosition = SpawnTransform.position;
                    float rndX = Random.Range(-2, 2);
                    float rndZ = Random.Range(-2, 2);  
                    SpawnPosition.x += rndX;
                    SpawnPosition.z += rndZ;

                    GameObject newEnemy = Instantiate(waves[0].enemyType, SpawnPosition, Quaternion.identity);
                    enemiesRemaining.Add(newEnemy);
                    numEnemiesSpawnedThisWave++;
                }
            }
        }
        
        if (!isBuildMode && enemiesRemaining.Count < 1)
        {
            waves.RemoveAt(0);
            isBuildMode = true;
            pickupController.canCarryObjects = true;

        }
    }

    void beginNextWave()
    {
        numEnemiesSpawnedThisWave = 0;


        
        
    }
}
