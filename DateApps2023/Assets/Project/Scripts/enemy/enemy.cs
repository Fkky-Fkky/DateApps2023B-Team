using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


//[RequireComponent(typeof(NavMeshAgent))]
public class enemy : MonoBehaviour
{

    [SerializeField]
    private PlayerDamage []PlayerDamage;

    //攻撃の当たり判定
    private Collider AttackCollider;

    [SerializeField] private Transform[] playerTransform; 

    [SerializeField] private Transform Centerpoint;

    [SerializeField] private Transform rirurnTransform;
    int move = 4;
    int work = 0;

    [SerializeField]
    private float climbing_speed = 3;

    enum summon
    {
        start,

        climbing,

        jump,

        landing,
    }
    summon gameState = summon.start;

    float rast_timer =0;
    int rast_timer_flag=0;
    float attck_time = 0;

    //BoxCollider boxCol;

    Animator animator;

    //int attck = Animator.StringToHash("attck");
    //int idle = Animator.StringToHash("idle");
    //int attck_idle = Animator.StringToHash("attck_idle");

    private Rigidbody rb;

    private NavMeshAgent _agent;

    int rnd;

    public CapsuleCollider cube_boxCol;

    void Start()
    {
        gameState = summon.start;

        //口の当たり判定の設定
        AttackCollider = GameObject.Find("RigHeadGizmo").GetComponent<BoxCollider>();
        AttackCollider.enabled = false;

        //cube_boxCol = cube.GetComponent<CapsuleCollider>();

        //アニメーター
        animator = GetComponent<Animator>();

        animator.SetTrigger("idle");

        //プレイヤーのランダム変数
        rnd = Random.Range(0, 3);
        //Navを取得
       _agent = GetComponent<NavMeshAgent>();
        GetComponent<NavMeshAgent>().enabled = false;

        Vector3 vector3 = Centerpoint.transform.position - this.transform.position;
        vector3.y = 0f;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        this.transform.rotation = quaternion;

        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        Quaternion rot = transform.rotation;
        #region 出現

        if (gameState == summon.start)
        {
            work = 1;
            move = 1;
            gameState = summon.climbing;
            myTransform.Rotate(270, 0f, 0f);
        }
        //y8.5
        else if(gameState == summon.climbing)
        {
            transform.position += new Vector3(0, 3.5f, 0) * Time.deltaTime;
            //climbing_speed
            if (pos.y >= -1.2)
            {

                if(pos.x>0)
                {
                    rb.useGravity = true;
                    Vector3 force = new Vector3(-2.0f, 15.0f, 0.0f);
                    rb.AddForce(force, ForceMode.Impulse);
                }

                else if(pos.x<=0)
                {
                    rb.useGravity = true;
                    Vector3 force = new Vector3(2.0f, 15.0f, 0.0f);
                    rb.AddForce(force, ForceMode.Impulse);
                }
                gameState = summon.jump;
            }
        }

        //空中での回転
         else if (gameState == summon.jump)
        {
            StartCoroutine(Onturn());
            if (rot.x >= 0)
            { 
               gameState = summon.landing;
               rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        else if(gameState==summon.landing)
        {
            if(pos.y<=0)
            {
                //GetComponent<NavMeshAgent>().enabled = true;
            }
        }
        #endregion


        #region 攻撃開始

        if (work == 0)
        {
           _agent.destination = playerTransform[rnd].transform.position;
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
            
            animator.SetTrigger("idle");
            _agent.destination = rirurnTransform.transform.position;
            if(pos.z<=-120)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        if(pos.y>=17)
            Destroy(gameObject);
        
    }

    //空中の回転
    IEnumerator Onturn()
    {
        Transform myTransform = this.transform;

        yield return new WaitForSeconds(1);
        for (int i = 1; i < 9; i += 1)
        {
            myTransform.Rotate(new Vector3(10, 0, 0));
            yield return new WaitForSeconds(0.05f);
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

        if (collision.gameObject.CompareTag("PlayerAttackPoint"))
        {
            cube_boxCol.isTrigger = true;
        }
    }

   

    public void OnattackCollider()
    {
        PlayerDamage[rnd].CallDamage();
    }
}