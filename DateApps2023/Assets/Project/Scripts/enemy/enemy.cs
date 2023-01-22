using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;


//[RequireComponent(typeof(NavMeshAgent))]
public class enemy : MonoBehaviour
{
    //CallDamage�Ăяo���p
    public GameObject[] players;

    [SerializeField]
    private PlayerDamage []PlayerDamage;

    //�U���̓����蔻��
    private Collider AttackCollider;

    [SerializeField] private Transform[] playerTransform;

    [SerializeField] private Transform rirurnTransform;
    int move = 4;
    int work = 0;
    float rast_timer =0;
    int rast_timer_flag=0;
    float attck_time = 0;

    //BoxCollider boxCol;

    Animator animator;

    //int attck = Animator.StringToHash("attck");
    //int idle = Animator.StringToHash("idle");
    //int attck_idle = Animator.StringToHash("attck_idle");
    
    // �G�[�W�F���g���L���b�V�����Ă����p
    private NavMeshAgent _agent;

    public int rnd;

    void Start()
    {
        //���̓����蔻��̐ݒ�
        AttackCollider = GameObject.Find("RigHeadGizmo").GetComponent<BoxCollider>();
        AttackCollider.enabled = false;

        //�A�j���[�^�[
        animator = GetComponent<Animator>();

        animator.SetTrigger("idle");

        //�v���C���[�̃����_���ϐ�
        rnd = Random.Range(0, 3);
        //Nav���擾
       _agent = GetComponent<NavMeshAgent>();
     
    }
    void Update()
    {
        Transform myTransform = this.transform;

        //if (move == 4)
        //{
        //    Vector3 pos = myTransform.position;
        //    pos.y += 0.1f;
        //    if (pos.y >= 55)
        //    {
        //        move = 0;
        //    }
        //}

        if (work == 0)
        {
            _agent.destination = players[rnd].transform.position;
        }

        attck_time += Time.deltaTime;

        if (attck_time >= 0.5&&move==1)
        {
            animator.SetTrigger("attck");
        }

        if (attck_time >= 0.75 && move == 1)
        {
            move = 0;
            work = 1;
        }

        if (attck_time >= 0.9 && move == 0)
        {
            AttackCollider.enabled = true;
        }
        if (attck_time >= 1.2 && move == 0)
        {

            AttackCollider.enabled = false;
        }

        if (rast_timer_flag == 1)
        {
            rast_timer += Time.deltaTime;
        }

        if(rast_timer>=2)
        {
            Vector3 pos = myTransform.position;
            animator.SetTrigger("idle");
            _agent.destination = rirurnTransform.transform.position;
            if(pos.z<=-120)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)//Trigger
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            work = 1;
            move = 1;
            attck_time = 0;
            rast_timer_flag = 1;
            animator.SetTrigger("attckidle");
        }

      
    }

    //public void OnattackCollider()
    //{
    //    PlayerDamage[rnd].CallDamage();
    //}
}