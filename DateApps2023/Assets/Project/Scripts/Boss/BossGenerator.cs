using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public BossManager bossManager;
    private BossCount bossCount;



    [SerializeField]
    private GameObject nomalBoss;

    [SerializeField]
    private GameObject miniBoss;

    [SerializeField]
    private GameObject bigBoss;

    GameObject bossC;
    GameObject bossR;
    GameObject bossL;

    private int bossRandom = 0;



    private Vector3 bossPositionCenter;
    private Vector3 bossPositionLeft;
    private Vector3 bossPositionRight;

    private float fallPosition = 500.0f;

    int bossPattern;

    int patternNumber;
    int start = 1;
    int end = 4;

    private int bossCountOne = 1;

    private bool isCenterLine = false;
    private bool isRightLine = false;
    private bool isLeftLine = false;

    private List<BossDamage> bossList = new List<BossDamage>();

    void Start()
    {
        bossCountOne  = 1;
        nomalBoss.tag = "Boss";

        isCenterLine = false;
        isRightLine  = false;
        isLeftLine   = false;

        bossPositionCenter = new Vector3(   0.0f, fallPosition, 300.0f);
        bossPositionLeft   = new Vector3(-100.0f, fallPosition, 300.0f);
        bossPositionRight  = new Vector3( 100.0f, fallPosition, 300.0f);

        bossCount = GetComponent<BossCount>();
        BossRandomGeneration();
        BossRandom();
    }

    void Update()
    {
        if (bossCountOne <= 10)
        {
            BossRandomGeneration();
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

    int GetRandomValue(int oldnum)
    {
        patternNumber = Random.Range(start, end);

        if (patternNumber == oldnum)
        {
            int n = patternNumber + Random.Range(1, end);

            return n < end ? n : n - end;
        }
        else
        {
            return patternNumber;
        }
    }

    void BossRandomGeneration()
    {
        if (bossCountOne == 1)
        {
            bossPattern = 1;
        }
        if (bossCountOne > 1)
        {
            bossPattern = GetRandomValue(bossPattern);
        }

        switch (bossPattern)
        {
            case 1:
                if (!isCenterLine)
                {
                    bossC = Instantiate(BossRandom());
                    bossC.transform.position = bossPositionCenter;
                    bossList.Add(bossC.GetComponent<BossDamage>());
                    bossCountOne++;
                    isCenterLine = true;
                }
                break;
            case 2:
                if (!isLeftLine)
                {
                    bossL = Instantiate(BossRandom());
                    bossL.transform.position = bossPositionLeft;
                    bossList.Add(bossL.GetComponent<BossDamage>());
                    bossCountOne++;
                    isLeftLine = true;
                }
                break;
            case 3:
                if (!isRightLine)
                {
                    bossR = Instantiate(BossRandom());
                    bossR.transform.position = bossPositionRight;
                    bossList.Add(bossR.GetComponent<BossDamage>());
                    bossCountOne++;
                    isRightLine = true;
                }
                break;
        }

    }

    private GameObject BossRandom()
    {
        if (bossCountOne == 1)
        {
            bossRandom = 1;

        }
        if (bossCountOne > 1)
        {
            bossRandom = GetRandomValue(bossRandom);
        }

        if (bossRandom == 1)
        {
            return nomalBoss;
        }

        if (bossRandom == 2)
        {
            return miniBoss;
        }
        
        if(bossRandom== 3)
        {
            return bigBoss;
        }
     

        return BossRandom();
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

}