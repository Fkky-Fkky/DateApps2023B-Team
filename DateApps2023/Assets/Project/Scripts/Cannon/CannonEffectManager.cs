// 担当者:吹上純平
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// 大砲のエフェクトを管理するクラス
    /// </summary>
    public class CannonEffectManager : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem cannonDamage = null;

        [SerializeField]
        private ParticleSystem cannonDamageSmoke = null;

        [SerializeField]
        private ParticleSystem connectEffect = null;

        [SerializeField]
        private ParticleSystem coolDown = null;

        [SerializeField]
        private ParticleSystem[] energyCharge = new ParticleSystem[3];

        [SerializeField]
        private ParticleSystem[] shotCharge = new ParticleSystem[3];

        [SerializeField]
        private ParticleSystem[] shotSmoke = new ParticleSystem[3];

        /// <summary>
        /// 大砲被ダメ時の火花エフェクト
        /// </summary>
        public ParticleSystem CannonDamageEffect { get { return cannonDamage; } }

        /// <summary>
        /// 大砲被ダメ時の煙エフェクト
        /// </summary>
        public ParticleSystem CannonDamageSmokeEffect { get { return cannonDamageSmoke; } }

        /// <summary>
        /// 発射台設置時のエフェクト
        /// </summary>
        public ParticleSystem ConnectEffect { get { return connectEffect; } }

        /// <summary>
        /// ビーム発射後のクールダウンエフェクト
        /// </summary>
        public ParticleSystem CoolDownEffect { get { return coolDown; } }

        /// <summary>
        /// エネルギーチャージ時のエフェクトを返す
        /// </summary>
        /// <param name="energyType">エネルギーの種類</param>
        /// <returns>エネルギーチャージエフェクト</returns>
        public ParticleSystem GetEnergyChargeEffect(int energyType)
        {
            return energyCharge[energyType];
        }

        /// <summary>
        /// ビーム発射前の溜めエフェクトを返す
        /// </summary>
        /// <param name="energyType">エネルギーの種類</param>
        /// <returns>溜めエフェクト</returns>
        public ParticleSystem GetShotChargeEffect(int energyType)
        {
            return shotCharge[energyType];
        }

        /// <summary>
        /// ビーム発射時の煙エフェクトを返す
        /// </summary>
        /// <param name="energyType">エネルギーの種類</param>
        /// <returns>ビーム発射時の煙エフェクト</returns>
        public ParticleSystem GetShotSmokeEffect(int energyType)
        {
            return shotSmoke[energyType];
        }
    }
}