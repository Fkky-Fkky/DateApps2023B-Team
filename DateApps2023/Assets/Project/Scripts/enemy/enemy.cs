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
public class enemy : MonoBehaviour
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
    private int ex_x = 14;

    [SerializeField]
    [Tooltip("場外判定-x")]
    private int ex_mx = -14;

    [SerializeField]
    [Tooltip("場外判定z")]
    private int ex_z = 7;

    [SerializeField]
    [Tooltip("場外判定-z")]
    private int ex_mz = -7;

    bool attck_flag = false;
    int work = 0;

    [SerializeField]
    private float climbing_speed = 3;

    enum summon
    {
        start,

        climbing,

        jump,

        landing,

        end,
    }
    summon gameState = summon.start;

    float rast_timer =0;
    int rast_timer_flag=0;
    float attck_time = 0;

    bool ex_flag = false;

    float wl_time = 0;

    bool noattck = false;

    bool one_flag = false;

    Animator animator;

    private Rigidbody rb;

    NavMeshAgent _agent;

    int rnd =0;

    int x;
    int z;

    void Start()
    {
        gameState = summon.start;

        myCollider = this.GetComponent<CapsuleCollider>();
        myCollider.enabled = true;

        //口の当たり判定の設定
        AttackCollider = gameObject.GetComponentInChildren<BoxCollider>();
        AttackCollider.enabled = false;

        //アニメーター
        animator = GetComponent<Animator>();

        animator.SetTrigger("idle");

        //プレイヤーのランダム変数
        //rnd = Random.Range(0, 3);
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

        if (gameState == summon.start)
        {
            rb.constraints = RigidbodyConstraints.None;
            work = 1;
            attck_flag = false;
            gameState = summon.climbing;
            myTransform.Rotate(-90, 0f, 0f);
        }
        //y8.5
        else if(gameState == summon.climbing)
        {
            transform.position += new Vector3(0, climbing_speed, 0) * Time.deltaTime;
            //climbing_speed
            if (pos.y >= -1.2)
            {
                if (pos.x>0)
                {
                    rb.useGravity = true;
                    Vector3 force = new Vector3(-2.0f, 15.0f, 0.0f);
                    rb.AddForce(force, ForceMode.Impulse);
                }

                else if(pos.x<0)
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
           if(pos.y>=5)
           {           
                StartCoroutine(Onturn());
                gameState = summon.landing;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        else if(gameState == summon.landing)
        {
            //着地
            if(pos.y<=0)
            {
                //rb.useGravity = false;
                work = 0;
                _agent.enabled = true;
                AttackCollider.enabled = false;
                gameState = summon.end;
                
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


        if (attck_flag == true)
        {
            attck_time += Time.deltaTime;

            if (AttackCollider.enabled == true)
            {
                if (attck_time >= 1.2)
                {
                    rast_timer_flag = 1;
                    attck_flag = false;
                    noattck = true;
                    AttackCollider.enabled = false;
                    attck_time = 0;
                }
            }
            else
            {
                if (attck_time >= 0.9)
                    AttackCollider.enabled = true;
                else if (attck_time >= 0.5)
                {
                    animator.SetTrigger("attck");
                }
            }
        }

        if (rast_timer_flag == 1)
        {
            rast_timer += Time.deltaTime;
        }
      //退場
        if (rast_timer >= 1)
        {
            //AttackCollider.enabled = false;
            
            if(one_flag==false)
            {
                animator.SetTrigger("idle");
                _agent.enabled = true;
                one_flag = true;
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
        //ex_flag = true;

        if (-25 >= pos.y )
        {
            //attck_flag = false;
            Destroy(gameObject);
        }

        #region ノックバック

        //ノックバック時に場外に行かなかった時の処理
        if (_agent.enabled == false　&&　gameState == summon.end)
        {
            wl_time+=Time.deltaTime;

            if(ex_flag == true)
            {
                rb.useGravity = false;
                myCollider.enabled = false;
            }
            else if(wl_time >= 1.5f&&ex_flag == false)
            {
                _agent.enabled = true;
               wl_time = 0;
            }
        }

        Vector3 rangeApos = spderpositionA.position;
        Vector3 rangeDpos = spderpositionD.position;
        if (rangeApos.x <= pos.x && gameState == summon.end ||
            rangeApos.x <= pos.z && gameState == summon.end)
            rb.useGravity = true;

        if (rangeDpos.x >= pos.x && gameState == summon.end ||
            rangeDpos.x >= pos.z && gameState == summon.end)
            rb.useGravity = true;

        if (pos.x >= ex_x && gameState == summon.end||
            pos.z >= ex_z && gameState == summon.end)
            ex_flag = true;

        if (pos.x <= ex_mx && gameState == summon.end ||
            pos.z <= ex_mz && gameState == summon.end )
            ex_flag = true;
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

    //壁との判定
    private void OnTriggerEnter(Collider other)
    {
        
    }

    //プレイヤーとの判定
    void OnCollisionEnter(Collision collision)//Trigger
    {
        if (collision.gameObject == players[rnd]&&noattck==false)
        {
            work = 1;
            attck_flag = true;
            attck_time = 0;
            noattck = true;
            one_flag = false;
            animator.SetTrigger("attckidle");
        }

        if (collision.gameObject.CompareTag("Wall") &&
            gameState == summon.end &&
            noattck == true)
        {
            
            myCollider.enabled=false;
            Debug.Log("Wall");
            _agent.enabled = false;
            StartCoroutine(Onex());
        }
    }

    public void OnattackCollider()
    {
        PlayerDamage[rnd].CallDamage();
    }
}