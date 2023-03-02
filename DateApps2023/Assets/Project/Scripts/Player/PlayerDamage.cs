using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;

public class PlayerDamage : MonoBehaviour
{
    #region
    private Rigidbody rb;
    private CapsuleCollider capsuleCol;
    private float time = 0;
    private bool isCurrentDamage;
    private bool isCurrentCapture;

    [SerializeField]
    private float stanTime = 5.0f;

    [SerializeField]
    private float captureTime = 5.0f;

    private float defaultPosY = 54.0f;
    private float damagePosX = 0.0f;
    private float damagePosZ = 0.0f;
    private bool hasDestroyStanEffect = false;

    private PlayerMove playerMove;
    private PlayerCarryDown playerCarryDown;
    private PlayerAttack playerAttack;

    private Animator animationImage;

    [SerializeField]
    private int endStanCount = 3;

    [SerializeField]
    private int endCaptureCount = 1;

    [SerializeField]
    private BoxCollider stanBoxCol;

    private int knockCount = 0;
    private int myPlayerNo = 5;
    private enemy enemyScript = null;

    [SerializeField]
    private GameObject knockbackEffect = null;

    [SerializeField]
    private GameObject stanEffect = null;
    private GameObject cloneStanEffect = null;

    [SerializeField]
    private float damageEffectInterval = 1.75f;

    [SerializeField]
    private Transform damageStanPoint = null;

    [SerializeField]
    private float damageEffectPosY = -2.0f;

    [SerializeField]
    private float captureEffectPosY = -0.75f;

    [SerializeField]
    private AudioClip stanSound = null;

    [SerializeField]
    private AudioClip knockbackSound = null;

    private AudioSource audioSource;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        capsuleCol = GetComponent<CapsuleCollider>();
        animationImage = GetComponent<Animator>();

        defaultPosY = transform.position.y;

        playerMove = GetComponent<PlayerMove>();
        playerCarryDown = GetComponentInChildren<PlayerCarryDown>();
        playerAttack = GetComponentInChildren<PlayerAttack>();

        audioSource = GetComponent<AudioSource>();

        knockCount = 0;
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

    void OnCurrentDamage()
    {
        time += Time.deltaTime;
        this.gameObject.transform.position = new Vector3(damagePosX, defaultPosY, damagePosZ);

        if (time > stanTime || knockCount >= endStanCount)
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

            isCurrentCapture = false;

        }
    }

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

    public void CallDamage()
    {
        if(cloneStanEffect != null)
        {
            Destroy(cloneStanEffect);
            cloneStanEffect = null;
        }

        capsuleCol.enabled = false;
        stanBoxCol.enabled = true;

        animationImage.SetBool("Carry", false);
        animationImage.SetBool("CarryMove", false);
        animationImage.SetBool("Capture", false);
        animationImage.SetBool("Damage", true);

        playerMove.PlayerDamage();
        playerCarryDown.CarryCancel();
        playerCarryDown.OnCarryDamage();
        playerAttack.OnIsDamage();

        damagePosX = transform.position.x;
        damagePosZ = transform.position.z;

        time = 0;
        knockCount = 0;
        hasDestroyStanEffect = false;

        if (isCurrentCapture)
        {
            isCurrentCapture = false;
        }

        isCurrentDamage = true;
    }

    public void CallCapture()
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
        animationImage.SetBool("Damage", false);
        animationImage.SetBool("Capture", true);

        playerMove.PlayerDamage();
        playerCarryDown.CarryCancel();
        playerCarryDown.OnCarryDamage();
        playerAttack.OnIsDamage(); 

        damagePosX = this.gameObject.transform.position.x;
        damagePosZ = this.gameObject.transform.position.z;

        Vector3 InstantPos = this.gameObject.transform.position;
        InstantPos.y = captureEffectPosY;
        cloneStanEffect = Instantiate(stanEffect, InstantPos, this.transform.rotation);
        audioSource.PlayOneShot(stanSound);

        time = 0;
        knockCount = 0;
        hasDestroyStanEffect = false;

        if (isCurrentDamage)
        {
            isCurrentDamage = false;
        }

        isCurrentCapture = true;
        enemyScript = null;

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
            enemyScript = other.gameObject.GetComponent<enemy>();
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

    public void JudgeCapture(GameObject enemy)
    {
        enemyScript = enemy.GetComponent<enemy>();
        if (!isCurrentDamage && myPlayerNo == enemyScript.rnd)
        {
            CallCapture();
        }
        else
        {
            enemyScript = null;
        }
    }

    public void CallKnockBack(Transform knockPos)
    {
        audioSource.PlayOneShot(knockbackSound);
    }

    public void GetPlayerNo(int myNumber)
    {
        myPlayerNo = myNumber;
    }

}
