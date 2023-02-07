using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private CannonManager cannonManager = null;
    [SerializeField]
    private BossGenerator bossGenerator = null;

    public BossDamage bossDamage;

    float bossGenerationTime = 0.0f;
    [SerializeField]
    float bossIntervalTime = 10.0f;

    public bool isGanerat;

    [SerializeField]
    private int bullet;

    private GameObject centerBoss;
    private GameObject leftBoss;
    private GameObject rightBoss;
    private void Start()
    {
        isGanerat = false;
        bullet = 1;


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

        BossFellDown();
    }

    private void BossDevack()
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

    private void BossDamage()
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

    private void BossFellDown()
    {
        centerBoss = GameObject.FindGameObjectWithTag("Center");
        if (centerBoss == null)
        {
            bossGenerator.IsCenterLineFalse();
        }
        else
        {
            bossGenerator.IsCenterLineTrue();
        }

        leftBoss = GameObject.FindGameObjectWithTag("Left");
        if (leftBoss == null)
        {
            bossGenerator.IsLeftLineFalse();
        }
        else
        {
            bossGenerator.IsLeftLineTrue();
        }

        rightBoss = GameObject.FindGameObjectWithTag("Right");
        if (rightBoss == null)
        {
            bossGenerator.IsRightLineFalse();
        }
        else
        {
            bossGenerator.IsRightLineTrue();
        }

    }
}