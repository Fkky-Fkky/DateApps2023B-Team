using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private Rigidbody rb;

    BossManager bossManager;

    [SerializeField] Animator AnimationImage = null;

    [SerializeField]
    private Transform damagePoint = null;

    [SerializeField]
    private GameObject explosionEffect = null;

    public bool isDamage = false;

    bool isKnockback = false;
    float knockbackTime = 0.0f;
    float knockbackTimeMax = 1.5f;


    BossMove bossMove;

    public BossCount bossCount;

    private float bossDestroyTime = 0.0f;
    private float bossDestroyTimeMax = 2.5f;

    [SerializeField]
    GameObject fellDownEffect;

    float effectPosY = -40.0f;

    private bool isInvincible = false;
    private float invincibleTime = 0.0f;
    [SerializeField]
    private float invincibleTimeMax = 4.0f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bossMove = GetComponent<BossMove>();
        isDamage = false;
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
                bossPos.z += 0.5f * Time.deltaTime;
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
            bossMove.bossHp--;
            if (bossMove.bossHp >= 1)
            {
                AnimationImage.SetTrigger("Damage");
            }
            else
            {
                AnimationImage.SetTrigger("Die");
            }
            isInvincible= true;
            bossMove.DamageFalse();
            isDamage = false;
        }

        if(isInvincible)
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
            bossCount.bossKillCount++;
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

    }
    public void KnockbackTrue( )
    {
        if (isKnockback)
            return;

        isKnockback = true;
        isDamage = true;
        bossMove.DamageTrue();
    }

    public void KnockbackTrueSub()
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

}
