using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーのアクションに関するクラス
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float hitTime = 0.25f;

    [SerializeField]
    private GameObject attackEffect = null;

    [SerializeField]
    private Transform effectPos = null;

    [SerializeField]
    private GameObject fistObject = null;

    [SerializeField]
    private Transform fistPos = null;

    [SerializeField]
    private SEManager seManager = null;

    private BoxCollider boxCol = null;
    private Animator animator = null;
    private AudioSource audioSource = null;
    private GameObject instantPunch = null;
    private PlayerMove playerMove = null;

    private int myPlayerNo = 5;
    private float time = 0;

    private bool isAttack = false;
    private bool isCarry = false;
    private bool isDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;

        animator = GetComponentInParent<Animator>();
        playerMove = GetComponentInParent<PlayerMove>();
        audioSource= GetComponentInParent<AudioSource>();

        time = 0;

        isAttack = false;
        isCarry = false;
        isDamage = false;
}

    // Update is called once per frame
    void Update()
    {
        if(!isCarry && !isDamage)
        {
            if (Gamepad.all[myPlayerNo].aButton.wasPressedThisFrame)
            {
                FistAttack();
            }
            if (isAttack)
            {
                EndAttack();
            }
        }
        else if(isCarry || isDamage)
        {
            if (isAttack)
            {
                instantPunch.GetComponent<FistDissolve>().CallEndDissolve();
                EndAttack();
                time = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (!rb)
                return;

            rb.AddForce(this.transform.forward * 5f, ForceMode.VelocityChange);

            NavMeshAgent nav = other.GetComponent<NavMeshAgent>();
            if (!nav)
                return;

            nav.enabled = false;
        }
    }

    /// <summary>
    /// プレイヤーがアクションを開始した際に呼び出す
    /// </summary>
    private void FistAttack()
    {
        if (!isAttack)
        {
            animator.SetBool("Attack", true);
            boxCol.enabled = true;
            playerMove.StartAttack();
            Instantiate(attackEffect, effectPos.position, this.transform.rotation);
            instantPunch = Instantiate(fistObject, fistPos.position, fistPos.rotation);
            audioSource.PlayOneShot(seManager.PlayerAttackSe);

            isAttack = true;
        }
    }

    /// <summary>
    /// プレイヤーのアクションが終了した際に呼び出す
    /// </summary>
    private void EndAttack()
    {
        time += Time.deltaTime;
        if (time >= hitTime)
        {
            animator.SetBool("Attack", false);
            boxCol.enabled = false;
            playerMove.EndAttack();
            instantPunch = null;

            isAttack = false;
            time = 0;
        }
    }

    /// <summary>
    /// 自身のプレイヤー番号を外部から取得する
    /// </summary>
    /// <param name="parentNumber">プレイヤー番号</param>
    public void GetPlayerNo(int parentNumber)
    {
        myPlayerNo = parentNumber;
    }

    /// <summary>
    /// プレイヤーが運搬を開始した際に呼び出す
    /// </summary>
    public void OnIsCarry()
    {
        isCarry = true;
    }

    /// <summary>
    /// プレイヤーが運搬を終了した際に呼び出す
    /// </summary>
    public void OffIsCarry()
    {
        isCarry = false;
    }

    /// <summary>
    /// プレイヤーがダメージを受けた際に呼び出す
    /// </summary>
    public void OnIsDamage()
    {
        isDamage = true;
    }

    /// <summary>
    /// プレイヤーのダメージが終了した際に呼び出す
    /// </summary>
    public void OffIsDamage()
    {
        isDamage = false;
    }
}