using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private CannonManager cannonManager = null;

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

        BossDamage();
    }

    void BossDamage()
    {
        if (cannonManager.IsShooting && cannonManager.DoConnectingPos == 1)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Center");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

        if (cannonManager.IsShooting && cannonManager.DoConnectingPos == 0)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Left");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

        if (cannonManager.IsShooting && cannonManager.DoConnectingPos == 2)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Right");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

    }
}