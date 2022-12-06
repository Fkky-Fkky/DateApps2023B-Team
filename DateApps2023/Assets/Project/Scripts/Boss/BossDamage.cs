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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        knockBackFlag = false;
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
            }
        }
        else
        {
            time = 0;

            boss.transform.position = new Vector3(
                0.0f,
                boss.transform.position.y,
                boss.transform.position.z + knockBackPower * Time.deltaTime);

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
            Destroy(explosionEffect);
        }
    }


    public void KnockbackTrue()
    {
        if (knockBackFlag)
            return;

        knockBackFlag = true;
        damageFlag = true;
        //DamegeをONにする処理
        AnimationImage.SetBool("Damage", true);
    }

    public void KnockbackFalse()
    {
        if(!knockBackFlag)
            return;

        knockBackFlag = false;
        //DamegeをOFFにする処理
        AnimationImage.SetBool("Damage", false);
    }

}
