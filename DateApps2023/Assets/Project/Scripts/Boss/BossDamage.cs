using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private Rigidbody rb;

    public bool knockBackFlag { get; private set; }

    //[SerializeField]
    //[Tooltip("mä∑éZÅH")]
    //private float knockBackPower = 300.0f;

    [SerializeField]
    [Tooltip("ÉmÉbÉNÉoÉbÉNå„çdíºéûä‘")]
    private float stopTime = 5.0f;

    float time = 0;

    [SerializeField] Animator AnimationImage = null;

    [SerializeField]
    private Transform damagePoint = null;

    [SerializeField]
    private GameObject explosionEffect = null;

    private bool damageFlag = false;
    private bool firstEffect = true;

    private float PosX;
    private float PosZ;

    BossMove bossMove;

    public BossCount bossCount;

    private float bossDestroyTime = 0.0f;
    private float bossDestroyTimeMax = 2.3f;

    [SerializeField]
    GameObject fellDownEffect;
    //[SerializeField]
    //private float damageIntervalTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bossMove = GetComponent<BossMove>();
        knockBackFlag = false;
        damageFlag = false;
        firstEffect = true;

        time = stopTime;
        //animationController = transform.GetChild(2).GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackFlag)
        {
            time += Time.deltaTime;
            if(time <= stopTime)
            {
                AnimationImage.SetBool("Damage", true);

                if (firstEffect)
                {
                    damageFlag = true;
                    firstEffect = false;
                }

                this.gameObject.transform.position = new Vector3(PosX, this.gameObject.transform.position.y, PosZ);
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else
            {
                damageFlag = false;
                bossMove.DamageFalse();
                GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("BoomEffect");
                foreach (GameObject clone_explosionEffect in cloneItem)
                {
                    Destroy(clone_explosionEffect);
                }
                firstEffect = true;
                knockBackFlag = false;
                time = 0;
            }
        }

        if (damageFlag)
        {
            Instantiate(explosionEffect, damagePoint.position, Quaternion.identity);
            bossMove.bossHp--;
            damageFlag = false;
        }

        if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("Damege") && AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f) 
        {
            AnimationImage.SetBool("Damage", false);
            //damageFlag = false;
            //knockBackFlag = false;
            //time = 0;
            //firstEffect = true;
            GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("BoomEffect");
            foreach (GameObject clone_explosionEffect in cloneItem)
            {
                Destroy(clone_explosionEffect);
            }
        }

        if (bossMove.bossHp <= 0)
        {
            bossCount.bossKillCount++;
            AnimationImage.SetTrigger("Die");
            bossDestroyTime += Time.deltaTime;
            if (bossDestroyTime >= bossDestroyTimeMax)
            {
                Instantiate(fellDownEffect, transform.position, Quaternion.identity);
                Destroy(gameObject.gameObject);
                bossDestroyTime = 0.0f;
            }
        }

    }


    public void KnockbackTrue()
    {
        if (knockBackFlag)
            return;

        time = 0;
        knockBackFlag = true;
        bossMove.DamageTrue();
        PosX = this.gameObject.transform.position.x;
        PosZ = this.gameObject.transform.position.z;
    }


}
