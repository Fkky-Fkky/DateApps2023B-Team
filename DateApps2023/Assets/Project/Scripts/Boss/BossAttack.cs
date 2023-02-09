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

    [SerializeField]
    private GameObject chargeEffect;
    private int effectStop = 0;
    private float effectY = -2.5f;
    private float effectZ = 2.0f;


    int areaCount;
    int areaCountMax = 1;

    float areaDestroyTime    = 0.0f;
    float areaDestroyTimeMax = 2.0f;

    public bool isAttack;

    private float beamTime    = 0.0f;
    [SerializeField]
    private float beamTimeMax = 2.0f;


    public BossMove bossMove;

    public BossDamage bossDamage;

    private void Start()
    {
        areaCount= 0;
        isAttack = false;

        effectStop = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (bossMove.bossHp > 0 && !bossDamage.IsDamage())
        {
            if (time >= attackIntervalTime)
            {
                attackAnimation.SetBool("Attack", true);
                Charge();
                AttackAnimation();
            }
        }
        else
        {
            isAttack = false;
        }
    }

    private void Charge()
    {
        if (effectStop < 1)
        {
            effectZ = transform.position.z - 50.0f;
            Vector3 pos = new Vector3(transform.position.x, effectY, effectZ);
            Instantiate(chargeEffect, pos, Quaternion.identity);
            effectStop++;
        }

    }
    void AttackAnimation()
    {

         beamTime += Time.deltaTime;
        if (beamTime >= beamTimeMax)
        {
            isAttack = true;
            DamageAreaControl();
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
            AttackOff();
        }

    }

    private void AttackOff()
    {
        isAttack = false;
        effectStop = 0;
        attackAnimation.SetBool("Attack", false);
        beamTime = 0.0f;
        areaDestroyTime = 0.0f;
        areaCount = 0;
        time = 0.0f;

    }
}
