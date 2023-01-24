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
    public GameObject beamObject;

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
    float areaDestroyTimeMax = 0.5f;

    public bool isAttack;

    private void Start()
    {
        areaCount= 0;
    }

    void Update()
    {
        time+= Time.deltaTime;
        if (time >= attackIntervalTime)
        {
            damageTime += Time.deltaTime;
            if (damageTime >= damageTimeMax)
            {
                //attackAnimation.SetBool("Attack", true);
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
            //attackAnimation.SetBool("Attack", false);
            damageTime = 0.0f;
            areaCount = 0;
            areaDestroyTime = 0.0f;
            time = 0.0f;
        }

    }

}
