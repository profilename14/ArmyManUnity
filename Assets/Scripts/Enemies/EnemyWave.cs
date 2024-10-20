using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyType; // This prefab will be spawned as the enemy type of this wave.
    //public float waveDelay = 10; // How many seconds after the last wave this should start.
    public float spawnCooldown; // How many seconds should the manager wait between spawning an enemy;
    public int numberOfEnemies; // How many enemies should be spawned.
    public List<GameObject> rewards; // enable these at the end of the wave
}
