using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour
{
    public BossCount bossCount = null;
    BossMove bossMove;

    [SerializeField] Animator AnimationImage = null;

    [SerializeField]
    private Transform damagePoint = null;

    [SerializeField]
    private GameObject explosionEffect = null;
    [SerializeField]
    GameObject fellDownEffect;

    float effectPosY = -40.0f;

    private bool isInvincible = false;
    private float invincibleTime = 0.0f;
    [SerializeField]
    private float invincibleTimeMax = 4.0f;

    private bool isDamage = false;

    private bool isBossDamage = false;

    private float BossDamgeOffTime    = 0.0f;
    private float BossDamgeOffTimeMax = 0.6f;


    bool isKnockback = false;
    float knockbackTime = 0.0f;
    float knockbackTimeMax = 1.5f;

    private bool isBossFellDown = false;

    private float bossDestroyTime = 0.0f;
    private float bossDestroyTimeMax = 2.5f;

    public bool isTrance = false; 

    private float tranceTime = 0.0f;
    [SerializeField]
    private float tranceTimeMax;

    [SerializeField]
    private Transform stunPos;

    [SerializeField]
    private GameObject stunEffct;

    private List<GameObject> stunEffectList = new List<GameObject>();
    private int stunEffectCount = 0;


    private int isBullet = -1;


    [SerializeField]
    private Slider hpBarGreen;

    ////[SerializeField]
    //private Slider hpBarRed;


    private float smallDamage = 0.5f;

    private float smallCounterDamage = 1.0f;

    private float MediumDamage = 1.0f;

    private float MediumCounterDamage = 2.0f;

    private float LargeDamage = 2.0f;

    private float LargeCounterDamage = 4.0f;


    void Start()
    {
        bossMove = GetComponent<BossMove>();


        isDamage = false;

        hpBarGreen.value = bossMove.bossHp;
        //hpBarRed.value   = bossMove.bossHp;
    }

    // Update is called once per frame
    void Update()
    {

        if (isKnockback)
        {
            knockbackTime += Time.deltaTime;
            if (knockbackTime <= knockbackTimeMax)
            {
                Vector3 bossPos = transform.position;
                bossPos.z += 3.0f * Time.deltaTime;
                transform.position = bossPos;
            }
            else
            {
                knockbackTime= 0.0f;
                isKnockback = false;
            }
        }

        if (isDamage)
        {
            if (!bossMove.IsAppearance())
            {
                if (!isTrance)
                {
                    Instantiate(explosionEffect, damagePoint.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(null, damagePoint.position, Quaternion.identity);
                }
                IsBullet();

                hpBarGreen.value = bossMove.bossHp;




                if (bossMove.bossHp > 0.0f)
                {
                    AnimationImage.SetTrigger("Damage");
                }
                else
                {
                    AnimationImage.SetTrigger("Die");
                }

                isInvincible = true;
                isBullet = -1;
                isDamage = false;
            }
        }
        

        if (isInvincible)
        {
            invincibleTime += Time.deltaTime;
            if(invincibleTime>=invincibleTimeMax)
            {
                isInvincible= false;
                bossMove.DamageFalse();
                invincibleTime = 0.0f;
            }
        }

        if (bossMove.bossHp <= 0.0f)
        {
            isBossFellDown = true;
            bossDestroyTime += Time.deltaTime;
            if (bossDestroyTime >= bossDestroyTimeMax)
            {
                Vector3 pos = new Vector3(transform.position.x, effectPosY, transform.position.z);
                Instantiate(fellDownEffect, pos, Quaternion.identity);
                StunEffectDestroy();
                Destroy(gameObject);
                bossDestroyTime = 0.0f;
            }
        }

        if (isBossDamage && !isTrance)
        {
            BossDamgeOffTime += Time.deltaTime;
            if (BossDamgeOffTime >= BossDamgeOffTimeMax)
            {
                isBossDamage = false;
                BossDamgeOffTime = 0.0f;
            }
        }

        Trance();
    }

    private void IsBullet()
    {
        if (isBullet == 0)
        {
            if (!isTrance)
            {
                bossMove.bossHp -= smallDamage;
            }
            else
            {
                bossMove.bossHp -= smallCounterDamage;
            }
        }
        else if (isBullet == 1)
        {
            if (!isTrance)
            {
                bossMove.bossHp -= MediumDamage;
            }
            else
            {
                bossMove.bossHp -= MediumCounterDamage;
            }
        }
        else if (isBullet == 2)
        {
            if (!isTrance)
            {
                bossMove.bossHp -= LargeDamage;
            }
            else
            {
                bossMove.bossHp -= LargeCounterDamage;
            }

        }

    }

    private void Trance() {
        if (isTrance)
        {
            AnimationImage.SetBool("Trance", true);


            if (stunEffectCount <= 0)
            {
                stunEffectList.Add(Instantiate(stunEffct, stunPos.position, Quaternion.identity));
                stunEffectCount++;
            }
            tranceTime += Time.deltaTime;
            if (tranceTime >= tranceTimeMax)
            {
                bossMove.DamageFalse();
                AnimationImage.SetBool("Trance", false);

                StunEffectDestroy();

                stunEffectCount = 0;
                isBossDamage = false;
                isTrance = false;
                tranceTime = 0.0f;
            }
        }

    }

    private void StunEffectDestroy()
    {
        for (int i = 0; i < stunEffectList.Count; i++)
        {
            Destroy(stunEffectList[i]);
            stunEffectList.RemoveAt(i);
        }

    }

    public void KnockbackTrueSmall()
    {
        DamageKnockBack(0);

    }
    public void KnockbackTrueMedium()
    {
        DamageKnockBack(1);

    }

    public void KnockbackTrueLarge()
    {

        DamageKnockBack(2);

    }

    void DamageKnockBack(int Bullet)
    {
        if (isKnockback)
            return;

        if (!bossMove.IsAppearance())
        {
            if (!isInvincible)
            {
                isKnockback = true;
                isDamage = true;
                isBossDamage = true;
                isBullet = Bullet;
                bossMove.DamageTrue();
            }
        }

    }

    public bool IsFellDown()
    {
        return isBossFellDown;
    }

    public bool IsDamage()
    {
        return isDamage;
    }

    public bool IsBossDamage()
    {
        return isBossDamage;
    }
}