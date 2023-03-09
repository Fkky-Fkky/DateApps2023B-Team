// 担当者：吹上純平
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// 大砲がダメージを受けた時の処理クラス
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