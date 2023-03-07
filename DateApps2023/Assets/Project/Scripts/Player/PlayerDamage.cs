using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// プレイヤーのダメージに関するクラス
/// </summary>
public class PlayerDamage : MonoBehaviour
{
    #region
    [SerializeField]
    private float stanTime = 5.0f;

    [SerializeField]
    private float captureTime = 5.0f;

    [SerializeField]
    private int endStanCount = 3;

    [SerializeField]
    private int endCaptureCount = 1;

    [SerializeField]
    private float damageEffectPosY = -2.0f;

    [SerializeField]
    private float captureEffectPosY = -0.75f;

    [SerializeField]
    private float damageEffectInterval = 1.75f;

    [SerializeField]
    private BoxCollider stanBoxCol;

    [SerializeField]
    private GameObject knockbackEffect = null;

    [SerializeField]
    private GameObject stanEffect = null;

    [SerializeField]
    private Transform damageStanPoint = null;

    [SerializeField]
    private AudioClip stanSound = null;

    [SerializeField]
    private AudioClip knockbackSound = null;

    private PlayerMove playerMove = null;
    private PlayerCarryDown playerCarryDown = null;
    private PlayerAttack playerAttack = null;
    private Enemy enemyScript = null;

    private GameObject cloneStanEffect = null;
    private Animator animationImage = null;
    private Rigidbody rb = null;
    private CapsuleCollider capsuleCol = null;
    private AudioSource audioSource = null;

    private int knockCount = 0;
    private int myPlayerNo = 5;
    private float time = 0.0f;
    private float defaultPosY = 54.0f;
    private float damagePosX = 0.0f;
    private float damagePosZ = 0.0f;

    private bool isCurrentDamage = false;
    private bool isCurrentCapture = false;
    private bool hasDestroyStanEffect = false;
    #endregion

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerCarryDown = GetComponentInChildren<PlayerCarryDown>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        enemyScript = null;

        animationImage = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCol = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();

        knockCount = 0;
        time = 0.0f;
        defaultPosY = transform.position.y;
        damagePosX = 0.0f;
        damagePosZ = 0.0f;

        isCurrentDamage = false;
        isCurrentCapture = false;
        hasDestroyStanEffect = false;
       
        stanBoxCol.enabled = false;
    }

    private void Update()
    {
        if (isCurrentDamage)
        {
            OnCurrentDamage();
        }
        if (isCurrentCapture)
        {
            OnCurrentCapture();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            knockCount++;
            Instantiate(knockbackEffect, this.transform.position, other.transform.rotation);

            if (!isCurrentDamage && !isCurrentCapture)
            {
                CallKnockBack(other.gameObject.transform.parent.gameObject.transform);
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyScript = other.gameObject.GetComponent<Enemy>();
            if (!isCurrentDamage && myPlayerNo == enemyScript.rnd)
            {
                CallCapture();
            }
            else
            {
                enemyScript = null;
            }
            JudgeCapture(other.gameObject);
        }
        if (other.gameObject.CompareTag("BossAttack"))
        {
            CallDamage();
        }
    }

    /// <summary>
    /// プレイヤーが気絶状態の際の処理を行う
    /// </summary>
    void OnCurrentDamage()
    {
        time += Time.deltaTime;
        this.gameObject.transform.position = new Vector3(damagePosX, defaultPosY, damagePosZ);

        if (time > stanTime || knockCount >= endStanCount)
        {
            CleanUpDamage();
            isCurrentDamage = false;
        }
        else if (time >= damageEffectInterval)
        {
            if (!hasDestroyStanEffect)
            {
                Vector3 InstantPos = damageStanPoint.position;
                InstantPos.y = damageEffectPosY;
                cloneStanEffect = Instantiate(stanEffect, InstantPos, this.transform.rotation);
                audioSource.PlayOneShot(stanSound);

                hasDestroyStanEffect = true;
            }
        }
    }

    /// <summary>
    /// プレイヤーが拘束状態の際の処理を行う
    /// </summary>
    void OnCurrentCapture()
    {
        if (!hasDestroyStanEffect)
        {
            hasDestroyStanEffect = true;
        }

        time += Time.deltaTime;
        this.gameObject.transform.position = new Vector3(damagePosX, defaultPosY, damagePosZ);

        if (time > captureTime || knockCount >= endCaptureCount)
        {
            CleanUpDamage();
            isCurrentCapture = false;
        }
    }

    /// <summary>
    /// プレイヤーがダメージから回復した際に呼び出す
    /// </summary>
    void CleanUpDamage()
    {
        time = 0;
        knockCount = 0;
        stanBoxCol.enabled = false;
        capsuleCol.enabled = true;
        if (hasDestroyStanEffect)
        {
            DeleteStanEffect();
        }

        playerMove.NotPlayerDamage();
        playerCarryDown.OffCarryDamage();
        playerAttack.OffIsDamage();
    }

    /// <summary>
    /// 発生させたスタンエフェクトを削除する
    /// </summary>
    void DeleteStanEffect()
    {
        Destroy(cloneStanEffect);
        cloneStanEffect = null;
        this.gameObject.transform.position = new Vector3(
        this.gameObject.transform.position.x,
        defaultPosY,
        this.gameObject.transform.position.z
        );
        animationImage.SetBool("Damage", false);
        animationImage.SetBool("Capture", false);

        hasDestroyStanEffect = false;
    }

    /// <summary>
    /// プレイヤーがボスから攻撃を受けた際に呼び出す
    /// </summary>
    public void CallDamage()
    {
        SetUpDamage();
        animationImage.SetBool("Capture", false);
        animationImage.SetBool("Damage", true);

        isCurrentDamage = true;
    }

    /// <summary>
    /// プレイヤーが小型エネミーから攻撃を受けた際に呼び出す
    /// </summary>
    public void CallCapture()
    {
        SetUpDamage();

        animationImage.SetBool("Damage", false);
        animationImage.SetBool("Capture", true);

        Vector3 InstantPos = this.gameObject.transform.position;
        InstantPos.y = captureEffectPosY;
        cloneStanEffect = Instantiate(stanEffect, InstantPos, this.transform.rotation);
        audioSource.PlayOneShot(stanSound);

        isCurrentCapture = true;
        enemyScript = null;
    }

    /// <summary>
    /// プレイヤーがダメージを受けた際の準備を行う
    /// </summary>
    void SetUpDamage()
    {
        if (cloneStanEffect != null)
        {
            Destroy(cloneStanEffect);
            cloneStanEffect = null;
        }

        capsuleCol.enabled = false;
        stanBoxCol.enabled = true;

        animationImage.SetBool("Carry", false);
        animationImage.SetBool("CarryMove", false);

        playerMove.PlayerDamage();
        playerCarryDown.CarryCancel();
        playerCarryDown.OnCarryDamage();
        playerAttack.OnIsDamage();

        damagePosX = transform.position.x;
        damagePosZ = transform.position.z;

        time = 0;
        knockCount = 0;
        hasDestroyStanEffect = false;

        if (isCurrentDamage)
        {
            isCurrentDamage = false;
        }
    }

    /// <summary>
    /// 自身が小型エネミーの標的かどうかを判定する
    /// </summary>
    /// <param name="enemy"></param>
    public void JudgeCapture(GameObject enemy)
    {
        enemyScript = enemy.GetComponent<Enemy>();
        if (!isCurrentDamage && myPlayerNo == enemyScript.rnd)
        {
            CallCapture();
        }
        else
        {
            enemyScript = null;
        }
    }

    /// <summary>
    /// 他プレイヤーからのパンチを受けた際に呼び出す
    /// </summary>
    /// <param name="knockPos"></param>
    public void CallKnockBack(Transform knockPos)
    {
        audioSource.PlayOneShot(knockbackSound);
    }

    /// <summary>
    /// 自身のプレイヤー番号を取得する
    /// </summary>
    /// <param name="myNumber"></param>
    public void GetPlayerNo(int myNumber)
    {
        myPlayerNo = myNumber;
    }

}
