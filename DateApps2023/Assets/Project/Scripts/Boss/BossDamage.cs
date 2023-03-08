using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour
{
    [SerializeField]
    private float invincibleTimeMax = 4.0f;

    [SerializeField]
    private GameObject explosionEffect = null;
    [SerializeField]
    private GameObject fellDownEffect  = null;

    [SerializeField]
    private GameObject hpCores    = null;
    [SerializeField]
    private GameObject[] hpBar    = new GameObject[9];
    [SerializeField]
    private GameObject[] hpMemori = new GameObject[9];

    [SerializeField]
    private Animator animationImage = null;

    [SerializeField]
    private Transform damagePoint = null;

    private bool isKnockback    = false;
    private bool isDamage       = false;
    private bool isBossDamage   = false;
    private bool isBossFellDown = false;

    private int maxHp              =  0;
    private int bulletType         = -1;
    private int smallEnergyDamage  =  0;
    private int mediumEnergyDamage =  0;
    private int largeEnergyDamage  =  0;

    private float invincibleTime   = 0.0f;
    private float bossDamgeOffTime = 0.0f;
    private float knockbackTime    = 0.0f;
    private float bossDestroyTime  = 0.0f;

    private BossMove bossMove   = null;
    private DamageCSV damageCSV = null;

    public bool IsInvincible { get; private set; }

    const float EFFECT_POS_Y             = -40.0f;
    const float BOSS_DAMGE_OFF_TIME_MAX  =   0.6f;
    const float KNOCK_BACK_TIME_MAX      =   1.5f;
    const float KNOCK_BACK_MOVE          =   3.0f;
    const float BOSS_DESTROY_TIME_MAX    =   2.5f;
    const float BOSS_COMPARE_SCALE_INDEX =  18.0f;

    const float SMALL_BOSS_HP_POS_X     = 0.6f;
    const float NOMAL_BOSS_MIN_HP_POS_X = 0.8f;
    const float BOSS_MIN_HP_POS_X       = 0.4f;
    const float BOSS_MAX_HP_POS_X       = 0.2f;
    const float BOSS_HP_BAR_POS_Y       = 0.7f;

    void Start()
    {
        bossMove = GetComponent<BossMove>();

        damageCSV = GameObject.Find("BossManager").GetComponent<DamageCSV>();

        smallEnergyDamage  = damageCSV.Small;
        mediumEnergyDamage = damageCSV.Medium;
        largeEnergyDamage  = damageCSV.Large;

        IsInvincible = false;

        maxHp = bossMove.BossHp;

        hpBar = new GameObject[maxHp];

        for (int i = 0; i < hpMemori.Length; i++)
        {
            hpMemori[i].SetActive(false);
        }

        for (int i = 0; i < maxHp; i++)
        {
            hpBar[i] = hpMemori[i];
            hpMemori[i].SetActive(true);
        }

        switch (maxHp) 
        {
            case 1:
                if (gameObject.transform.localScale.y < BOSS_COMPARE_SCALE_INDEX)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(SMALL_BOSS_HP_POS_X, BOSS_HP_BAR_POS_Y, 0);
                }
                if (gameObject.transform.localScale.y > BOSS_COMPARE_SCALE_INDEX)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(NOMAL_BOSS_MIN_HP_POS_X, -BOSS_HP_BAR_POS_Y, 0);
                }
                break;
            case 2:
                if (gameObject.transform.localScale.y > BOSS_COMPARE_SCALE_INDEX)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(SMALL_BOSS_HP_POS_X, -BOSS_HP_BAR_POS_Y, 0);
                }
                break;
            case 3:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(BOSS_MIN_HP_POS_X, -BOSS_HP_BAR_POS_Y, 0);
                break;
            case 4:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(BOSS_MAX_HP_POS_X, -BOSS_HP_BAR_POS_Y, 0);
                break;
            case 7:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(BOSS_MIN_HP_POS_X, 0, 0);
                break;
            case 8:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(BOSS_MAX_HP_POS_X, 0, 0);
                break;
        }
    }

    void Update()
    {
        if (isKnockback)
        {
            knockbackTime += Time.deltaTime;
            if (knockbackTime <= KNOCK_BACK_TIME_MAX)
            {
                Vector3 bossPos = transform.position;
                bossPos.z += KNOCK_BACK_MOVE * Time.deltaTime;
                transform.position = bossPos;
            }
            else
            {
                knockbackTime = 0.0f;
                isKnockback = false;
            }
        }

        if (isDamage)
        {
            if (!bossMove.IsAppearance())
            {
                Instantiate(explosionEffect, damagePoint.position, Quaternion.identity);
                BulletTypeDamage();
                DamageAnimation();

                IsInvincible = true;
                bulletType = -1;
                isDamage = false;
            }
        }

        if (IsInvincible)
        {
            invincibleTime += Time.deltaTime;
            if (invincibleTime >= invincibleTimeMax)
            {
                IsInvincible = false;
                animationImage.SetTrigger("Walk");
                bossMove.DamageFalse();
                invincibleTime = 0.0f;
            }
        }

        if (bossMove.BossHp <= 0)
        {
            bossDestroyTime += Time.deltaTime;
            if (bossDestroyTime >= BOSS_DESTROY_TIME_MAX)
            {
                isBossFellDown = true;
                Vector3 pos = new Vector3(transform.position.x, EFFECT_POS_Y, transform.position.z);
                Instantiate(fellDownEffect, pos, Quaternion.identity);
                Destroy(gameObject);
                bossDestroyTime = 0.0f;
            }
        }

        if (isBossDamage)
        {
            bossDamgeOffTime += Time.deltaTime;
            if (bossDamgeOffTime >= BOSS_DAMGE_OFF_TIME_MAX)
            {
                isBossDamage = false;
                bossDamgeOffTime = 0.0f;
            }
        }
    }

    private void BulletTypeDamage()
    {
        if (bulletType == 0)
        {
            Damage(smallEnergyDamage);
            hpBar[bossMove.BossHp + 0].SetActive(false);
        }
        else if (bulletType == 1)
        {
            Damage(mediumEnergyDamage);
            HpBarMediumActive();
        }
        else if (bulletType == 2)
        {
            Damage(largeEnergyDamage);
            HpBarLargeActive();
        }
    }

    private void Damage(int damage)
    {
        bossMove.BossHp -= damage;
        if (bossMove.BossHp < 0)
        {
            bossMove.BossHp = 0;
        }
    }

    private void HpBarMediumActive()
    {
        for (int i = 0; i < mediumEnergyDamage; i++)
        {
            if (bossMove.BossHp + i < maxHp)
            {
                hpBar[bossMove.BossHp + i].SetActive(false);
            }
        }
    }
    private void HpBarLargeActive()
    {
        for(int i = 0; i < maxHp; i++)
        {
            hpBar[bossMove.BossHp + i].SetActive(false);
        }
    }

    private void DamageAnimation()
    {
        if (bossMove.BossHp > 0)
        {
            animationImage.SetTrigger("Damage");
        }
        else
        {
            animationImage.SetTrigger("Die");
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
        {
            return;
        }

        if (!bossMove.IsAppearance())
        {
            if (!IsInvincible)
            {
                isKnockback = true;
                isDamage = true;
                isBossDamage = true;
                bulletType = Bullet;
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