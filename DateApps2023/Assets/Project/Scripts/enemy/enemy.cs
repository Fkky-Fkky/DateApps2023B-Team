using System.Collections;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject[] players = null;

    [SerializeField] private PlayerDamage[] playerDamage = null;

    [SerializeField] private Transform centerPoint = null;

    [SerializeField] private Transform rirurnTransform = null;

    [SerializeField]
    private float ClimbingSpeed = 3;

    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Transform spderpositionA = null;

    [SerializeField]
    [Tooltip("生成する範囲D")]
    private Transform spderpositionD = null;

    //攻撃の当たり判定
    private Collider AttackCollider = null;

    private Collider myCollider = null;

    private Animator animator = null;

    private Rigidbody rb = null;

    private NavMeshAgent _agent = null;

    int work = 0;

    enum SUMMON
    {
        START,

        CLIMBIN,

        JUMP,

        LAMDING,

        END,
    }
    SUMMON gameState = SUMMON.START;

    float rastTimer =0;

    float wlTime = 0;

    float attackTime = 0;

    bool rastTimerFlag = false;

    bool attackFlag = false;

    bool exFlag = false;

    bool noAttack = false;

    bool oneFlag = false;

    [SerializeField]
    [Tooltip("場外判定x")]
    private int exitX = 14;

    [SerializeField]
    [Tooltip("場外判定-x")]
    private int exitMinx = -14;

    [SerializeField]
    [Tooltip("場外判定z")]
    private int exitZ = 7;

    [SerializeField]
    [Tooltip("場外判定-z")]
    private int exitMinz = -7;

    public int rnd;

    int destroyPosition = -25;

    void Start()
    {
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
       _agent.enabled = false;

        Vector3 vec = centerPoint.transform.position - this.transform.position;

        vec.y = 0f;
        Quaternion quaternion = Quaternion.LookRotation(vec);
        this.transform.rotation = quaternion;

        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;

    }
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        Quaternion rot = transform.rotation;

        //出現
        Moobing(myTransform, pos);

        //小型エネミーの攻撃
        EnemyAttack(pos);

        if (destroyPosition >= pos.y)
        {
            Destroy(gameObject);
        }
        KnockBack(pos);
    }

    void Moobing(Transform myTransform, Vector3 pos)
    {
        if (gameState == SUMMON.START)
        {
            rb.constraints = RigidbodyConstraints.None;
            work = 1;
            attackFlag = false;
            gameState = SUMMON.CLIMBIN;
            myTransform.Rotate(-90, 0f, 0f);
        }
        //y8.5
        else if (gameState == SUMMON.CLIMBIN)
        {
            transform.position += new Vector3(0, ClimbingSpeed, 0) * Time.deltaTime;
            //climbing_speed
            if (pos.y >= -1.2)
            {
                if (pos.x > 0)
                {
                    rb.useGravity = true;
                    Vector3 force = new Vector3(-1.0f, 15.0f, 0.0f);
                    rb.AddForce(force, ForceMode.Impulse);
                }

                else if (pos.x < 0)
                {
                    rb.useGravity = true;
                    Vector3 force = new Vector3(1.0f, 15.0f, 0.0f);
                    rb.AddForce(force, ForceMode.Impulse);
                }
                gameState = SUMMON.JUMP;
            }
        }

        //空中での回転
        else if (gameState == SUMMON.JUMP)
        {
            if (pos.y >= 5)
            {
                StartCoroutine(Onturn());
                gameState = SUMMON.LAMDING;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        else if (gameState == SUMMON.LAMDING)
        {
            //着地
            if (pos.y <= 0)
            {
                work = 0;
                _agent.enabled = true;
                AttackCollider.enabled = false;
                gameState = SUMMON.END;

            }
        }
    }

    void EnemyAttack(Vector3 pos)
    {
        if (work == 0 && _agent.enabled == true)
        {
            _agent.destination = players[rnd].transform.position;
        }

        if (attackFlag == true)
        {
            attackTime += Time.deltaTime;

            if (AttackCollider.enabled == true)
            {
                if (attackTime >= 1.2)
                {
                    rastTimerFlag = true;
                    attackFlag = false;
                    noAttack = true;
                    AttackCollider.enabled = false;
                    attackTime = 0;
                }
            }
            else
            {
                if (attackTime >= 0.9)
                    AttackCollider.enabled = true;
                else if (attackTime >= 0.5)
                {
                    animator.SetTrigger("attck");
                }
            }
        }

        if (rastTimerFlag)
        {
            rastTimer += Time.deltaTime;
        }
        //退場
        if (rastTimer >= 1)
        {
            //AttackCollider.enabled = false;

            if (oneFlag == false)
            {
                animator.SetTrigger("idle");
                _agent.enabled = true;
                oneFlag = true;
            }

            if (_agent.enabled == true)
                _agent.destination = rirurnTransform.transform.position;

            if (pos.z <= -120)
            {
                //ex_flag = false;
                Destroy(gameObject);
            }
        }
    }

    void KnockBack(Vector3 pos)
    {
        //ノックバック時に場外に行かなかった時の処理
        if (_agent.enabled == false && gameState == SUMMON.END)
        {
            wlTime += Time.deltaTime;

            if (exFlag == true)
            {
                rb.useGravity = false;
                myCollider.enabled = false;
            }
            else if (wlTime >= 1.5f && exFlag == false)
            {
                _agent.enabled = true;
                wlTime = 0;
            }
        }

        Vector3 rangeApos = spderpositionA.position;
        Vector3 rangeDpos = spderpositionD.position;

        if (gameState == SUMMON.END)
        {

            if (rangeApos.x <= pos.x ||
                rangeApos.x <= pos.z)
                rb.useGravity = true;

            if (rangeDpos.x >= pos.x ||
                rangeDpos.x >= pos.z)
                rb.useGravity = true;

            if (pos.x >= exitX ||
                pos.z >= exitZ)
                exFlag = true;

            else if (pos.x <= exitMinx ||
                      pos.z <= exitMinz)
                exFlag = true;
            else
                exFlag = false;

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

    //帰宅
    IEnumerator Onexit()
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
        if (collision.gameObject == players[rnd]&&noAttack==false)
        {
            work = 1;
            attackFlag = true;
            attackTime = 0;
            noAttack = true;
            oneFlag = false;
            animator.SetTrigger("attckidle");
        }

        if (collision.gameObject.CompareTag("Wall") &&
            gameState == SUMMON.END &&
            noAttack == true)
        {
            myCollider.enabled=false;
            Debug.Log("Wall");
            _agent.enabled = false;
            StartCoroutine(Onexit());
        }

        if (collision.gameObject.CompareTag("thin wall") &&
            gameState == SUMMON.LAMDING)    
        {
            Vector3 force = new Vector3(0.0f, 0.0f, 3.0f);
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    public void OnAttackCollider()
    {
        playerDamage[rnd].CallDamage();
    }
}