//担当者:武田碧
using UnityEngine;

/// <summary>
/// ボスのマネージャー
/// </summary>
public class BossManager : MonoBehaviour
{
    [SerializeField]
    private Resistance.CannonManager cannonManager       = null;
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

    const int CENTER_POS = 1;
    const int LEFT_POS   = 0;
    const int RIGHT_POS  = 2;

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
            if (cannonManager.DoConnectingPos[i] == CENTER_POS)
            {
                boss = GameObject.FindGameObjectWithTag("Center");
                if (boss == null)
                {
                    continue;
                }
            }

            if (cannonManager.DoConnectingPos[i] == LEFT_POS)
            {
                boss = GameObject.FindGameObjectWithTag("Left");
                if (boss == null)
                {
                    continue;
                }
            }

            if (cannonManager.DoConnectingPos[i] == RIGHT_POS)
            {
                boss = GameObject.FindGameObjectWithTag("Right");
                if (boss == null)
                {
                    continue;
                }
            }

            switch (cannonManager.IsShotEnergyType[i])
            {
                case (int)Resistance.EnergyCharge.ENERGY_TYPE.SMALL:
                    boss.GetComponent<BossDamage>().KnockbackTrueSmall();
                    break;
                case (int)Resistance.EnergyCharge.ENERGY_TYPE.MEDIUM:
                    boss.GetComponent<BossDamage>().KnockbackTrueMedium();
                    break;
                case (int)Resistance.EnergyCharge.ENERGY_TYPE.LARGE:
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
    /// <summary>
    /// 怪獣撃破時のフラグを返す
    /// </summary>
    /// <returns>怪獣を撃破したフラグ</returns>
    public bool IsBossKill()
    {
        return bossCSVGenerator.IsKill;
    }
    /// <summary>
    /// ボスの種類の値を返す
    /// </summary>
    /// <returns>ボスの種類の値</returns>
    public int BossType()
    {
        return bossCSVGenerator.BossTypeDate();//中 1, ミニ 2, Big 3
    }
    /// <summary>
    /// 怪獣がチャージするフラグを返す
    /// </summary>
    /// <returns>チャージ開始</returns>
    public bool Charge()
    {  
        return bossCSVGenerator.IsCharge;//破壊光線チャージ時
    }
    /// <summary>
    /// 怪獣が接近しているフラグを返す
    /// </summary>
    /// <returns>接近</returns>
    public bool Danger()
    {
        return bossCSVGenerator.IsDanger;//接近時
    }
    /// <summary>
    /// ゲームオーバーのフラグを返す
    /// </summary>
    /// <returns>ゲームオーバーのフラグ</returns>
    public bool IsGameOver()
    {
        return bossCSVGenerator.IsGameOver;//ゲームオーバーフラグ
    }
}