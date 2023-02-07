using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public BossManager bossManager;
    BossMove bossMove;

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

    private int bossCount = 1;

    [SerializeField]
    private bool isCenterLine = false;
    [SerializeField]
    private bool isRightLine = false;
    [SerializeField]
    private bool isLeftLine = false;

    void Start()
    {
        bossCount = 1;
        bossModel.tag = "Boss";

        isCenterLine = false;
        isRightLine = false;
        isLeftLine = false;

        bossMove = GetComponent<BossMove>();
    }

    void Update()
    {
        if (bossManager.isGanerat)
        {
            BossRandomGeneration();
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
        if (bossCount == 1)
        {
            bossPattern = 1;
        }
        if (bossCount >= 2)
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
                    bossCount++;
                    bossManager.isGanerat = false;
                    isCenterLine = true;
                }
                break;
            case 2:
                if (!isLeftLine)
                {
                    bossL = Instantiate(bossModel);
                    bossL.transform.position = bossPositionLeft;
                    bossCount++;
                    bossManager.isGanerat = false;
                    isLeftLine = true;
                }
                break;
            case 3:
                if (!isRightLine)
                {
                    bossR = Instantiate(bossModel);
                    bossR.transform.position = bossPositionRight;
                    bossCount++;
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