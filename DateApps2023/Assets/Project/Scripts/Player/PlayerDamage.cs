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
    float time = 0;
    private bool currentDamage;
    private bool currentCapture;

    [SerializeField]
    private float stanTime = 5.0f;

    [SerializeField]
    private float captureTime = 5.0f;

    private float defaultPosY = 54.0f;
    private float DamagePosX = 0.0f;
    private float DamagePosZ = 0.0f;
    private bool doCouroutine = false;

    private PlayerMove playerMove;
    private PlayerCarryDown playerCarryDown;
    private PlayerAttack playerAttack;

    private Animator AnimationImage;

    [SerializeField]
    private int EndStanCount = 3;

    [SerializeField]
    private int EndCaptureCount = 1;

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
        AnimationImage = GetComponent<Animator>();

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
        if (currentDamage)
        {
            InCurrentDamage();
        }

        if (currentCapture)
        {
            InCurrentCapture();
        }

    }

    void InCurrentDamage()
    {
        time += Time.deltaTime;
        this.gameObject.transform.position = new Vector3(DamagePosX, defaultPosY, DamagePosZ);

        if (time > stanTime || knockCount >= EndStanCount)
        {
            EndPlayerDamage();
            currentDamage = false;
        }
        else if (time >= damageEffectInterval)
        {
            if (!doCouroutine)
            {
                Vector3 InstantPos = damageStanPoint.position;
                InstantPos.y = damageEffectPosY;
                cloneStanEffect = Instantiate(stanEffect, InstantPos, this.transform.rotation);
                audioSource.PlayOneShot(stanSound);

                doCouroutine = true;
            }
        }
    }

    void InCurrentCapture()
    {
        if (!doCouroutine)
        {
            doCouroutine = true;
        }

        time += Time.deltaTime;
        this.gameObject.transform.position = new Vector3(DamagePosX, defaultPosY, DamagePosZ);

        if (time > captureTime || knockCount >= EndCaptureCount)
        {
            EndPlayerDamage();
            currentCapture = false;
        }
    }

    void EndPlayerDamage()
    {
        time = 0;
        knockCount = 0;
        stanBoxCol.enabled = false;
        capsuleCol.enabled = true;
        if (doCouroutine)
        {
            DestroyStanEffect();
            AnimationImage.SetBool("Damage", false);
            AnimationImage.SetBool("Capture", false);

            doCouroutine = false;
        }

        playerMove.NotPlayerDamage();
        playerCarryDown.OffCarryDamage();
        playerAttack.OffIsDamage();
    }

    void DestroyStanEffect()
    {
        Destroy(cloneStanEffect);
        cloneStanEffect = null;
        this.gameObject.transform.position = new Vector3(
        this.gameObject.transform.position.x,
        defaultPosY,
        this.gameObject.transform.position.z
        );
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

        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);
        AnimationImage.SetBool("Capture", false);
        AnimationImage.SetBool("Damage", true);

        playerMove.PlayerDamage();
        playerCarryDown.CarryCancel();
        playerCarryDown.OnCarryDamage();
        playerAttack.OnIsDamage();

        DamagePosX = transform.position.x;
        DamagePosZ = transform.position.z;

        time = 0;
        knockCount = 0;
        doCouroutine = false;

        if (currentCapture)
        {
            currentCapture = false;
        }

        currentDamage = true;
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

        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);
        AnimationImage.SetBool("Damage", false);
        AnimationImage.SetBool("Capture", true);

        playerMove.PlayerDamage();
        playerCarryDown.CarryCancel();
        playerCarryDown.OnCarryDamage();
        playerAttack.OnIsDamage(); 

        DamagePosX = this.gameObject.transform.position.x;
        DamagePosZ = this.gameObject.transform.position.z;

        Vector3 InstantPos = this.gameObject.transform.position;
        InstantPos.y = captureEffectPosY;
        cloneStanEffect = Instantiate(stanEffect, InstantPos, this.transform.rotation);
        audioSource.PlayOneShot(stanSound);

        time = 0;
        knockCount = 0;
        doCouroutine = false;

        if (currentDamage)
        {
            currentDamage = false;
        }

        currentCapture = true;
        enemyScript = null;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            knockCount++;
            Instantiate(knockbackEffect, this.transform.position, other.transform.rotation);

            if (!currentDamage && !currentCapture)
            {
                CallKnockBack(other.gameObject.transform.parent.gameObject.transform);
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyScript = other.gameObject.GetComponent<enemy>();
            if (!currentDamage && myPlayerNo == enemyScript.rnd)
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
        if (!currentDamage && myPlayerNo == enemyScript.rnd)
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
