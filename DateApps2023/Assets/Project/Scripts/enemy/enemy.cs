using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


//[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    //攻撃の当たり判定
    private Collider AttackCollider;

    private Collider myCollider;

    [SerializeField] private GameObject[] players;

    private GameObject oriznal;

    [SerializeField] private PlayerDamage[] PlayerDamage;

    [SerializeField] private Transform Centerpoint;

    [SerializeField] private Transform rirurnTransform;

    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Transform spderpositionA;
    
    [SerializeField]
    [Tooltip("生成する範囲D")]
    private Transform spderpositionD;


    [SerializeField]
    [Tooltip("場外判定x")]
    private int ExX = 14;

    [SerializeField]
    [Tooltip("場外判定-x")]
    private int ExMx = -14;

    [SerializeField]
    [Tooltip("場外判定z")]
    private int ExZ = 7;

    [SerializeField]
    [Tooltip("場外判定-z")]
    private int ExMz = -7;

    bool AttckFlag = false;
    int work = 0;

    [SerializeField]
    private float ClimbingSpeed = 3;

    enum SUMMON
    {
        start,

        climbing,

        jump,

        landing,

        end,
    }
    SUMMON gameState = SUMMON.start;

    float RastTimer =0;
    int RastTimerFlag=0;
    float AttckTime = 0;

    bool ExFlag = false;

    float WlTime = 0;

    bool noattck = false;

    bool OneFlag = false;

    Animator animator;

    private Rigidbody rb;

    NavMeshAgent _agent;

    public int rnd;

    void Start()
    {
        gameState = SUMMON.start;

        myCollider = this.GetComponent<CapsuleCollider>();
        myCollider.enabled = true;

        //口の当たり判定の設定
        AttackCollider = gameObject.GetComponentInChildren<BoxCollider>();
        AttackCollider.enabled = false;

        //アニメーター
        animator = GetComponent<Animator>();

        animator.SetTrigger("idle");

        //プレイヤーのランダム変数
        rnd = Random.Range(0, 3);
        //Navを取得
       _agent = this.GetComponent<NavMeshAgent>();
        //NavMeshAgent nav = this. GetComponent<NavMeshAgent>();
        _agent.enabled = false;

        Vector3 vec = Centerpoint.transform.position - this.transform.position;

        vec.y = 0f;
        Quaternion quaternion = Quaternion.LookRotation(vec);
        this.transform.rotation = quaternion;

        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;

        oriznal = GameObject.Find("Spider");

    }
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        Quaternion rot = transform.rotation;

        #region 出現

        if (gameState == SUMMON.start)
        {
            rb.constraints = RigidbodyConstraints.None;
            work = 1;
            AttckFlag = false;
            gameState = SUMMON.climbing;
            myTransform.Rotate(-90, 0f, 0f);
        }
        //y8.5
        else if(gameState == SUMMON.climbing)
        {
            transform.position += new Vector3(0, ClimbingSpeed, 0) * Time.deltaTime;
            //climbing_speed
            if (pos.y >= -1.2)
            {
                if (pos.x>0)
                {
                    rb.useGravity = true;
                    Vector3 force = new Vector3(-1.0f, 15.0f, 0.0f);
                    rb.AddForce(force, ForceMode.Impulse);
                }

                else if(pos.x<0)
                {
                    rb.useGravity = true;
                    Vector3 force = new Vector3(1.0f, 15.0f, 0.0f);
                    rb.AddForce(force, ForceMode.Impulse);
                }
                gameState = SUMMON.jump;
            }
        }

        //空中での回転
         else if (gameState == SUMMON.jump)
        {
           if(pos.y>=5)
           {           
                StartCoroutine(Onturn());
                gameState = SUMMON.landing;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        else if(gameState == SUMMON.landing)
        {
            //着地
            if(pos.y<=0)
            {
                //rb.useGravity = false;
                work = 0;
                _agent.enabled = true;
                AttackCollider.enabled = false;
                gameState = SUMMON.end;
                
            }
        }
        #endregion


        #region 攻撃開始

        if (work == 0&& _agent.enabled == true)
        {
            _agent.destination = players[rnd].transform.position;
        }



        //if (attck_time >= 0.75 && attck_flag == true)
        //{
        //    work = 1;
        //}


        if (AttckFlag == true)
        {
            AttckTime += Time.deltaTime;

            if (AttackCollider.enabled == true)
            {
                if (AttckTime >= 1.2)
                {
                    RastTimerFlag = 1;
                    AttckFlag = false;
                    noattck = true;
                    AttackCollider.enabled = false;
                    AttckTime = 0;
                }
            }
            else
            {
                if (AttckTime >= 0.9)
                    AttackCollider.enabled = true;
                else if (AttckTime >= 0.5)
                {
                    animator.SetTrigger("attck");
                }
            }
        }

        if (RastTimerFlag == 1)
        {
            RastTimer += Time.deltaTime;
        }
      //退場
        if (RastTimer >= 1)
        {
            //AttackCollider.enabled = false;
            
            if(OneFlag==false)
            {
                animator.SetTrigger("idle");
                _agent.enabled = true;
                OneFlag = true;
            }

            if(_agent.enabled == true)
            _agent.destination = rirurnTransform.transform.position;
           
            if (pos.z<=-120)
            {
                //ex_flag = false;
                Destroy(gameObject);
            }
        }
       
        #endregion

        if (-25 >= pos.y )
        {
            Destroy(gameObject);
        }
        #region ノックバック

        //ノックバック時に場外に行かなかった時の処理
        if (_agent.enabled == false　&&　gameState == SUMMON.end)
        {
            WlTime+=Time.deltaTime;

            if(ExFlag == true)
            {
                rb.useGravity = false;
                myCollider.enabled = false;
            }
            else if(WlTime >= 1.5f&&ExFlag == false)
            {
                _agent.enabled = true;
               WlTime = 0;
            }
        }

        Vector3 rangeApos = spderpositionA.position;
        Vector3 rangeDpos = spderpositionD.position;

        if(gameState == SUMMON.end)
        {
        
        if (rangeApos.x <= pos.x  ||
            rangeApos.x <= pos.z)
            rb.useGravity = true;

        if (rangeDpos.x >= pos.x ||
            rangeDpos.x >= pos.z)
            rb.useGravity = true;

        if (pos.x >= ExX ||
            pos.z >= ExZ )
            ExFlag = true;

        else if (pos.x <= ExMx ||
                  pos.z <= ExMz)
            ExFlag = true;
        else
            ExFlag = false;

        }
        #endregion

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

    //帰宅
    IEnumerator Onex()
    {
       
        for (int i = 1; i < 9; i += 1)
        {
            this.transform.position += new Vector3(0, 0, -0.5f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    //プレイヤーとの判定
    void OnCollisionEnter(Collision collision)//Trigger
    {
        if (collision.gameObject == players[rnd]&&noattck==false)
        {
            work = 1;
            AttckFlag = true;
            AttckTime = 0;
            noattck = true;
            OneFlag = false;
            animator.SetTrigger("attckidle");
        }

        if (collision.gameObject.CompareTag("Wall") &&
            gameState == SUMMON.end &&
            noattck == true)
        {
            myCollider.enabled=false;
            Debug.Log("Wall");
            _agent.enabled = false;
            StartCoroutine(Onex());
        }

        if (collision.gameObject.CompareTag("thin wall") &&
            gameState == SUMMON.landing)    
        {
            Vector3 force = new Vector3(0.0f, 0.0f, 3.0f);
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    public void OnattackCollider()
    {
        PlayerDamage[rnd].CallDamage();
    }
}