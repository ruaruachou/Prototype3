using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovaLeft : MonoBehaviour
{
    public float moveSpeed=20;

    private PlayerController playerControllerScript;
    private float leftBound = -15;


    void Start()
    {
        playerControllerScript=GameObject.Find("Player").GetComponent<PlayerController>();    
    }


    void Update()
    {

        {
            if (playerControllerScript.isGameOver == false)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}
