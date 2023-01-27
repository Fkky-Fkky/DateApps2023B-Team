using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;

public class PlayerDamage : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider capsuleCol;
    float time = 0;
    private bool currentDamage;
    private bool currentCapture;

    [SerializeField]
    private float stanTime = 1.0f;

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

    //[SerializeField]
    //private float knockBackPower = 50.0f;

    [SerializeField]
    private int EndStanCount = 3;

    [SerializeField]
    private int EndCaptureCount = 1;

    [SerializeField]
    private BoxCollider stanBoxCol;

    private int knockCount = 0;
    //private bool onlyKnock = true;

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
            time += Time.deltaTime;
            this.gameObject.transform.position = new Vector3(DamagePosX, defaultPosY, DamagePosZ);

            if (time > stanTime || knockCount >= EndStanCount)
            {
                time = 0;
                knockCount = 0;
                stanBoxCol.enabled = false;
                capsuleCol.enabled = true;
                if (doCouroutine)
                {
                    Destroy(cloneStanEffect);
                    cloneStanEffect = null;
                    this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    defaultPosY,
                    this.gameObject.transform.position.z
                    );
                    AnimationImage.SetBool("Damage", false);
                    AnimationImage.SetBool("Capture", false);

                    doCouroutine = false;
                }
        
                playerMove.NotPlayerDamage();
                playerCarryDown.OffCarryDamage();
                playerAttack.OffIsDamage();

                currentDamage = false;

            }else if(time >= damageEffectInterval)
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

        if (currentCapture)
        {
            if (!doCouroutine)
            {
                doCouroutine = true;
            }

            time += Time.deltaTime;
            this.gameObject.transform.position = new Vector3(DamagePosX, defaultPosY, DamagePosZ);

            if (time > captureTime || knockCount >= EndCaptureCount)
            {
                time = 0;
                knockCount = 0;
                stanBoxCol.enabled = false;
                capsuleCol.enabled = true;
                if (doCouroutine)
                {
                    Destroy(cloneStanEffect);
                    cloneStanEffect = null;
                    this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    defaultPosY,
                    this.gameObject.transform.position.z
                    );
                    AnimationImage.SetBool("Damage", false);
                    AnimationImage.SetBool("Capture", false);

                    doCouroutine = false;
                }

                playerMove.NotPlayerDamage();
                playerCarryDown.OffCarryDamage();
                playerAttack.OffIsDamage();

                currentCapture = false;

            }
        }

        ////デバッグ用コマンド　C：拘束　D：ダメージ
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    CallCapture();
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    CallDamage();
        //}
    }

    public void CallDamage()
    {
        if(cloneStanEffect != null)
        {
            Destroy(cloneStanEffect);
            cloneStanEffect = null;
        }
        playerMove.CallHanteiEnter();


        capsuleCol.enabled = false;
        stanBoxCol.enabled = true;

        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);
        AnimationImage.SetBool("Capture", false);
        AnimationImage.SetBool("Damage", true);

        playerMove.PlayerDamage();
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
        playerMove.CallHanteiEnter();

        capsuleCol.enabled = false;
        stanBoxCol.enabled = true;

        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);
        AnimationImage.SetBool("Damage", false);
        AnimationImage.SetBool("Capture", true);

        playerMove.PlayerDamage();
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

            if (!currentDamage && !currentCapture)
            {
                Instantiate(knockbackEffect, this.transform.position, other.transform.rotation);
                CallKnockBack(other.gameObject.transform.parent.gameObject.transform);
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyScript = collision.gameObject.GetComponent<enemy>();
            if (!currentDamage && myPlayerNo == enemyScript.rnd)
            {
                CallCapture();
            }
            else
            {
                enemyScript = null;
            }
        }
        if (other.gameObject.CompareTag("BossAttack"))
        {
            CallDamage();
        }
    }

    public void CallKnockBack(Transform knockPos)
    {
        audioSource.PlayOneShot(knockbackSound);
        //var distination = (transform.position - knockPos.position).normalized;
        //transform.position += distination * knockBackPower;
        rb.velocity = Vector3.zero;
    }

    public void GetPlayerNo(int myNumber)
    {
        myPlayerNo = myNumber;
    }

}
