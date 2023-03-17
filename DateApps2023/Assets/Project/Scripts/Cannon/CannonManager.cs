// �S���ҁF���㏃��
using System.Collections.Generic;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// ��C�̃}�l�[�W���[�N���X
    /// </summary>
    public class CannonManager : MonoBehaviour
    {
        [SerializeField]
        private List<CannonConnect> cannonConnect = new List<CannonConnect>();

        [SerializeField]
        private List<CannonShot> cannonShot = new List<CannonShot>();

        [SerializeField]
        private List<EnergyCharge> energyCharge = new List<EnergyCharge>();

        private List<int> isShotEnergyTypeList = new List<int>();
        private List<int> connectingPosList = new List<int>();
        private List<bool> isShootingList = new List<bool>();

        const int CANNON_MAX = 2;

        /// <summary>
        /// ��C�̐���Ԃ�
        /// </summary>
        public int CanonMax { get { return CANNON_MAX; } }

        private void Start()
        {
            for (int i = 0; i < CANNON_MAX; i++)
            {
                isShootingList.Add(cannonShot[i].IsNowShot);
                isShotEnergyTypeList.Add(energyCharge[i].ChargeEnergyType);
                connectingPosList.Add(cannonConnect[i].ConnectingPos);
            }
        }

        /// <summary>
        /// ��C�����˂��Ă��邩��Ԃ�
        /// </summary>
        /// <returns>��C�����˂��Ă��邩�����X�g�ŕԂ�</returns>
        public List<bool> IsShooting
        {
            get
            {
                for (int i = 0; i < CANNON_MAX; i++)
                {
                    isShootingList[i] = cannonShot[i].IsNowShot;
                }
                return isShootingList;
            }
        }

        /// <summary>
        /// ���˂���G�l���M�[�̎�ނ�Ԃ�
        /// </summary>
        /// <returns>���˂��Ă���G�l���M�[�����X�g�ŕԂ�</returns>
        public List<int> IsShotEnergyType
        {
            get
            {
                for (int i = 0; i < CANNON_MAX; i++)
                {
                    isShotEnergyTypeList[i] = energyCharge[i].ChargeEnergyType;
                }
                return isShotEnergyTypeList;
            }
        }

        /// <summary>
        /// �ݒu����Ă����C�̏ꏊ��Ԃ�
        /// </summary>
        /// <returns>�ݒu����Ă����C�̏ꏊ�����X�g�ŕԂ�</returns>
        public List<int> DoConnectingPos
        {
            get
            {
                for (int i = 0; i < CANNON_MAX; i++)
                {
                    connectingPosList[i] = cannonConnect[i].ConnectingPos;
                }
                return connectingPosList;
            }
        }

        /// <summary>
        /// �G�l���M�[����C�Ƀ`���[�W���ꂽ����Ԃ�
        /// </summary>
        /// <returns>��ڂ̑�C�ɃG�l���M�[���`���[�W����Ă��邩��Ԃ�</returns>
        public bool IsFirstCharge()
        {
            return energyCharge[0].IsEnergyCharge;
        }
    }
}