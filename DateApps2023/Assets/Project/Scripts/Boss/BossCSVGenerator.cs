using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class BossCSVGenerator : MonoBehaviour
{
    [SerializeField]
    private BossManager bossManager = null;
    [SerializeField]
    private opretar opretar         = null;
    [SerializeField]
    private BossCSV bossCSV         = null;

    [SerializeField]
    private GameObject nomalBoss = null;
    [SerializeField]
    private GameObject miniBoss  = null;
    [SerializeField]
    private GameObject bigBoss   = null;

    [SerializeField]
    private int bossCountMax = 10;

    [SerializeField]
    private float sidePos    = 100.0f;

    private int bossCountOne = 1;

    private const float centerPosX = 0.0f;
    private float leftPosX         = 0.0f;
    private float rightPosX        = 0.0f;

    private GameObject boss = null;
    private int bossType    = 0;

    private string bossTypeDate      = null;
    private int bossLane             = 0;
    private float attackIntervalDate = 0.0f;
    private float posZ               = 0.0f;
    private int bossHpDate           = 0;
    private float moveSpeedDate      = 0.0f;


    private const float fallPosition = 500.0f;

    private float time = 0.0f;

    private bool isCenterLine = false;
    private bool isRightLine  = false;
    private bool isLeftLine   = false;

    private List<BossDamage> bossList       = new List<BossDamage>();
    private List<BossMove>   bossMoveList   = new List<BossMove>();
    private List<BossAttack> bossAttackList = new List<BossAttack>();

    public bool IsFirstKill { get; private set; }
    public bool IsKill { get; private set; }

    public bool IsLanding { get; private set; }
    public bool IsDanger { get; private set; }

    public bool IsCharge { get; private set; }

    public bool IsGameOver { get; private set; }

    private float landingTime = 0.0f;

    private float dangerOffTime           = 0.0f;
    private float killOffTime             = 0.0f;
    private const float messageOffTimeMax = 0.05f;

    private int messageCount = 0;
    private int landingCount = 0;

    private float bossTypeOffTime          = 0.0f;
    private const float bossTypeOffTimeMax = 0.03f;

    private BossCount bossCount = null;

    void Start()
    {
        bossCount = GetComponent<BossCount>();

        IsFirstKill = false;
        IsKill = false;

        IsLanding = false;

        leftPosX = -sidePos;
        rightPosX = sidePos;

    }

    void Update()
    {

        for (int i = 0; i < bossList.Count; i++)
        {
            if (bossList[i].IsFellDown())
            {
                if (messageCount < 1)
                {
                    IsKill = true;
                    messageCount++;
                }
                bossCount.SetBossKillCount();
                bossMoveList.RemoveAt(i);
                bossList.RemoveAt(i);
            }
        }

        //スタートフラグ
        if (!opretar.Getstartflag())
        {
            return;
        }

        if (bossCountOne <= bossCountMax)
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

                BossGanarater();
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
            if (bossMoveList[i].IsHazard)
            {
                IsDanger = true;
            }
        }

        for (int i = 0; i < bossAttackList.Count; i++)
        {
            if (bossAttackList[i].IsCharge)
            {
                IsCharge = true;
                break;
            }
            IsCharge = false;
        }

        for(int i = 0; i < bossMoveList.Count; i++)
        {
            if (bossMoveList[i].IsGameOver)
            {
                IsGameOver = true;
                break;
            }
            IsGameOver = false;
        }

        MessageCancel();
    }

    private void MessageCancel()
    {
        if (IsLanding)
        {
            landingTime += Time.deltaTime;
            if (landingTime >= messageOffTimeMax)
            {
                IsLanding = false;
                landingTime = 0.0f;
            }
        }

        if (IsFirstKill)
        {
            killOffTime += Time.deltaTime;
            if (killOffTime >= messageOffTimeMax)
            {
                messageCount = 0;
                IsFirstKill = false;

                killOffTime = 0.0f;
            }
        }

        if (IsKill)
        {
            killOffTime += Time.deltaTime;
            if (killOffTime >= messageOffTimeMax)
            {
                messageCount = 0;
                IsKill = false;

                killOffTime = 0.0f;
            }
        }

        if (IsDanger)
        {
            dangerOffTime += Time.deltaTime;
            if (dangerOffTime >= messageOffTimeMax)
            {
                IsDanger = false;
                dangerOffTime = 0.0f;
            }
        }

        if (bossType != 0)
        {
            bossTypeOffTime += Time.deltaTime;
            if (bossTypeOffTime >= bossTypeOffTimeMax)
            {
                bossType = 0;
                bossTypeOffTime = 0.0f;
            }
        }

    }

    private void BossGanarater()
    {
        switch (bossLane)
        {
            case 1:
                if (!isLeftLine)
                {
                    BossAppearanceDate(leftPosX);
                    isLeftLine = true;
                }
                break;
            case 2:
                if (!isCenterLine)
                {
                    BossAppearanceDate(centerPosX);
                    isCenterLine = true;
                }
                break;
            case 3:
                if (!isRightLine)
                {
                    BossAppearanceDate(rightPosX);
                    isRightLine = true;
                }
                break;
        }
    }

    private void BossAppearanceDate(float bossPositionX)
    {
        BossType();
        boss.transform.position = new Vector3(bossPositionX, fallPosition, posZ);
        bossList.Add(boss.GetComponent<BossDamage>());
        bossMoveList.Add(boss.GetComponent<BossMove>());
        bossAttackList.Add(boss.GetComponent<BossAttack>());
        if (opretar.Getstartflag())
        {
            bossCountOne++;
        }

        time = 0.0f;
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
                bossType = 2;
                break;
            case "Big":
                boss = Instantiate(bigBoss);
                bossType = 3;
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

    public void FirstBossGanaretar()
    {
        bossTypeDate = "Nomal";
        bossLane = 2;
        attackIntervalDate = 10000.0f;
        posZ = 345.0f;
        bossHpDate = 3;
        moveSpeedDate = 3;

        BossGanarater();
    }

    public void SecondBossGanaretar()
    {
        bossTypeDate = "Nomal";
        bossLane = 3;
        attackIntervalDate = 10000.0f;
        posZ = 345.0f;
        bossHpDate = 3;
        moveSpeedDate = 3;

        BossGanarater();
    }

}