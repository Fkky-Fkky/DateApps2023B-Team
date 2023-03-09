using UnityEngine;

/// <summary>
/// �G�l���M�[�^���N�̃}�l�[�W���[�N���X
/// </summary>
public class TankManager : MonoBehaviour
{
    [SerializeField]
    private EnergyCharge energyCharge = null;

    private bool oldEnergy = false;
    private TankCharge energyTank = null;

    private void Start()
    {
        energyTank = GetComponentInChildren<TankCharge>();
    }

    // Update is called once per frame
    void Update()
    {
        bool doEnergyCharge = energyCharge.IsEnergyCharge != oldEnergy;

        if (!doEnergyCharge)
        {
            return;
        }
        EnergyCharge();
    }

    /// <summary>
    /// �G�l���M�[�^���N�̌����ڂ�ύX����
    /// </summary>
    private void EnergyCharge()
    {
        if (energyCharge.IsEnergyCharge)
        {
            energyTank.Charge(energyCharge.ChargeEnergyType);
        }
        else
        {
            energyTank.DisCharge();
        }
        oldEnergy = energyCharge.IsEnergyCharge;
    }
}
