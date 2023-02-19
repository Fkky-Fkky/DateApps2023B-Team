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
    private int bullet = 1;

    private GameObject centerBoss;
    private GameObject leftBoss;
    private GameObject rightBoss;
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
        DebugDamage();
        BossFellDown();
        
    }

    private void DebugDamage()
    {


        GameObject boss = null;

        if (Input.GetKeyDown(KeyCode.C))
        {
            boss = GameObject.FindGameObjectWithTag("Center");
            if (boss == null)
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            boss = GameObject.FindGameObjectWithTag("Left");
            if (boss == null)
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            boss = GameObject.FindGameObjectWithTag("Right");
            if (boss == null)
            {
                return;
            }
        }


        switch (bullet)
        {
            case 0:
                boss.GetComponent<BossDamage>().KnockbackTrueSmall();
                break;
            case 1:
                boss.GetComponent<BossDamage>().KnockbackTrueMedium();
                break;
            case 2:
                boss.GetComponent<BossDamage>().KnockbackTrueLarge();
                break;
        }

    }

    private void BossDamage()
    {
        if (!cannonManager.IsShooting)
        {
            return;
        }

        GameObject boss = null;
        if (cannonManager.DoConnectingPos == 1)
        {
            boss = GameObject.FindGameObjectWithTag("Center");
            if (boss == null)
            {
                return;
            }
        }

        if (cannonManager.DoConnectingPos == 0)
        {
            boss = GameObject.FindGameObjectWithTag("Left");
            if (boss == null)
            {
                return;
            }
        }

        if (cannonManager.DoConnectingPos == 2)
        {
            boss = GameObject.FindGameObjectWithTag("Right");
            if (boss == null)
            {
                return;
            }
        }

        switch (cannonManager.IsShotEnergyType)
        {
            case (int)EnergyCharge.EnergyType.SMALL:
                boss.GetComponent<BossDamage>().KnockbackTrueSmall();
                break;
            case (int)EnergyCharge.EnergyType.MEDIUM:
                boss.GetComponent<BossDamage>().KnockbackTrueMedium();
                break;
            case (int)EnergyCharge.EnergyType.LARGE:
                boss.GetComponent<BossDamage>().KnockbackTrueLarge();
                break;
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