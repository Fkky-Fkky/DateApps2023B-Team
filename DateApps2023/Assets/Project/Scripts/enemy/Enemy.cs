using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

/// <summary>
/// �G�l�~�[�̃N���X
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

    private Rigidbody rb = null;

    private NavMeshAgent agent = null;

    enum SUMMON
    {
        CLIMB,

        JUMP,

        LAMDING,

        END,
    }
    SUMMON gameState = SUMMON.CLIMB;

    private bool jumpFlag = false;

    public int rnd;

    private int destroyPosition = -25;

    private int rotationStatePosition = 4;

    private int climbingPosition = 1;

    private float jumpStatePosition = -0.5f;

    private float jumpPower = 18.0f;

    void Start()
    {
        //����������
        Vector3 vec = centerPoint.transform.position - this.transform.position;
        vec.y = 0f;
        Quaternion quaternion = Quaternion.LookRotation(vec);
        this.transform.rotation = quaternion;
        this.transform.Rotate(-90, 0f, 0f);

        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;

        agent = this.GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
        agent.enabled = false;

        rnd = Random.Range(0, 3);
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
    /// ���n��̃v���C���[�ǂ������鎞�̊֐�
    /// </summary>
    private void End()
    {
        if (gameState == SUMMON.END)
            agent.destination = players[rnd].transform.position;
    }

    /// <summary>
    /// �G�l�~�[���|���������̊֐�
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
    ///�G�l�~�[���ǂ�o��֐� 
    /// </summary>
    private void Climb(Vector3 pos)
    {
        if (gameState == SUMMON.CLIMB)
        {
            if (pos.y >= climbingPosition)
                jumpFlag = true;

            animator.SetTrigger("work");
            transform.position += new Vector3(0, climbingSpeed, 0) * Time.deltaTime;
        }

        OnJumpFlag(pos);
    }
    /// <summary>
    /// �W�����v�t���O�ɂȂ������̊֐�
    /// </summary>
    private void OnJumpFlag(Vector3 pos)
    {
        if (jumpFlag)
        {
            if (pos.x > 0)
            {
                Vector3 force = new Vector3(-5.0f, jumpPower, 0.0f);
                rb.AddForce(force, ForceMode.Impulse);
            }

            if (pos.x < 0)
            {
                Vector3 force = new Vector3(5.0f, jumpPower, 0.0f);
                rb.AddForce(force, ForceMode.Impulse);
            }

            rb.useGravity = true;
            gameState = SUMMON.JUMP;
            jumpFlag = false;
        }
    }

    /// <summary>
    ///�ǂ����I������̃W�����v�̊֐� 
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
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                gameState = SUMMON.LAMDING;
                animator.SetTrigger("idle");
            }
        }
    }
    /// <summary>
    /// ���n�̊֐�
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
    /// �_���[�W�Ăяo���֐�
    /// </summary>
    public void OnAttackCollider()
    {
        playerDamage[rnd].CallDamage();
    }

    /// <summary>
    /// �A�j���[�^�[�Ăяo���̊֐�
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionStay(Collision collision)//Trigger
    {
        if (collision.gameObject == players[rnd])
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