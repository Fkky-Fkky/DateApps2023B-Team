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

    [SerializeField]
    private int bullet; 
    private void Start()
    {
        isGanerat = false;
        bullet = -1;
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
        BossDevack();

    }

    void BossDevack()
    {
        if (bullet == 0)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Center");
                foreach (GameObject boss in objects)
                {
                    boss.GetComponent<BossDamage>().KnockbackTrue();
                }
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Left");
                foreach (GameObject boss in objects)
                {
                    boss.GetComponent<BossDamage>().KnockbackTrue();
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Right");
                foreach (GameObject boss in objects)
                {
                    boss.GetComponent<BossDamage>().KnockbackTrue();
                }
            }
        }

        if (bullet == 1)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Center");
                foreach (GameObject boss in objects)
                {
                    boss.GetComponent<BossDamage>().KnockbackTrueTwo();
                }
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Left");
                foreach (GameObject boss in objects)
                {
                    boss.GetComponent<BossDamage>().KnockbackTrueTwo();
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Right");
                foreach (GameObject boss in objects)
                {
                    boss.GetComponent<BossDamage>().KnockbackTrueTwo();
                }
            }

        }

    }

    void BossDamage()
    {
        if (cannonManager.IsShooting && cannonManager.DoConnectingPos == 1)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Center");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrue();
            }
        }

        if (cannonManager.IsShooting && cannonManager.DoConnectingPos == 0)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Left");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrue();
            }
        }

        if (cannonManager.IsShooting && cannonManager.DoConnectingPos == 2)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Right");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrue();
            }
        }

    }


}