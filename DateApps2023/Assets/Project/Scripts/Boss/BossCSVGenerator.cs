using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCSVGenerator : MonoBehaviour
{
    public BossManager bossManager;
    private BossCount bossCount;
    private opretar opretar;

    [SerializeField]
    private BossCSV bossCSV = null;

    [SerializeField]
    private GameObject nomalBoss;

    [SerializeField]
    private GameObject miniBoss;

    [SerializeField]
    private GameObject bigBoss;


    private GameObject boss;

    private string bossTypeDate;
    private int bossLane = 0;
    private float attackIntervalDate;
    private float posZ;
    private int bossHpDate;
    private float moveSpeedDate;

    private float centerPosX =    0.0f;
    private float leftPosX   = -100.0f;
    private float rightPosX  =  100.0f;

    private float fallPosition = 500.0f;

    private float time = 0.0f;

    private int bossCountOne = 1;
    [SerializeField]
    private int bossCountMax = 10;

    private bool isCenterLine = false;
    private bool isRightLine = false;
    private bool isLeftLine = false;

    private List<BossDamage> bossList = new List<BossDamage>();

    private void Awake()
    {
        opretar = GameObject.Find("opretar").GetComponent<opretar>();
    }

    void Start()
    {
        bossCountOne  = 1;
        nomalBoss.tag = "Boss";

        isCenterLine = false;
        isRightLine  = false;
        isLeftLine   = false;

        bossCount = GetComponent<BossCount>();
    }

    void Update()
    {
        
        time += Time.deltaTime;
        if (time >= bossCSV.appearanceTime[bossCountOne])
        {
            bossTypeDate = bossCSV.bossType[bossCountOne];
            bossLane = bossCSV.appearanceLane[bossCountOne];
            attackIntervalDate = bossCSV.attackIntervalTime[bossCountOne];
            posZ = bossCSV.positionZ[bossCountOne];
            bossHpDate = bossCSV.bossHp[bossCountOne];
            moveSpeedDate = bossCSV.bossSpeed[bossCountOne];



            BossTypeGanarate();
        }

        if (bossCountOne >= bossCountMax)
        {
            bossCountOne = bossCountMax;
        }

        for (int i = 0; i < bossList.Count; i++)
        {
            if (bossList[i].IsFellDown())
            {
                bossCount.SetBossKillCount();
                bossList.RemoveAt(i);
            }
        }
    }

    private void BossTypeGanarate()
    {
        BossLane();
    }

    private void BossType()
    {
        switch (bossTypeDate)
        {
            case "Nomal":
                boss = Instantiate(nomalBoss);
                opretar.summonboss();
                break;
            case "Mini":
                boss = Instantiate(miniBoss);
                opretar.summonminiboss();
                break;
            case "Big":
                boss = Instantiate(bigBoss);
                opretar.summonbigboss();
                break;
        }
    }

    private void BossLane()
    {
        switch (bossLane)
        {
            case 1:
                if (!isLeftLine)
                {
                    BossType();
                    boss.transform.position = new Vector3(leftPosX, fallPosition, posZ);
                    bossList.Add(boss.GetComponent<BossDamage>());
                    bossCountOne++;
                    time = 0.0f;
                    isLeftLine = true;
                }
                break;
            case 2:
                if (!isCenterLine)
                {
                    BossType();
                    boss.transform.position = new Vector3(centerPosX, fallPosition, posZ);
                    bossList.Add(boss.GetComponent<BossDamage>());
                    bossCountOne++;
                    time = 0.0f;
                    isCenterLine = true;
                }
                break;
            case 3:
                if (!isRightLine)
                {
                    BossType();
                    boss.transform.position = new Vector3(rightPosX, fallPosition, posZ);
                    bossList.Add(boss.GetComponent<BossDamage>());
                    bossCountOne++;
                    time = 0.0f;
                    isRightLine = true;
                }
                break;
        }
    }

    public void IsCenterLineFalse()
    {
        isCenterLine = false;
    }

    public void IsCenterLineTrue()
    {
        isCenterLine = true;
    }


    public void IsLeftLineFalse()
    {
        isLeftLine = false;
    }

    public void IsLeftLineTrue()
    {
        isLeftLine = true;
    }

    public void IsRightLineFalse()
    {
        isRightLine = false;
    }

    public void IsRightLineTrue()
    {
        isRightLine = true;
    }

    public float AttackIntervalTime()
    {
        return attackIntervalDate;
    }

    public int BossHP()
    {
        return bossHpDate;
    }

    public float BossMoveSpeed()
    {
        return moveSpeedDate;
    }
}