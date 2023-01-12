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
    float bossInterval = 2.0f;


    Vector3 bossPositionCenter = new Vector3(   0.0f, -220.0f, 745.0f);
    Vector3 bossPositionLeft   = new Vector3(-736.0f, -251.0f, 920.0f);
    Vector3 bossPositionRight  = new Vector3(1065.0f, -251.0f, 889.0f);

    int bossPattern;

    int start = 1;
    int end   = 3;
    List<int> pattern = new List<int>();

    void Start()
    {
    }

    void Update()
    {
        intervalTime += Time.deltaTime;

        if (intervalTime >= bossInterval)
        {
            BossRandomGeneration();
        }
    }

    void BossRandomGeneration()
    {
            bossPattern = Random.Range(1,3);

       

        switch (bossPattern) {
            case 1:
                bossC = Instantiate(bossModel);
                bossC.transform.position = bossPositionCenter;
                intervalTime = 0.0f;
                break;
            case 2:
                bossL = Instantiate(bossModel);
                bossL.transform.position = bossPositionLeft;
                
                intervalTime = 0.0f;
                break;
            case 3:
                bossR = Instantiate(bossModel);
                bossR.transform.position = bossPositionRight;
                intervalTime = 0.0f;
                break;

        }

    }
}
