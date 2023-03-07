using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;


//[RequireComponent(typeof(NavMeshAgent))]
public class Enemy1 : MonoBehaviour
{
    [SerializeField] private GameObject[] players = null;

    [SerializeField] private PlayerDamage[] playerDamage = null;

    [SerializeField] private Transform centerPoint = null;

    [SerializeField] private Transform rirurnTransform = null;

    [SerializeField]
    private float climbingSpeed = 3;

    [SerializeField]
    [Tooltip("¶¬·éÍÍA")]
    private Transform spderPositionA = null;

    [SerializeField]
    [Tooltip("¶¬·éÍÍD")]
    private Transform spderPositionD = null;

    //UÌ½è»è
    private Collider attackCollider = null;

    private Collider myCollider = null;

    private Animator animator = null;

    private Rigidbody rb = null;

    private NavMeshAgent _agent = null;

    enum SUMMON
    {
        CLIMB,

        JUMP,

        LAMDING,

        END,
    }
    SUMMON gameState = SUMMON.CLIMB;

    public int rnd;

    private int destroyPosition = -25;

    void Start()
    {
      
    }
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        if (destroyPosition >= pos.y)
        {
            Destroy(gameObject);
        }

       // if(E)
    }
}