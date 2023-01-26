using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject bossModel;

    GameObject bossC;
    GameObject bossR;
    GameObject bossL;

    float intervalTime =  0.0f;
    [SerializeField]
    float bossInterval = 10.0f;


    Vector3 bossPositionCenter = new Vector3(    0.0f, -51.4f, 210.7f);
    Vector3 bossPositionLeft   = new Vector3(-100.0f, -51.4f, 210.7f);
    Vector3 bossPositionRight  = new Vector3( 100.0f, -51.4f, 210.7f);

    int bossPattern;

    int patternNumber;
    int start = 1;
    int end   = 4;

    int bossCount;

    void Start()
    {
        bossCount = 1;
        intervalTime = 9.0f;
        bossModel.tag = "Boss";
    }

    void Update()
    {
        intervalTime += Time.deltaTime;

        if (intervalTime >= bossInterval)
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


        switch (bossPattern) {
            case 1:
                bossC = Instantiate(bossModel);
                bossC.transform.position = bossPositionCenter;
                bossCount++;
                intervalTime = 0.0f;

                break;
            case 2:
                bossL = Instantiate(bossModel);
                bossL.transform.position = bossPositionLeft;
                bossCount++;
                intervalTime = 0.0f;
                break;
            case 3:
                bossR = Instantiate(bossModel);
                bossR.transform.position = bossPositionRight;
                bossCount++;
                intervalTime = 0.0f;
                break;
        }

    }

    public GameObject BossModle()
    {
        return bossModel;
    }

}
