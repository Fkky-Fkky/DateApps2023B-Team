using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossAttack : MonoBehaviour
{

    float time = 0.0f;

    [SerializeField]
    float attackIntervalTime = 20.0f;


    [SerializeField]
    Animator attackAnimation = null;


    float centerTarget = 0.0f;
    float rightTarget = 0.1f;
    float leftTarget = -0.1f;


    [SerializeField]
    GameObject dmageAreaCenter;
    [SerializeField]
    GameObject damageAreaRight;
    [SerializeField]
    GameObject damageAreaLeft;

    int areaCount;
    int areaCountMax = 1;

    float areaDestroyTime    = 0.0f;
    float areaDestroyTimeMax = 3.0f;

    public bool isAttack;

    private float beamTime    = 0.0f;
    private float beamTimeMax = 2.0f;


    float animationtime = 0.0f;
    float animationtimeMax = 2.0f;


    public BossMove bossMove;

    public BossDamage bossDamage;

    private void Start()
    {
        areaCount= 0;
        isAttack = false;
    }

    void Update()
    {
        time+= Time.deltaTime;
        if (!bossDamage.IsDamage())
        {
            AttackAnimation();
        }
        else
        {
            isAttack = false;
        }
    }

    void AttackAnimation()
    {
        if (bossMove.bossHp > 0)
        {
            if (time >= attackIntervalTime)
            {
                attackAnimation.SetTrigger("Stand-By");

                animationtime += Time.deltaTime;
                if (animationtime >= animationtimeMax)
                {
                    attackAnimation.SetTrigger("Attack");

                    beamTime+= Time.deltaTime;
                    if (beamTime >= beamTimeMax)
                    {
                        isAttack = true;
                        DamageAreaControl();
                    }
                }
            }
        }
        else
        {
            isAttack = false;
        }
    }

    void DamageAreaControl()
    {
        if (gameObject.transform.position.x == centerTarget)
        {
            if (areaCount < areaCountMax)
            {
                Instantiate(dmageAreaCenter);
                areaCount++;
            }
        }

        if (gameObject.transform.position.x >= rightTarget)
        {
            if (areaCount < areaCountMax)
            {
                Instantiate(damageAreaRight);
                areaCount++;
            }
        }

        if (gameObject.transform.position.x <= leftTarget)
        {
            if (areaCount < areaCountMax)
            {
                Instantiate(damageAreaLeft);
                areaCount++;
            }
        }
        areaDestroyTime += Time.deltaTime;
        if (areaDestroyTime >= areaDestroyTimeMax)
        {
            isAttack= false;
            areaDestroyTime = 0.0f;
            animationtime = 0.0f;
            areaCount = 0;
            time = 0.0f;
        }

    }

}
