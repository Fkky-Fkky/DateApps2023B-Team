using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossAttack : MonoBehaviour
{

    float time = 0.0f;

    private float attackIntervalTime;

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
    private Transform chargePos;


    [SerializeField]
    private GameObject chargeEffect;
    private int effectStop = 0;

    private List<GameObject> effectList = new List<GameObject>();

    [SerializeField]
    private GameObject dangerZone;

    private Vector3 dangerCenter = new Vector3(  0.0f, 1.0f, 0.0f);
    private Vector3 dangerLeft   = new Vector3(-10.0f, 1.0f, 0.0f);
    private Vector3 dangerRigth  = new Vector3( 10.0f, 1.0f, 0.0f);

    private List<GameObject> dangerAreaList = new List<GameObject>();

    private float dangerAngle = 180.0f;

    int areaCount;
    int areaCountMax = 1;

    float beamOffTime    = 0.0f;
    float beamOffTimeMax = 2.0f;

    public bool isAttack = false;

    private bool isAttackAll = false;

    private float beamTime    = 0.0f;
    [SerializeField]
    private float beamTimeMax = 10.0f;

    public BossMove bossMove;

    public BossDamage bossDamage;

    private BossCSVGenerator bossCSVGenerator;

    private void Start()
    {
        areaCount= 0;
        isAttack = false;
        bossCSVGenerator = GameObject.Find("BossGenerator").GetComponent<BossCSVGenerator>();

        attackIntervalTime = bossCSVGenerator.AttackIntervalTime();
    }

    void Update()
    {
        if (!bossMove.IsAttackOff())
        {
            if (!bossDamage.isTrance)
            {
                Attack();
            }
        }

        if (bossDamage.isTrance || bossMove.bossHp <= 0)
        {
            for (int i = 0; i < effectList.Count; i++)
            {
                Destroy(effectList[i]);
                effectList.RemoveAt(i);
            }

            for (int i = 0; i < dangerAreaList.Count; i++)
            {
                Destroy(dangerAreaList[i]);
                dangerAreaList.RemoveAt(i);
            }
        }

    }

    private void Attack()
    {
        time += Time.deltaTime;
        if (bossMove.bossHp > 0 && !bossDamage.IsDamage())
        {
            if (time >= attackIntervalTime)
            {
                isAttackAll = true;
                attackAnimation.SetBool("Attack", true);
                Charge();
                AttackAnimation();
            }
        }

    }

    private void Charge()
    {
        if (effectStop < 1)
        {
            effectList.Add(Instantiate(chargeEffect, chargePos.position, Quaternion.identity));

            DangerZone();
            effectStop++;
        }

    }


    private void DangerZone()
    {
        if (gameObject.tag == "Center")
        {
            dangerAreaList.Add(Instantiate(dangerZone, dangerCenter, Quaternion.Euler(0.0f, dangerAngle, 0.0f)));
        }

        if (gameObject.tag == "Left")
        {
            dangerAreaList.Add(Instantiate(dangerZone, dangerLeft,   Quaternion.Euler(0.0f, dangerAngle, 0.0f)));
        }

        if (gameObject.tag == "Right")
        {
            dangerAreaList.Add(Instantiate(dangerZone, dangerRigth, Quaternion.Euler(0.0f, dangerAngle, 0.0f)));
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
        else if (beamTime < beamTimeMax&& bossDamage.IsBossDamage())
        {
            AttackOff();
            bossDamage.isTrance = true;
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
        beamOffTime += Time.deltaTime;
        if (beamOffTime >= beamOffTimeMax)
        {
            AttackOff();
        }

    }

    private void AttackOff()
    {
        isAttack = false;
        isAttackAll = false;
        effectStop = 0;
        attackAnimation.SetBool("Attack", false);
        beamTime = 0.0f;
        beamOffTime = 0.0f;
        areaCount = 0;
        time = 0.0f;

    }

    public float BeamTimeMax()
    {
        return beamTimeMax;
    }

    public float BeamOffTimeMax()
    {
        return beamOffTimeMax;
    }

    public bool IsAttackAll()
    {
        return isAttackAll;
    }
}