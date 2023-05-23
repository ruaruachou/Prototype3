using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //声明引用
    private Rigidbody playerRb;
    private Animator playerAnimator;
    //跳跃和重力参数
    public float JumpForce = 10;
    public float gravityModifier;
    //记录状态：是否在地面，是否游戏结束
    public bool isOnGround = true;
    public bool isGameOver = false;
    //获取动效和音效组件
    public ParticleSystem boomParticle;
    public ParticleSystem dirtParticle;
    public AudioSource playerAudioSource;
    public AudioClip jumpSFX;
    public AudioClip crashSFX;


    void Start()
    {
        //获取刚体组件，根据重力参数调节重力
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        //获取动画组件
        playerAnimator = GetComponent<Animator>();
        //获取音效组件
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //跳跃条件：在地面 且 游戏在进行
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && isGameOver == false)
        {
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);//跳跃力
            isOnGround = false;//改变触地状态
            dirtParticle.Stop();//停止播放尾迹动效

            playerAnimator.SetTrigger("Jump_trig");//触发跑》跳之间的开关
            playerAudioSource.PlayOneShot(jumpSFX,0.8f);//播放跳跃音效
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //地面状态
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        //碰到障碍的状态
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            playerAnimator.SetBool("Death_b", true);//进入死亡状态
            playerAnimator.SetInteger("DeathType_int", 1);//选择死亡动画
            boomParticle.Play();
            dirtParticle.Stop();
            playerAudioSource.PlayOneShot(crashSFX, 1f);
        }
    }
}
