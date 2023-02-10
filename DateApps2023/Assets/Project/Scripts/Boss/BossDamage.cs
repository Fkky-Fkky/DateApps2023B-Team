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


    bool isKnockback = false;
    float knockbackTime = 0.0f;
    float knockbackTimeMax = 1.5f;

    public bool isBossFellDown;



    private float bossDestroyTime = 0.0f;
    private float bossDestroyTimeMax = 2.5f;

    public bool isTrance = false; 

    private float tranceTime = 0.0f;
    [SerializeField]
    private float tranceTimeMax;

    private bool isBullet = false;

    [SerializeField]
    GameObject hpBarObject= null;


    [SerializeField]
    Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        bossMove = GetComponent<BossMove>();

        isDamage = false;

        hpBarObject.SetActive(true);

        hpBar.value = bossMove.bossHp;

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
            Instantiate(explosionEffect, damagePoint.position, Quaternion.identity);

            if (!isBullet)
            {
                bossMove.bossHp--;
            }
            else
            {
                bossMove.bossHp -= 2;
            }
            hpBar.value = bossMove.bossHp;
            if (bossMove.bossHp >= 1)
            {
                AnimationImage.SetTrigger("Damage");
            }
            else
            {
                AnimationImage.SetTrigger("Die");
            }
            isInvincible= true;
            isBullet = false;
            isDamage = false;
        }

        if (isInvincible)
        {
            invincibleTime += Time.deltaTime;
            if(invincibleTime>=invincibleTimeMax)
            {
                isInvincible= false;
                invincibleTime = 0.0f;
            }
        }

        if (bossMove.bossHp <= 0)
        {
            //bossCount.bossKillCount++;
            isBossFellDown = true;
            bossCount.SetBossKillCount();
            bossDestroyTime += Time.deltaTime;
            if (bossDestroyTime >= bossDestroyTimeMax)
            {
                Vector3 pos = new Vector3(transform.position.x, effectPosY, transform.position.z);
                Instantiate(fellDownEffect, pos, Quaternion.identity);
                Destroy(gameObject);
                bossDestroyTime = 0.0f;
            }
        }

        Trance();
    }

    private void Trance() {
        if (isTrance)
        {
            tranceTime += Time.deltaTime;
            if (tranceTime >= tranceTimeMax)
            {
                bossMove.DamageFalse();
                tranceTime = 0.0f;
            }
        }
    }

    public void KnockbackTrue()
    {
        if (isKnockback)
            return;

        if (!isInvincible)
        {
            isKnockback = true;
            isDamage = true;
            bossMove.DamageTrue();
        }

    }
    public void KnockbackTrueTwo()
    {
        if (isKnockback)
            return;

        if (!isInvincible)
        {
            isKnockback = true;
            isDamage = true;
            isBullet = true;
            bossMove.DamageTrue();
        }

    }

    public bool IsDamage()
    {
        return isDamage;
    }

}
