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

    float damageTime = 0.0f;
    float damageTimeMax = 0.5f;

    int areaCount;
    int areaCountMax = 1;

    float areaDestroyTime = 0.0f;
    float areaDestroyTimeMax = 2.0f;

    public bool isAttack;

    float animationtime = 0.0f;
    float animationtimeMax = 1.5f;


    public BossMove bossMove;

    private void Start()
    {
        areaCount= 0;
        isAttack = false;
    }

    void Update()
    {
        time+= Time.deltaTime;

        if (bossMove.bossHp > 0)
        {
            if (time >= attackIntervalTime)
            {
                attackAnimation.SetTrigger("Attack");
                Attack();
            }
        }
        else
        {
            isAttack = false;
        }
    }

    void Attack()
    {
        damageTime += Time.deltaTime;
        if (damageTime >= damageTimeMax)
        {

            animationtime += Time.deltaTime;
            if (animationtime >= animationtimeMax)
            {
                isAttack = true;
                DamageAreaControl();
            }
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
            damageTime = 0.0f;
            areaCount = 0;
            time = 0.0f;
        }

    }

}
