using UnityEngine;

/// <summary>
/// ボスのマネージャー
/// </summary>
public class BossManager : MonoBehaviour
{
    [SerializeField]
    private CannonManager cannonManager       = null;
    [SerializeField]
    private BossCSVGenerator bossCSVGenerator = null;

    private GameObject centerBoss = null;
    private GameObject leftBoss   = null;
    private GameObject rightBoss  = null;

    void Update()
    {
        BossDamage();
        BossFellDown();
    }

    /// <summary>
    /// どのボスが攻撃を受けるか
    /// </summary>
    private void BossDamage()
    {
        for (int i = 0; i < cannonManager.CanonMax; i++)
        {
            if (!cannonManager.IsShooting[i])
            {
                continue;
            }

            GameObject boss = null;
            if (cannonManager.DoConnectingPos[i] == 1)
            {
                boss = GameObject.FindGameObjectWithTag("Center");
                if (boss == null)
                {
                    continue;
                }
            }

            if (cannonManager.DoConnectingPos[i] == 0)
            {
                boss = GameObject.FindGameObjectWithTag("Left");
                if (boss == null)
                {
                    continue;
                }
            }

            if (cannonManager.DoConnectingPos[i] == 2)
            {
                boss = GameObject.FindGameObjectWithTag("Right");
                if (boss == null)
                {
                    continue;
                }
            }

            switch (cannonManager.IsShotEnergyType[i])
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
    }

    /// <summary>
    /// ボスがいないレーンを探す
    /// </summary>
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
    public bool IsBossKill()
    {
        return bossCSVGenerator.IsKill;//怪獣撃破時
    }

    public int BossType()
    {
        return bossCSVGenerator.BossTypeDate();//中 1, ミニ 2, Big 3
    }

    public bool Charge()
    {  
        return bossCSVGenerator.IsCharge;//破壊光線チャージ時
    }
    public bool Danger()
    {
        return bossCSVGenerator.IsDanger;//接近時
    }
    public bool IsGameOver()
    {
        return bossCSVGenerator.IsGameOver;//ゲームオーバーフラグ
    }
}