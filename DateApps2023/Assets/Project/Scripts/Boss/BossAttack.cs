using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private Animator attackAnimation = null;

    [SerializeField]
    private GameObject dmageAreaCenter = null;
    [SerializeField]
    private GameObject damageAreaRight = null;
    [SerializeField]
    private GameObject damageAreaLeft  = null;

    [SerializeField]
    private Transform chargePos     = null;
    [SerializeField]
    private GameObject chargeEffect = null;
    [SerializeField]
    private float chageTimeMax      = 10.0f;

    [SerializeField]
    private GameObject dangerZone = null;

    [SerializeField]
    private BossMove bossMove     = null;
    [SerializeField]
    private BossDamage bossDamage = null;

    [SerializeField]
    private AudioClip beamSE = null;


    private float time               = 0.0f;
    private float attackIntervalTime = 0.0f;

    private const float centerTarget =  0.0f;
    private const float rightTarget  =  0.1f;
    private const float leftTarget   = -0.1f;

    public bool IsCharge { get; private set; }
    private float chargeTime = 0.0f;

    private int effectStop              = 0;
    private List<GameObject> effectList = new List<GameObject>();

    private Vector3 dangerCenter = new Vector3(  0.0f, -1.2f, 0.0f);
    private Vector3 dangerLeft   = new Vector3(-10.0f, -1.2f, 0.0f);
    private Vector3 dangerRigth  = new Vector3( 10.0f, -1.2f, 0.0f);

    private List<GameObject> dangerAreaList = new List<GameObject>();
    private const float dangerObjectAngleY  = 180.0f;

    private int areaCount          = 0;
    private const int areaCountMax = 1;

    private float beamOffTime          = 0.0f;
    private const float beamOffTimeMax = 2.0f;

    public bool IsAttack     = false;
    private bool isAttackAll = false;

    private AudioSource audioSource = null;
    private int seCount             = 0;
    private const int seCountMax    = 1;

    private BossCSVGenerator bossCSVGenerator = null;

    private void Start()
    {
        bossCSVGenerator = GameObject.Find("BossGenerator").GetComponent<BossCSVGenerator>();
        audioSource = GetComponent<AudioSource>();

        attackIntervalTime = bossCSVGenerator.AttackIntervalTime();
        IsCharge = false;
    }
    
    void Update()
    {
        if (!bossMove.IsAttackOff())
        {
            if (!bossDamage.IsInvincible)
            {
                Attack();
            }
        }

        if (bossMove.BossHp <= 0 || bossDamage.IsInvincible)
        {
            AttackOff();

            ListDestroy(effectList);
            ListDestroy(dangerAreaList);
        }
    }

    private void Attack()
    {
        time += Time.deltaTime;
        if (bossMove.BossHp > 0 && !bossDamage.IsDamage())
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
            IsCharge = true;
            effectStop++;
        }
        else
        {
            IsCharge = false;
        }
    }

    private void DangerZone()
    {
        switch (gameObject.tag)
        {
            case "Center":
                dangerAreaList.Add(Instantiate(dangerZone, dangerCenter, Quaternion.Euler(0.0f, dangerObjectAngleY, 0.0f)));
                break;
            case "Left":
                dangerAreaList.Add(Instantiate(dangerZone, dangerLeft, Quaternion.Euler(0.0f, dangerObjectAngleY, 0.0f)));
                break;
            case "Right":
                dangerAreaList.Add(Instantiate(dangerZone, dangerRigth, Quaternion.Euler(0.0f, dangerObjectAngleY, 0.0f)));
                break;
        }
    }

    void AttackAnimation()
    {
         chargeTime += Time.deltaTime;
        if (chargeTime >= chageTimeMax)
        {
            IsAttack = true;
            DamageAreaControl();
        }
        else if (chargeTime < chageTimeMax && bossDamage.IsBossDamage())
        {
            AttackOff();
            ListDestroy(effectList);
            ListDestroy(dangerAreaList);
        }
    }

    void ListDestroy(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
            list.RemoveAt(i);
        }
    }
    void DamageAreaControl()
    {
        if (seCount < seCountMax)
        {
            audioSource.PlayOneShot(beamSE);
            seCount++;
        }

        if (gameObject.transform.position.x == centerTarget)
        {
            DamageObject(dmageAreaCenter);
        }
        if (gameObject.transform.position.x >= rightTarget)
        {
            DamageObject(damageAreaRight);
        }
        if (gameObject.transform.position.x <= leftTarget)
        {
            DamageObject(damageAreaLeft);
        }

        beamOffTime += Time.deltaTime;
        if (beamOffTime >= beamOffTimeMax)
        {
            AttackOff();
        }
    }

    private void DamageObject(GameObject damageArea)
    {
        if (areaCount < areaCountMax)
        {
            Instantiate(damageArea);
            areaCount++;
        }
    }

    private void AttackOff()
    {
        IsAttack = false;
        isAttackAll = false;
        effectStop = 0;
        attackAnimation.SetBool("Attack", false);
        seCount = 0;
        chargeTime = 0.0f;
        beamOffTime = 0.0f;
        areaCount = 0;
        time = 0.0f;
    }

    public float BeamTimeMax()
    {
        return chageTimeMax;
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