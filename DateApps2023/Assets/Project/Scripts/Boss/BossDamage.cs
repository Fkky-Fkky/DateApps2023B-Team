using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour
{

    [SerializeField] Animator AnimationImage = null;

    [SerializeField]
    private Transform damagePoint = null;

    [SerializeField]
    private GameObject explosionEffect = null;
    [SerializeField]
    private GameObject fellDownEffect = null;

    [SerializeField]
    private float invincibleTimeMax = 4.0f;

    [SerializeField]
    private GameObject hpCores = null;

    [SerializeField]
    private GameObject[] hpBar = new GameObject[9];

    [SerializeField]
    private GameObject[] hpMemori = new GameObject[9];

    public BossCount BossCount = null;
    private BossMove bossMove = null;

    private DamageCSV damageCSV = null;

    private const float effectPosY = -40.0f;

    public bool IsInvincible { get; private set; }
    private float invincibleTime = 0.0f;

    private bool isDamage = false;
    private bool isBossDamage = false;

    private float BossDamgeOffTime    = 0.0f;
    private const float BossDamgeOffTimeMax = 0.6f;

    private bool isKnockback = false;
    private float knockbackTime = 0.0f;
    private const float knockbackTimeMax = 1.5f;

    private bool isBossFellDown = false;

    private float bossDestroyTime = 0.0f;
    private const float bossDestroyTimeMax = 2.5f;

    private int isBullet = -1;
    private int maxHp = 0;

    private int smallDamage = 0;
    private int MediumDamage = 0;
    private int LargeDamage = 0;

    void Start()
    {
        bossMove = GetComponent<BossMove>();

        damageCSV = GameObject.Find("BossManager").GetComponent<DamageCSV>();

        smallDamage  = damageCSV.Small;
        MediumDamage = damageCSV.Medium;
        LargeDamage  = damageCSV.Large;

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
                if (gameObject.transform.localScale.y < 18.0f)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.6f, 0.7f, 0);
                }
                if (gameObject.transform.localScale.y > 18.0f)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.8f, -0.7f, 0);
                }
                break;
            case 2:
                if (gameObject.transform.localScale.y > 18.0f)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.6f, -0.7f, 0);
                }
                break;
            case 3:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.4f, -0.7f, 0);
                break;
            case 4:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.2f, -0.7f, 0);
                break;
            case 7:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.4f, 0, 0);
                break;
            case 8:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.2f, 0, 0);
                break;
        }

    }

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
                knockbackTime = 0.0f;
                isKnockback = false;
            }
        }

        if (isDamage)
        {
            if (!bossMove.IsAppearance())
            {
                Instantiate(explosionEffect, damagePoint.position, Quaternion.identity);
                IsBullet();
                DamageAnimation();

                IsInvincible = true;
                isBullet = -1;
                isDamage = false;
            }
        }

        if (IsInvincible)
        {
            invincibleTime += Time.deltaTime;
            if (invincibleTime >= invincibleTimeMax)
            {
                IsInvincible = false;
                AnimationImage.SetTrigger("Walk");
                bossMove.DamageFalse();
                invincibleTime = 0.0f;
            }
        }

        if (bossMove.BossHp <= 0)
        {
            bossDestroyTime += Time.deltaTime;
            if (bossDestroyTime >= bossDestroyTimeMax)
            {
                isBossFellDown = true;
                Vector3 pos = new Vector3(transform.position.x, effectPosY, transform.position.z);
                Instantiate(fellDownEffect, pos, Quaternion.identity);
                Destroy(gameObject);
                bossDestroyTime = 0.0f;
            }
        }

        if (isBossDamage)
        {
            BossDamgeOffTime += Time.deltaTime;
            if (BossDamgeOffTime >= BossDamgeOffTimeMax)
            {
                isBossDamage = false;
                BossDamgeOffTime = 0.0f;
            }
        }
    }

    private void IsBullet()
    {
        if (isBullet == 0)
        {
            Damage(smallDamage);
            hpBar[bossMove.BossHp + 0].SetActive(false);
        }
        else if (isBullet == 1)
        {
            Damage(MediumDamage);
            HpBarMediumActive();
        }
        else if (isBullet == 2)
        {
            Damage(LargeDamage);
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
        for (int i = 0; i < MediumDamage; i++)
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
            AnimationImage.SetTrigger("Damage");
        }
        else
        {
            AnimationImage.SetTrigger("Die");
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
            if (!IsInvincible)
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