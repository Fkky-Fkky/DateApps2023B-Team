// 担当者：吹上純平
using System.Collections.Generic;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// SEのマネージャークラス
    /// </summary>
    public class SEManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip playerPunchHit = null;

        [SerializeField]
        private AudioClip playerStan = null;

        [SerializeField]
        private AudioClip playerAttack = null;

        [SerializeField]
        private AudioClip bossBeamCharge = null;

        [SerializeField]
        private AudioClip bossBeam = null;

        [SerializeField]
        private AudioClip bossFinalAttack = null;

        [SerializeField]
        private AudioClip cannonConnect = null;

        [SerializeField]
        private AudioClip energyCharge = null;

        [SerializeField]
        private List<AudioClip> cannonBeams = new List<AudioClip>();

        /// <summary>
        /// プレイヤーのパンチヒットSE
        /// </summary>
        public AudioClip PlayerPunchHitSe { get { return playerPunchHit; } }

        /// <summary>
        /// プレイヤーの気絶SE
        /// </summary>
        public AudioClip PlayerStanSe { get { return playerStan; } }

        /// <summary>
        /// プレイヤーパンチSE
        /// </summary>
        public AudioClip PlayerAttackSe { get { return playerAttack; } }

        /// <summary>
        /// ボス攻撃チャージSE
        /// </summary>
        public AudioClip BossBeamCharge { get { return bossBeamCharge; } }

        /// <summary>
        /// ボス攻撃SE
        /// </summary>
        public AudioClip BossBeamSe { get { return bossBeam; } }

        /// <summary>
        /// ボスのとどめ時SE
        /// </summary>
        public AudioClip BossFinalAttack { get { return bossFinalAttack; } }

        /// <summary>
        /// 発射台設置SE
        /// </summary>
        public AudioClip CannonConnectSe { get { return cannonConnect; } }

        /// <summary>
        /// エネルギーチャージ時SE
        /// </summary>
        public AudioClip EnergyChargeSe { get { return energyCharge; } }

        /// <summary>
        /// 大砲のビームSEを返す
        /// </summary>
        /// <param name="beamType">ビームの種類</param>
        /// <returns>指定されたビームのAudioClip</returns>
        public AudioClip GetCannonBeamSe(int beamType)
        {
            return cannonBeams[beamType];
        }
    }
}