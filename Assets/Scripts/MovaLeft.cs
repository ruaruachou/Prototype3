using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovaLeft : MonoBehaviour
{
    public float normalMoveSpeed = 20;

    private PlayerController playerControllerScript;
    private Animator playerAnimator;
    private float leftBound = -15;

    public float score = 0;


    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
    }


    void Update()
    {

        SpeedUp();
        ScoreRecord();
        {
            if (playerControllerScript.isGameOver == false)
            {
                transform.Translate(Vector3.left * normalMoveSpeed * Time.deltaTime);
            }
            if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
    void SpeedUp()
    {
        if (Input.GetMouseButton(0))
        {
            normalMoveSpeed = 40;
            playerAnimator.speed = 3;
        }
        if (Input.GetMouseButtonUp(0))
        {
            normalMoveSpeed = 20;
            playerAnimator.speed = 1;    
        }
    }

    void ScoreRecord()
    {

        score += normalMoveSpeed * Time.deltaTime;
        //Debug.Log(score);
    }
}
