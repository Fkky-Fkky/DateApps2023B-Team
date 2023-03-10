using UnityEngine;

/// <summary>
/// �{�X�̃}�l�[�W���[
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
    /// �����̃��[���Ƀ{�X�����邩�̃t���O
    /// </summary>
    public bool IsCenterLine { get; private set; }
    /// <summary>
    /// �E���̃��[���Ƀ{�X�����邩�̃t���O
    /// </summary>
    public bool IsRightLine { get; private set; }
    /// <summary>
    /// �����̃��[���Ƀ{�X�����邩�̃t���O
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
    /// �ǂ̃{�X���U�����󂯂邩
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
    /// �{�X�����Ȃ����[����T��
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
        return bossCSVGenerator.IsKill;//���b���j��
    }

    public int BossType()
    {
        return bossCSVGenerator.BossTypeDate();//�� 1, �~�j 2, Big 3
    }

    public bool Charge()
    {  
        return bossCSVGenerator.IsCharge;//�j������`���[�W��
    }
    public bool Danger()
    {
        return bossCSVGenerator.IsDanger;//�ڋߎ�
    }
    public bool IsGameOver()
    {
        return bossCSVGenerator.IsGameOver;//�Q�[���I�[�o�[�t���O
    }
}