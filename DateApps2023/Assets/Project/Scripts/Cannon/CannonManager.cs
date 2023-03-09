// 担当者：吹上純平
using System.Collections.Generic;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// 大砲のマネージャークラス
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
        /// 大砲の数を返す
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
        /// 大砲が発射しているかを返す
        /// </summary>
        /// <returns>大砲が発射しているかをリストで返す</returns>
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
        /// 発射するエネルギーの種類を返す
        /// </summary>
        /// <returns>発射しているエネルギーをリストで返す</returns>
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
        /// 設置されている大砲の場所を返す
        /// </summary>
        /// <returns>設置されている大砲の場所をリストで返す</returns>
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
        /// エネルギーが大砲にチャージされたかを返す
        /// </summary>
        /// <returns>一つ目の大砲にエネルギーがチャージされているかを返す</returns>
        public bool IsFirstCharge()
        {
            return energyCharge[0].IsEnergyCharge;
        }
    }
}