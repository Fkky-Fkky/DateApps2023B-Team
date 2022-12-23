using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerDamage : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider capsuleCol;
    float time = 0;
    private bool currentDamage;

    [SerializeField]
    private float stanTime = 5.0f;

    private float defaultPosY = 54.0f;
    private float DamagePosX = 0.0f;
    private float DamagePosZ = 0.0f;
    private bool doCouroutine = false;

    private PlayerMove playerMove;
    private PlayerCarryDown playerCarryDown;

    private Animator AnimationImage;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        capsuleCol = this.gameObject.GetComponent<CapsuleCollider>();
        AnimationImage = GetComponent<Animator>();

        defaultPosY = this.gameObject.transform.position.y;

        playerMove = this.gameObject.GetComponent<PlayerMove>();
        playerCarryDown = this.gameObject.GetComponentInChildren<PlayerCarryDown>();

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

            if (time > stanTime)
            {
                time = 0;
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
    }

    public void CallDamage()
    {
        capsuleCol.enabled = false;
        
        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);
        AnimationImage.SetBool("Damage", true);

        playerMove.PlayerDamage();
        playerCarryDown.carryDamage = true;

        DamagePosX = this.gameObject.transform.position.x;
        DamagePosZ = this.gameObject.transform.position.z;

        currentDamage = true;
    }

}
