using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BossCSVGenerator : MonoBehaviour
{
    public BossManager bossManager;
    private BossCount bossCount;

    [SerializeField]
    private BossCSV bossCSV = null;

    [SerializeField]
    private GameObject nomalBoss;

    [SerializeField]
    private GameObject miniBoss;

    [SerializeField]
    private GameObject bigBoss;

    private GameObject boss;

    private int bossType = 0;

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
    private List<BossMove> bossMoveList = new List<BossMove>();
    private List<BossAttack> bossAttackList = new List<BossAttack>();

    public bool IsFirstKill { get; private set; }
    public bool IsKill { get; private set; }

    public bool IsLanding { get; private set; }
    public bool IsDanger { get; private set; }

    public bool IsCharge { get; private set; }

    private float landingTime=0.0f;

    private float killOffTime = 0.0f;
    private float MessageOffTimeMax = 0.05f;

    private int messageCount = 0;

    private int messageFollDwonCount = 0;

    private int landingCount = 0;

    void Start()
    {
        bossCountOne = 1;
        nomalBoss.tag = "Boss";

        isCenterLine = false;
        isRightLine = false;
        isLeftLine = false;

        bossCount = GetComponent<BossCount>();

        IsFirstKill = false;
        IsKill = false;

        IsLanding = false;
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
                if (messageFollDwonCount == 0)
                {
                    if (messageCount < 1)
                    {
                        IsFirstKill = true;
                        messageCount++;
                        messageFollDwonCount++;
                    }
                }
                if (messageFollDwonCount >= 1)
                {
                    if (messageCount < 1)
                    {
                        IsKill = true;
                        messageCount++;
                        messageFollDwonCount++;
                    }
                }
                bossCount.SetBossKillCount();
                bossMoveList.RemoveAt(i);
                bossList.RemoveAt(i);
            }
        }

        for (int i = 0; i < bossMoveList.Count; i++)
        {
            if (bossMoveList[i].IsLanding)
            {
                if (landingCount < 1)
                {
                    IsLanding = true;
                    landingCount++;
                }
            }
            if (bossMoveList[i].isHazard)
            {
                IsDanger = true;
            }
            else
            {
                IsDanger = false;
            }
        }

        for(int i = 0; i<bossAttackList.Count; i++)
        {
            if (bossAttackList[i].IsCharge)
            {
                IsCharge = true;
            }
            else
            {
                IsCharge = false;
            }
        }

        MessageCancel();
    }

    private void MessageCancel()
    {
        if(IsLanding)
        {
            landingTime += Time.deltaTime;
            if (landingTime >= MessageOffTimeMax)
            {
                IsLanding = false;
                landingTime = 0.0f;
            }
        }

        if (IsFirstKill)
        {
            killOffTime += Time.deltaTime;
            if (killOffTime >= MessageOffTimeMax)
            {
                messageCount = 0;
                IsFirstKill = false;

                killOffTime = 0.0f;
            }
        }

        if (IsKill)
        {
            killOffTime += Time.deltaTime;
            if (killOffTime >= MessageOffTimeMax)
            {
                messageCount = 0;
                IsKill = false;

                killOffTime = 0.0f;
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
                bossType = 1;
                break;
            case "Mini":
                boss = Instantiate(miniBoss);
                bossType= 2;
                break;
            case "Big":
                boss = Instantiate(bigBoss);
                bossType = 3;
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
                    bossMoveList.Add(boss.GetComponent<BossMove>());
                    bossAttackList.Add(boss.GetComponent<BossAttack>());
                    bossCountOne++;
                    time = 0.0f;
                    bossType= 0;
                    isLeftLine = true;
                }
                break;
            case 2:
                if (!isCenterLine)
                {
                    BossType();
                    boss.transform.position = new Vector3(centerPosX, fallPosition, posZ);
                    bossList.Add(boss.GetComponent<BossDamage>());
                    bossMoveList.Add(boss.GetComponent<BossMove>());
                    bossAttackList.Add(boss.GetComponent<BossAttack>());
                    bossCountOne++;
                    time = 0.0f;
                    bossType = 0;
                    isCenterLine = true;
                }
                break;
            case 3:
                if (!isRightLine)
                {
                    BossType();
                    boss.transform.position = new Vector3(rightPosX, fallPosition, posZ);
                    bossList.Add(boss.GetComponent<BossDamage>());
                    bossMoveList.Add(boss.GetComponent<BossMove>());
                    bossAttackList.Add(boss.GetComponent<BossAttack>());
                    bossCountOne++;
                    time = 0.0f;
                    bossType = 0;
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

    public int BossTypeDate()
    {
        return bossType;
    }
}