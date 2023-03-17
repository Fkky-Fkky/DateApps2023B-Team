// �S���ҁF���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// ��C���_���[�W���󂯂����̏����N���X
    /// </summary>
    public class CannonDamage : MonoBehaviour
    {
        [SerializeField]
        private EnergyCharge energyCharge = null;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("BossAttack"))
            {
                return;
            }
            energyCharge.DisChargeEnergy();
        }
    }
}