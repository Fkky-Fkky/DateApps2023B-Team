using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

/// <summary>
/// エネミーのクラス
/// </summary>
//[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject[] players = null;

    [SerializeField] private PlayerDamage[] playerDamage = null;

    [SerializeField] private Transform centerPoint = null;

    [SerializeField]
    private float climbingSpeed = 3;

    [SerializeField]
    private int enemyHp = 3;

    private Animator animator = null;

    private Rigidbody rigidbody = null;

    private NavMeshAgent agent = null;


    /// <summary>
    /// エネミーが登場してからプレイヤーを追い始めるまでのステート
    /// </summary>
    enum SUMMON
    {
        CLIMB,

        JUMP,

        LAMDING,

        END,
    }
    SUMMON gameState = SUMMON.CLIMB;

    private bool isJumpFlag = false;

    public int random = 0;

    private int destroyPosition = -25;

    private int rotationStatePosition = 4;

    private int climbingPosition = 1;

    private float jumpStatePosition = -0.5f;

    private float jumpPower = 18.0f;

    void Start()
    {
        CenterRotate();
       
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;

        agent = this.GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
        agent.enabled = false;

        random = Random.Range(0, 3);
    }

    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        Destroy(pos);

        Climb(pos);

        Jump(pos);

        Lamding();

        End();

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
        this.transform.Rotate(-90, 0f, 0f);
    }
    /// <summary>
    /// 着地後のプレイヤー追いかける時の関数
    /// </summary>
    private void End()
    {
        if (gameState == SUMMON.END)
            agent.destination = players[random].transform.position;
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
                Vector3 force = new Vector3(-5.0f, jumpPower, 0.0f);
                rigidbody.AddForce(force, ForceMode.Impulse);
            }

            if (pos.x < 0)
            {
                Vector3 force = new Vector3(5.0f, jumpPower, 0.0f);
                rigidbody.AddForce(force, ForceMode.Impulse);
            }

            rigidbody.useGravity = true;
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
                this.transform.Rotate(0.35f, 0f, 0f);
            }

            if (pos.y <= jumpStatePosition)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                gameState = SUMMON.LAMDING;
                animator.SetTrigger("idle");
            }
        }
    }
    /// <summary>
    /// 着地時の関数
    /// </summary>
    private void Lamding()
    {
        if (gameState == SUMMON.LAMDING)
        {
            agent.enabled = true;
            animator.SetTrigger("work");
            gameState = SUMMON.END;
        }
    }
    /// <summary>
    /// ダメージ呼び出し関数
    /// </summary>
    public void OnAttackCollider()
    {
        playerDamage[random].CallDamage();
    }

    /// <summary>
    /// アニメーター呼び出しの関数
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionStay(Collision collision)//Trigger
    {
        if (collision.gameObject == players[random])
        {
            agent.enabled = false;
            animator.SetTrigger("attack");
        }
    }

    void OnCollisionExit(Collision collision)//Trigger
    {
        agent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAttackPoint"))
        {
            enemyHp -= 1;
        }
    }
}