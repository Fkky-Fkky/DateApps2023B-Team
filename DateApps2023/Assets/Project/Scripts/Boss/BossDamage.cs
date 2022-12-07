using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private GameObject boss;

    public bool knockBackFlag { get; private set; }

    [SerializeField]
    [Tooltip("m換算？")]
    private float knockBackPower = 300.0f;

    [SerializeField]
    [Tooltip("ノックバック後硬直時間")]
    private float stopTime = 5.0f;

    float time = 0;

    [SerializeField] Animator AnimationImage = null;

    [SerializeField]
    private Transform damagePoint = null;

    [SerializeField]
    private GameObject explosionEffect = null;

    private bool damageFlag = false;
    private bool firstEffect = true;

    [SerializeField]
    private float damageIntervalTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        knockBackFlag = false;
        damageFlag = false;
        firstEffect = true;

        time = stopTime;
        //animationController = transform.GetChild(2).GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!knockBackFlag)
        {
            time += Time.deltaTime;
            if (time <= stopTime)
            {
                boss.transform.position = new Vector3(
                    0.0f,
                    boss.transform.position.y,
                    boss.transform.position.z);
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                time = 0;

            }
        }
        else
        {
            time += Time.deltaTime;

            if(time >= damageIntervalTime)
            {
                AnimationImage.SetBool("Damage", true);
                if (firstEffect)
                {
                    damageFlag = true;
                    firstEffect = false;
                }

                boss.transform.position = new Vector3(
                    0.0f,
                    boss.transform.position.y,
                    boss.transform.position.z + knockBackPower * Time.deltaTime
                    );
            }

        

        }

        if (damageFlag)
        {
            Instantiate(explosionEffect, damagePoint.position, Quaternion.identity);
            damageFlag = false;
        }

        if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("Damege") && AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f) 
        {
            AnimationImage.SetBool("Damage", false);
            damageFlag = false;
            knockBackFlag = false;
            firstEffect = true;
            GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("BoomEffect");
            foreach (GameObject clone_explosionEffect in cloneItem)
            {
                Destroy(clone_explosionEffect);
            }

        }
    }


    public void KnockbackTrue()
    {
        if (knockBackFlag)
            return;

        time = 0;
        knockBackFlag = true;
        //DamegeをONにする処理
    }

    //public void KnockbackFalse()
    //{
    //    if(!knockBackFlag)
    //        return;

    //    time = 0;
    //    knockBackFlag = false;
    //    //DamegeをOFFにする処理
    //    AnimationImage.SetBool("Damage", false);
    //}

}
