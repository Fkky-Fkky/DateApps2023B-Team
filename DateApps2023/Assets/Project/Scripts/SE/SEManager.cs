// �S���ҁF���㏃��
using System.Collections.Generic;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// SE�̃}�l�[�W���[�N���X
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
        /// �v���C���[�̃p���`�q�b�gSE
        /// </summary>
        public AudioClip PlayerPunchHitSe { get { return playerPunchHit; } }

        /// <summary>
        /// �v���C���[�̋C��SE
        /// </summary>
        public AudioClip PlayerStanSe { get { return playerStan; } }

        /// <summary>
        /// �v���C���[�p���`SE
        /// </summary>
        public AudioClip PlayerAttackSe { get { return playerAttack; } }

        /// <summary>
        /// �{�X�U���`���[�WSE
        /// </summary>
        public AudioClip BossBeamCharge { get { return bossBeamCharge; } }

        /// <summary>
        /// �{�X�U��SE
        /// </summary>
        public AudioClip BossBeamSe { get { return bossBeam; } }

        /// <summary>
        /// �{�X�̂Ƃǂߎ�SE
        /// </summary>
        public AudioClip BossFinalAttack { get { return bossFinalAttack; } }

        /// <summary>
        /// ���ˑ�ݒuSE
        /// </summary>
        public AudioClip CannonConnectSe { get { return cannonConnect; } }

        /// <summary>
        /// �G�l���M�[�`���[�W��SE
        /// </summary>
        public AudioClip EnergyChargeSe { get { return energyCharge; } }

        /// <summary>
        /// ��C�̃r�[��SE��Ԃ�
        /// </summary>
        /// <param name="beamType">�r�[���̎��</param>
        /// <returns>�w�肳�ꂽ�r�[����AudioClip</returns>
        public AudioClip GetCannonBeamSe(int beamType)
        {
            return cannonBeams[beamType];
        }
    }
}