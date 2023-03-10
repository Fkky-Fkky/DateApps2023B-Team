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

    /// <summary>
    /// 中央のレーンにボスがいるかのフラグ
    /// </summary>
    public bool IsCenterLine { get; private set; }
    /// <summary>
    /// 右側のレーンにボスがいるかのフラグ
    /// </summary>
    public bool IsRightLine { get; private set; }
    /// <summary>
    /// 左側のレーンにボスがいるかのフラグ
    /// </summary>
    public bool IsLeftLine { get; private set; }


    private void Start()
    {
        IsCenterLine = false;
        IsRightLine  = false;
        IsLeftLine   = false;
    }

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
                case (int)EnergyCharge.ENERGY_TYPE.SMALL:
                    boss.GetComponent<BossDamage>().KnockbackTrueSmall();
                    break;
                case (int)EnergyCharge.ENERGY_TYPE.MEDIUM:
                    boss.GetComponent<BossDamage>().KnockbackTrueMedium();
                    break;
                case (int)EnergyCharge.ENERGY_TYPE.LARGE:
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
            IsCenterLine = false;
        }
        else
        {
            IsCenterLine= true;
        }

        leftBoss = GameObject.FindGameObjectWithTag("Left");
        if (leftBoss == null)
        {
            IsLeftLine = false;
        }
        else
        {
            IsLeftLine= true;
        }

        rightBoss = GameObject.FindGameObjectWithTag("Right");
        if (rightBoss == null)
        {
            IsRightLine= false;
        }
        else
        {
            IsRightLine= true;
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