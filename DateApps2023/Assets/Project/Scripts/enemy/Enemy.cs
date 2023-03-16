//�S����:�ێq�
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�l�~�[�̃N���X
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
    /// �G�l�~�[���o�ꂵ�Ă���v���C���[��ǂ��n�߂�܂ł̃X�e�[�g
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

    private int playerNumber = 0;

    private const int ROTATION_STATE_POSITION = 4;

    private const int CLIMBING_POSITION = 1;

    private const int DESTROY_POSITION = -25;

    private const int LEFT_JUMP_POWER = -5;

    private const int LIGHT_JUMP_POWER = 5;

    private const float JUMP_ROTATE = 0.35f;

    private const float JUMP_STATE_POSITION = -0.5f;

    private const float JUMP_POWER = 18.0f;

    private const float CENTER_ROTATE = -90;

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
    /// �ŏ��̃G�l�~�[�̕����ݒ�
    /// </summary>
    private void CenterRotate()
    {
        Vector3 vec = centerPoint.transform.position - this.transform.position;
        vec.y = 0f;
        Quaternion quaternion = Quaternion.LookRotation(vec);
        this.transform.rotation = quaternion;
        this.transform.Rotate(CENTER_ROTATE, 0f, 0f);
    }

    /// <summary>
    /// �G�l�~�[���|���������̊֐�
    /// </summary>
    private void Destroy(Vector3 pos)
    {
        if (DESTROY_POSITION >= pos.y)
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
            if (pos.y >= CLIMBING_POSITION)
                isJumpFlag = true;

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
        if (isJumpFlag)
        {
            if (pos.x > 0)
            {
                Vector3 force = new Vector3(LEFT_JUMP_POWER, JUMP_POWER, 0.0f);
                myRigidbody.AddForce(force, ForceMode.Impulse);
            }

            if (pos.x < 0)
            {
                Vector3 force = new Vector3(LIGHT_JUMP_POWER, JUMP_POWER, 0.0f);
                myRigidbody.AddForce(force, ForceMode.Impulse);
            }

            myRigidbody.useGravity = true;
            gameState = SUMMON.JUMP;
            isJumpFlag = false;
        }
    }

    /// <summary>
    ///�ǂ����I������̃W�����v�̊֐� 
    /// </summary>
    private void Jump(Vector3 pos)
    {
        if (gameState == SUMMON.JUMP)
        {
            if (pos.y >= ROTATION_STATE_POSITION)
            {
                this.transform.Rotate(JUMP_ROTATE, 0f, 0f);
            }

            if (pos.y <= JUMP_STATE_POSITION)
            {
                myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                gameState = SUMMON.LANDING;
                animator.SetTrigger("idle");
            }
        }
    }
    /// <summary>
    /// ���n���̊֐�
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
    /// ���n��̃v���C���[�ǂ������鎞�̊֐�
    /// </summary>
    private void End()
    {
        if (gameState == SUMMON.END)
        {
            agent.destination = players[playerNumber].transform.position;
        }
    }

    /// <summary>
    /// �_���[�W�Ăяo���֐�
    /// </summary>
    public void OnAttackCollider()
    {
        playerDamage[playerNumber].CallDamage();
    }

    /// <summary>
    /// �A�j���[�^�[�Ăяo���̊֐�
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }
}