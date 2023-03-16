//担当者:丸子羚
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// エネミーのクラス
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players = null;

    [SerializeField]
    private PlayerDamage[] playerDamage = null;

    [SerializeField]
    private Transform centerPoint = null;

    [SerializeField]
    private float climbingSpeed = 3;

    [SerializeField]
    private int enemyHp = 3;

    private Animator animator = null;

    private Rigidbody myRigidbody = null;

    private NavMeshAgent agent = null;

    /// <summary>
    /// エネミーが登場してからプレイヤーを追い始めるまでのステート
    /// </summary>
    enum SUMMON
    {
        CLIMB,

        JUMP,

        LANDING,

        END,
    }
    SUMMON gameState = SUMMON.CLIMB;

    private bool isJumpFlag = false;

    private int destroyPosition = -25;

    private int rotationStatePosition = 4;

    private int climbingPosition = 1;

    private int playerNumber = 0;

    private int leftJumpPower = -5;

    private int lightJumpPower = 5;

    private float jumpRotate = 0.35f;

    private float jumpStatePosition = -0.5f;

    private float jumpPower = 18.0f;

    private float centerRotate = -90 * Time.deltaTime;

    public int Random { get; private set; }

    void Start()
    {
        CenterRotate();
       
        myRigidbody = this.GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;

        agent = this.GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
        agent.enabled = false;

        Random = UnityEngine.Random.Range(0, 3);
        playerNumber=Random;
    }

    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        Destroy(pos);

        Climb(pos);

        Jump(pos);

        Landing();

        End();

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == players[playerNumber])
        {
            agent.enabled = false;
            animator.SetTrigger("attack");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        agent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttackPoint"))
        {
            enemyHp -= 1;
        }
    }

    /// <summary>
    /// 最初のエネミーの方向設定
    /// </summary>
    private void CenterRotate()
    {
        Vector3 vec = centerPoint.transform.position - this.transform.position;
        vec.y = 0f;
        Quaternion quaternion = Quaternion.LookRotation(vec);
        this.transform.rotation = quaternion;
        this.transform.Rotate(centerRotate, 0f, 0f);
    }

    /// <summary>
    /// エネミーが倒される条件の関数
    /// </summary>
    private void Destroy(Vector3 pos)
    {
        if (destroyPosition >= pos.y)
            Destroy(gameObject);

        if (0 >= enemyHp)
        {
            animator.SetTrigger("Die");
            agent.enabled = false;
        }
    }
    /// <summary>
    ///エネミーが壁を登る関数
    /// </summary>
    private void Climb(Vector3 pos)
    {
        if (gameState == SUMMON.CLIMB)
        {
            if (pos.y >= climbingPosition)
                isJumpFlag = true;

            animator.SetTrigger("work");
            transform.position += new Vector3(0, climbingSpeed, 0) * Time.deltaTime;
        }

        OnJumpFlag(pos);
    }
    /// <summary>
    /// ジャンプフラグになった時の関数
    /// </summary>
    private void OnJumpFlag(Vector3 pos)
    {
        if (isJumpFlag)
        {
            if (pos.x > 0)
            {
                Vector3 force = new Vector3(leftJumpPower, jumpPower, 0.0f);
                myRigidbody.AddForce(force, ForceMode.Impulse);
            }

            if (pos.x < 0)
            {
                Vector3 force = new Vector3(lightJumpPower, jumpPower, 0.0f);
                myRigidbody.AddForce(force, ForceMode.Impulse);
            }

            myRigidbody.useGravity = true;
            gameState = SUMMON.JUMP;
            isJumpFlag = false;
        }
    }

    /// <summary>
    ///壁を上り終えた後のジャンプの関数 
    /// </summary>
    private void Jump(Vector3 pos)
    {
        if (gameState == SUMMON.JUMP)
        {
            if (pos.y >= rotationStatePosition)
            {
                this.transform.Rotate(jumpRotate, 0f, 0f);
            }

            if (pos.y <= jumpStatePosition)
            {
                myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                gameState = SUMMON.LANDING;
                animator.SetTrigger("idle");
            }
        }
    }
    /// <summary>
    /// 着地時の関数
    /// </summary>
    private void Landing()
    {
        if (gameState == SUMMON.LANDING)
        {
            agent.enabled = true;
            animator.SetTrigger("work");
            gameState = SUMMON.END;
        }
    }

    /// <summary>
    /// 着地後のプレイヤー追いかける時の関数
    /// </summary>
    private void End()
    {
        if (gameState == SUMMON.END)
        {
            agent.destination = players[playerNumber].transform.position;
        }
    }

    /// <summary>
    /// ダメージ呼び出し関数
    /// </summary>
    public void OnAttackCollider()
    {
        playerDamage[playerNumber].CallDamage();
    }

    /// <summary>
    /// アニメーター呼び出しの関数
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }
}