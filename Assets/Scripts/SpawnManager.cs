using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstacle;
    public float spawnDelay = 2;
    public float spawnInterval = 2;
    void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Spawn()
    {
        Instantiate(obstacle, transform.position, obstacle.transform.rotation);
    }
}
