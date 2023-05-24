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
    public int jumpIndex = 0;
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

     
        StartCoroutine(ChangeAnimatorState());
    }

    void Update()
    {
        
        //��Ծ�������ڵ��� �� ��Ϸ�ڽ���
        if (Input.GetKeyDown(KeyCode.Space) && isGameOver == false&&jumpIndex<2)
        {
            jumpIndex += 1;
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);//��Ծ��
            isOnGround = false;//�ı䴥��״̬
            dirtParticle.Stop();//ֹͣ����β����Ч

            playerAnimator.SetTrigger("Jump_trig");//�����ܡ���֮��Ŀ���
            playerAudioSource.PlayOneShot(jumpSFX,0.8f);//������Ծ��Ч
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerAnimator.SetFloat("Speed_f", 0.6f);

        //����״̬
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpIndex = 0;
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

    private IEnumerator ChangeAnimatorState()
    {
        playerAnimator.SetFloat("Speed_f", 0.3f);//��ʼʱΪ0.3
        yield return new WaitForSeconds(2f);//����5��

        playerAnimator.SetFloat("Speed_f", 1f);//�����Ϊ1
        yield break;
    }
}
