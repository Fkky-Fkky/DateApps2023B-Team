using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerDamage : MonoBehaviour
{
    private Rigidbody rb;
    //private BoxCollider boxCol;
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

    private GameObject sabotageObject;
    private GameObject player;

    private Animator AnimationImage;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        capsuleCol = this.gameObject.GetComponent<CapsuleCollider>();
        AnimationImage = GetComponent<Animator>();

        defaultPosY = this.gameObject.transform.position.y;

        playerMove = this.gameObject.GetComponent<PlayerMove>();
        playerCarryDown = this.gameObject.GetComponentInChildren<PlayerCarryDown>();

        player = this.gameObject;

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
            //this.gameObject.transform.position = new Vector3(
            //        this.gameObject.transform.position.x,
            //        defaultPosY,
            //        this.gameObject.transform.position.z
            //        );

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
        Debug.Log("anpanan");

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

    //public void AvoidObject()
    //{
    //    var heading = player.transform.position - sabotageObject.transform.position;
    //    this.gameObject.transform.position += new Vector3(heading.x * 2.0f, 0.0f, heading.z * 2.0f);
    //    DamagePosX = this.gameObject.transform.position.x;
    //    DamagePosZ = this.gameObject.transform.position.z;

    //}

    //public void SetSabotageObject(GameObject setObject)
    //{
    //    sabotageObject = setObject;
    //}

}
