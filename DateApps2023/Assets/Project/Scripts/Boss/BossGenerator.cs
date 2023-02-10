using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public BossManager bossManager;
    private BossCount bossCount;


    [SerializeField]
    GameObject bossModel;

    GameObject bossC;
    GameObject bossR;
    GameObject bossL;


    Vector3 bossPositionCenter = new Vector3(0.0f, -51.4f, 210.7f);
    Vector3 bossPositionLeft = new Vector3(-100.0f, -51.4f, 210.7f);
    Vector3 bossPositionRight = new Vector3(100.0f, -51.4f, 210.7f);

    int bossPattern;

    int patternNumber;
    int start = 1;
    int end = 4;

    private int bossCountOne = 1;

    [SerializeField]
    private bool isCenterLine = false;
    [SerializeField]
    private bool isRightLine = false;
    [SerializeField]
    private bool isLeftLine = false;

    private List<BossDamage> bossList = new List<BossDamage>();

    void Start()
    {
        bossCountOne = 1;
        bossModel.tag = "Boss";

        isCenterLine = false;
        isRightLine = false;
        isLeftLine = false;

        bossCount = GetComponent<BossCount>();
        BossRandomGeneration();
    }

    void Update()
    {
        if (bossManager.isGanerat)
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
        if (bossCountOne >= 2)
        {
            bossPattern = GetRandomValue(bossPattern);
        }

        switch (bossPattern)
        {
            case 1:
                if (!isCenterLine)
                {
                    bossC = Instantiate(bossModel);
                    bossC.transform.position = bossPositionCenter;
                    bossList.Add(bossC.GetComponent<BossDamage>());
                    bossCountOne++;
                    bossManager.isGanerat = false;
                    isCenterLine = true;
                }
                break;
            case 2:
                if (!isLeftLine)
                {
                    bossL = Instantiate(bossModel);
                    bossL.transform.position = bossPositionLeft;
                    bossList.Add(bossL.GetComponent<BossDamage>());
                    bossCountOne++;
                    bossManager.isGanerat = false;
                    isLeftLine = true;
                }
                break;
            case 3:
                if (!isRightLine)
                {
                    bossR = Instantiate(bossModel);
                    bossR.transform.position = bossPositionRight;
                    bossList.Add(bossR.GetComponent<BossDamage>());
                    bossCountOne++;
                    bossManager.isGanerat = false;
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

}