using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public BossDamage bossDamage;
    
    float bossGenerationTime = 0.0f;
    [SerializeField]
    float bossIntervalTime = 10.0f;

    public bool isGanerat;
    private void Start()
    {
        isGanerat = false;
    }

    void Update()
    {
        bossGenerationTime += Time.deltaTime;
        if (bossGenerationTime >= bossIntervalTime)
        {
            isGanerat = true;
            bossGenerationTime = 0.0f;
        }
        Debugging();
    }

    void Debugging()
    {
        //デバック用
        if (Input.GetKeyDown(KeyCode.K))//<-ifの中に大砲を発射したフラグと大砲がいるレーンの情報
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Center");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Left");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Right");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

    }

}
