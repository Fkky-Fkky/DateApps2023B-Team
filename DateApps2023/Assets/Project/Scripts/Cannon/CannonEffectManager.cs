// �S����:���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// ��C�̃G�t�F�N�g���Ǘ�����N���X
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
        /// ��C��_�����̉ΉԃG�t�F�N�g
        /// </summary>
        public ParticleSystem CannonDamageEffect { get { return cannonDamage; } }

        /// <summary>
        /// ��C��_�����̉��G�t�F�N�g
        /// </summary>
        public ParticleSystem CannonDamageSmokeEffect { get { return cannonDamageSmoke; } }

        /// <summary>
        /// ���ˑ�ݒu���̃G�t�F�N�g
        /// </summary>
        public ParticleSystem ConnectEffect { get { return connectEffect; } }

        /// <summary>
        /// �r�[�����ˌ�̃N�[���_�E���G�t�F�N�g
        /// </summary>
        public ParticleSystem CoolDownEffect { get { return coolDown; } }

        /// <summary>
        /// �G�l���M�[�`���[�W���̃G�t�F�N�g��Ԃ�
        /// </summary>
        /// <param name="energyType">�G�l���M�[�̎��</param>
        /// <returns>�G�l���M�[�`���[�W�G�t�F�N�g</returns>
        public ParticleSystem GetEnergyChargeEffect(int energyType)
        {
            return energyCharge[energyType];
        }

        /// <summary>
        /// �r�[�����ˑO�̗��߃G�t�F�N�g��Ԃ�
        /// </summary>
        /// <param name="energyType">�G�l���M�[�̎��</param>
        /// <returns>���߃G�t�F�N�g</returns>
        public ParticleSystem GetShotChargeEffect(int energyType)
        {
            return shotCharge[energyType];
        }

        /// <summary>
        /// �r�[�����ˎ��̉��G�t�F�N�g��Ԃ�
        /// </summary>
        /// <param name="energyType">�G�l���M�[�̎��</param>
        /// <returns>�r�[�����ˎ��̉��G�t�F�N�g</returns>
        public ParticleSystem GetShotSmokeEffect(int energyType)
        {
            return shotSmoke[energyType];
        }
    }
}