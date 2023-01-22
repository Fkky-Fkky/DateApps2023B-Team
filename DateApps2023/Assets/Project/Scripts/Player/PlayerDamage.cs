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
    private float stanTime = 5.0f;

    [SerializeField]
    private float captureTime = 5.0f;

    private float defaultPosY = 54.0f;
    private float DamagePosX = 0.0f;
    private float DamagePosZ = 0.0f;
    private bool doCouroutine = false;

    private PlayerMove playerMove;
    private PlayerCarryDown playerCarryDown;

    private Animator AnimationImage;

    [SerializeField]
    private float knockBackPower = 50.0f;

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



    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        capsuleCol = this.gameObject.GetComponent<CapsuleCollider>();
        AnimationImage = GetComponent<Animator>();

        defaultPosY = this.gameObject.transform.position.y;

        playerMove = this.gameObject.GetComponent<PlayerMove>();
        playerCarryDown = this.gameObject.GetComponentInChildren<PlayerCarryDown>();

        knockCount = 0;
        stanBoxCol.enabled = false;
    }

    private void Update()
    {
        if (currentDamage)
        {
            if (!doCouroutine)
            {
                doCouroutine = true;
            }

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
                    this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    defaultPosY,
                    this.gameObject.transform.position.z
                    );
                    AnimationImage.SetBool("Damage", false);

                    doCouroutine = false;
                }
        
                playerMove.NotPlayerDamage();
                playerCarryDown.carryDamage = false;
                currentDamage = false;

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
                    this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    defaultPosY,
                    this.gameObject.transform.position.z
                    );
                    AnimationImage.SetBool("Damage", false);

                    doCouroutine = false;
                }

                playerMove.NotPlayerDamage();
                playerCarryDown.carryDamage = false;
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
        Debug.Log("anpanan");

        capsuleCol.enabled = false;
        stanBoxCol.enabled = true;

        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);
        AnimationImage.SetBool("Damage", true);

        playerMove.PlayerDamage();
        playerCarryDown.carryDamage = true;

        DamagePosX = this.gameObject.transform.position.x;
        DamagePosZ = this.gameObject.transform.position.z;

        knockCount = 0;

        if (currentCapture)
        {
            currentCapture = false;
        }

        currentDamage = true;
    }

    public void CallCapture()
    {
        capsuleCol.enabled = false;
        stanBoxCol.enabled = true;

        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);
        AnimationImage.SetBool("Damage", true);

        playerMove.PlayerDamage();
        playerCarryDown.carryDamage = true;

        DamagePosX = this.gameObject.transform.position.x;
        DamagePosZ = this.gameObject.transform.position.z;

        knockCount = 0;

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

        }
    }

    public void CallKnockBack(Transform knockPos)
    {
        var distination = (transform.position - knockPos.position).normalized;
        transform.position += distination * knockBackPower;
    }

    public void GetPlayerNo(int myNumber)
    {
        myPlayerNo = myNumber;
    }

}
