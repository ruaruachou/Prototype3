using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //��������
    private Rigidbody playerRb;
    private Animator playerAnimator;
    //��Ծ����������
    public float JumpForce = 10;
    public float gravityModifier;
    //��¼״̬���Ƿ��ڵ��棬�Ƿ���Ϸ����
    public bool isOnGround = true;
    public bool isGameOver = false;
    //��ȡ��Ч����Ч���
    public ParticleSystem boomParticle;
    public ParticleSystem dirtParticle;
    public AudioSource playerAudioSource;
    public AudioClip jumpSFX;
    public AudioClip crashSFX;


    void Start()
    {
        //��ȡ�����������������������������
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        //��ȡ�������
        playerAnimator = GetComponent<Animator>();
        //��ȡ��Ч���
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //��Ծ�������ڵ��� �� ��Ϸ�ڽ���
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && isGameOver == false)
        {
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);//��Ծ��
            isOnGround = false;//�ı䴥��״̬
            dirtParticle.Stop();//ֹͣ����β����Ч

            playerAnimator.SetTrigger("Jump_trig");//�����ܡ���֮��Ŀ���
            playerAudioSource.PlayOneShot(jumpSFX,0.8f);//������Ծ��Ч
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //����״̬
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        //�����ϰ���״̬
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            playerAnimator.SetBool("Death_b", true);//��������״̬
            playerAnimator.SetInteger("DeathType_int", 1);//ѡ����������
            boomParticle.Play();
            dirtParticle.Stop();
            playerAudioSource.PlayOneShot(crashSFX, 1f);
        }
    }
}
