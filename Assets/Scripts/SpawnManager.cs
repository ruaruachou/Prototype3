using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacleArray;
    public float spawnDelay = 2;
    public float spawnInterval = 2;

    private PlayerController playerControllerScript;
    void Start()
    {
        
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("Spawn", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Spawn()
    {
        int obstacleIndex = Random.Range(0, obstacleArray.Length);
        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(obstacleArray[obstacleIndex], transform.position, obstacleArray[obstacleIndex].transform.rotation);
        }
    }
}
