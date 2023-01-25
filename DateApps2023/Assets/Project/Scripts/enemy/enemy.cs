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

    //[SerializeField] 
    private GameObject[] players;

    [SerializeField] private PlayerDamage[] PlayerDamage;

    [SerializeField] private Transform Centerpoint;

    [SerializeField] private Transform rirurnTransform;

    

    [SerializeField]
    [Tooltip("場外判定x")]
    private int ex_x = 17;

    [SerializeField]
    [Tooltip("場外判定-x")]
    private int ex_mx = -17;

    [SerializeField]
    [Tooltip("場外判定z")]
    private int ex_z = 10;

    [SerializeField]
    [Tooltip("場外判定-z")]
    private int ex_mz = -10;

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

        end,
    }
    summon gameState = summon.start;

    float rast_timer =0;
    int rast_timer_flag=0;
    float attck_time = 0;

    bool ex_flag = false;

    float wl_time = 0;

    bool noattck = false;

    Animator animator;

    private Rigidbody rb;

    NavMeshAgent _agent;

    int rnd;

    int x;
    int z;

    void Start()
    {
        //players[0] = GameObject.Find("player1");

        gameState = summon.start;

        //口の当たり判定の設定
        AttackCollider = GameObject.Find("RigHeadGizmo").GetComponent<BoxCollider>();
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
            move = 4;
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
                work = 0;
                _agent.enabled = true;
                gameState = summon.end;
            }
        }
        #endregion


        #region 攻撃開始

        if (work == 0)
        {
            Debug.Log("選ばれたプレイヤー");
            Debug.Log(rnd);
            // agent.destination = target.transform.position;
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

        if (attck_time >= 0.9 && move == 0&&noattck!=true)
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
      //退場
        if (rast_timer>=2)
        {
            AttackCollider.enabled = false;
            noattck = true;
            animator.SetTrigger("idle");
           _agent.destination = rirurnTransform.transform.position;

            //_agent.enabled = false;

            if (pos.z<=-120)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        if (pos.y <= -10)
            Destroy(gameObject);

        //ノックバック時に場外に行かなかった時の処理
        if (_agent.enabled == false　&&　gameState == summon.end)
        {
            wl_time+=Time.deltaTime;
            if(wl_time >= 1.5f&&ex_flag == false)
            {
               _agent.enabled = true;
               wl_time = 0;
            }
        }


        if (pos.x >= ex_x && gameState == summon.end||
            pos.z >= ex_z && gameState == summon.end)
            ex_flag = true;

        if (pos.x <= ex_mx && gameState == summon.end ||
            pos.z <= ex_mz && gameState == summon.end )
            ex_flag = true;

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

    IEnumerator Onex()
    {
       
        for (int i = 1; i < 9; i += 1)
        {
            this.transform.position += new Vector3(0, 0, -0.5f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall")&&
            gameState == summon.end&&
            noattck==true)
        {
            Debug.Log("Wall");
            _agent.enabled = false;
            StartCoroutine(Onex());
            
        }
    }

    void OnCollisionEnter(Collision collision)//Trigger
    {
        if (collision.gameObject == players[rnd])
        {
            work = 1;
            move = 1;
            attck_time = 0;
            rast_timer_flag = 1;
            animator.SetTrigger("attckidle");
        }
    }

    public void OnattackCollider()
    {
        PlayerDamage[rnd].CallDamage();
    }
}