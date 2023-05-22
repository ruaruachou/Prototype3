using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovaLeft : MonoBehaviour
{
    public float moveSpeed=20;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript=GetComponent<PlayerController>();    
    }


    void Update()
    {

        {
            if (playerControllerScript.isGameOver == false)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
        }
    }
}
