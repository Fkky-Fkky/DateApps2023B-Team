// �S���ҁF���㏃��
using System.Collections.Generic;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �Q�[�����Ɏg�p����A�G�l���M�[�����̐����������s���N���X
    /// </summary>
    public class NormalEnergyGenerator : EnergyGeneratorBase
    {
        [SerializeField]
        private float energyGenerateInterval = 10.0f;

        [SerializeField]
        private GameManager gameManager = null;

        private int generateNum = 0;
        private bool isGenerateEnergy = false;
        private List<float> generateTimeList = new List<float>();

        private void Update()
        {
            if (gameManager.IsGameOver)
            {
                return;
            }

            CalculateEnergyGenerateTime();
            if (isGenerateEnergy)
            {
                GenerateEnergy();
            }
        }

        /// <summary>
        /// �G�l���M�[�𐶐����鎞�Ԃ��v��
        /// </summary>
        private void CalculateEnergyGenerateTime()
        {
            for (int i = 0; i < generateTimeList.Count; i++)
            {
                generateTimeList[i] -= Time.deltaTime;
                if (generateTimeList[i] <= 0.0f)
                {
                    isGenerateEnergy = true;
                    generateNum++;
                }
            }
        }

        /// <summary>
        /// �G�l���M�[�����𐶐�����
        /// </summary>
        private void GenerateEnergy()
        {
            for (int i = 0; i < generateNum; ++i)
            {
                base.GenerateEnergy();
                base.RemoveList();
                generateTimeList.RemoveAt(0);
            }
            generateNum = 0;
            isGenerateEnergy = false;
        }

        /// <summary>
        /// ��������G�l���M�[�����̎�ނ�I������
        /// </summary>
        protected override void GenerateEnergyType()
        {
            const int RANDOM_MAX = 100;
            const int MEDIUM_MIN = 40;
            const int MEDIUM_MAX = 90;
            int type = (int)EnergyCharge.ENERGY_TYPE.SMALL;
            int energyNum = Random.Range(0, RANDOM_MAX);
            if (energyNum >= MEDIUM_MIN && energyNum < MEDIUM_MAX)
            {
                type = (int)EnergyCharge.ENERGY_TYPE.MEDIUM;
            }
            else if (energyNum >= MEDIUM_MAX && energyNum < RANDOM_MAX)
            {
                type = (int)EnergyCharge.ENERGY_TYPE.LARGE;
            }
            createEnergyTypeList.Add(type);
        }

        /// <summary>
        /// �G�l���M�[�����𐶐�����
        /// </summary>
        public override void GenerateEnergyResource()
        {
            GenerateEnergyType();
            GeneratePosition();
            generateTimeList.Add(energyGenerateInterval);
        }

        /// <summary>
        /// �G�l���M�[������ǉ��Ő������鐔��Ԃ�
        /// </summary>
        /// <returns>�G�l���M�[������ǉ��Ő������鐔</returns>
        public int GetNumberOfGenerateEnergy()
        {
            int activeEnergy = 0;
            const int CHECK_ENERGY_TYPE = 2;
            for (int i = 0; i < CHECK_ENERGY_TYPE; ++i)
            {
                for (int k = 0; k < energiesList[i].Length; ++k)
                {
                    if (energiesList[i][k].activeSelf)
                    {
                        activeEnergy++;
                    }
                }
            }
            const int MAX_ENERGY = 4;
            return MAX_ENERGY - activeEnergy;
        }
    }
}