using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectScript : MonoBehaviour
{
    public float time = 5;

    [Header("Sounds")]
    public AudioSource speaker;
    public AudioClip[] deathSFX;

    void Start()
    {
        speaker = GetComponent<AudioSource>();
        Destroy(gameObject, time);
        
        AudioClip randomClip = deathSFX[Random.Range(0, deathSFX.Length)];
        speaker.PlayOneShot(randomClip);
    }
}
