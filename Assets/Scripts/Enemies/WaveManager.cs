using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> enemiesRemaining;

    public List<EnemyWave> waves;

    public List<Transform> spawnPoints;

    private AudioManager theAM;
    private AudioSource speaker;

    public AudioClip waveMusic;
    public AudioClip buildMusic;
    public AudioClip waveStart;

    private float spawnTimer = 0;
    private int numEnemiesSpawnedThisWave = 0;

    public bool isBuildMode = true;
    [SerializeField] private PickupController pickupController;
    // Start is called before the first frame update
    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
        pickupController.canCarryObjects = true;
        speaker = GetComponent<AudioSource>();

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
            theAM.ChangeBGM(buildMusic);
        }
    }

    void beginNextWave()
    {
        speaker.PlayOneShot(waveStart);

        //play the wave music
        if (waveMusic != null)
        {
            theAM.ChangeBGM(waveMusic);
            numEnemiesSpawnedThisWave = 0;
        }
    }
    public void removeEnemy(GameObject oldEnemy)
    {
        enemiesRemaining.Remove(oldEnemy);
    }
  }
