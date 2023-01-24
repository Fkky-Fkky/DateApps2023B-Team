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

    [SerializeField]
    private PlayerDamage []PlayerDamage;

    //攻撃の当たり判定
    private Collider AttackCollider;

    [SerializeField] private GameObject[] playerTransform; 

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

        end,
    }
    summon gameState = summon.start;

    float rast_timer =0;
    int rast_timer_flag=0;
    float attck_time = 0;

    float wl_time = 0;

    bool noattck = false;

    //BoxCollider boxCol;

    Animator animator;

    //int attck = Animator.StringToHash("attck");
    //int idle = Animator.StringToHash("idle");
    //int attck_idle = Animator.StringToHash("attck_idle");

    private Rigidbody rb;

    NavMeshAgent _agent;

    int rnd = 0;

    int x;
    int z;

    //public CapsuleCollider cube_boxCol;

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
        //rnd = Random.Range(0, 3);
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

        if (pos.y <= -40)
            Destroy(gameObject);

        

        //ノックバック時の壁に当たった時の処理
        if (_agent.enabled == false　&&　gameState == summon.end)
        {
            wl_time+=Time.deltaTime;
            if(wl_time>=3.0f)
            {
               _agent.enabled = true;
               wl_time = 0;
            }
        }
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
        if (collision.gameObject == playerTransform[rnd])
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