using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private CannonManager cannonManager = null;

    [SerializeField]
    private BossCSVGenerator bossCSVGenerator = null;

    public BossDamage bossDamage;

    private GameObject centerBoss;
    private GameObject leftBoss;
    private GameObject rightBoss;
    private void Start()
    {

    }

    void Update()
    {
        BossDamage();
        BossFellDown();
        
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
            bossCSVGenerator.IsCenterLineFalse();
        }
        else
        {
            bossCSVGenerator.IsCenterLineTrue();
        }

        leftBoss = GameObject.FindGameObjectWithTag("Left");
        if (leftBoss == null)
        {
            bossCSVGenerator.IsLeftLineFalse();
        }
        else
        {
            bossCSVGenerator.IsLeftLineTrue();
        }

        rightBoss = GameObject.FindGameObjectWithTag("Right");
        if (rightBoss == null)
        {
            bossCSVGenerator.IsRightLineFalse();
        }
        else
        {
            bossCSVGenerator.IsRightLineTrue();
        }

    }
}