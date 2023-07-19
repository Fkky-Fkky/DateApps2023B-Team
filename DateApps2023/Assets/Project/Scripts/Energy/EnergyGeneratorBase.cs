// �S���ҁF���㏃��
using System.Collections.Generic;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �G�l���M�[�W�F�l���[�^�[�̊��N���X
    /// </summary>
    public abstract class EnergyGeneratorBase : MonoBehaviour
    {
        [SerializeField]
        protected private GameObject[] smallEnergies = new GameObject[MAX_ENERGY];

        [SerializeField]
        protected private GameObject[] mediumEnergies = new GameObject[MAX_ENERGY];

        [SerializeField]
        protected private GameObject[] largeEnergies = new GameObject[MAX_ENERGY];

        [SerializeField]
        protected private Transform generatePosMin = null;

        [SerializeField]
        protected private Transform generatePosMax = null;

        protected float[] createArea = new float[MAX_AREA + 1];
        protected List<GameObject[]> energiesList = new List<GameObject[]>();
        protected List<int> createEnergyTypeList = new List<int>();
        protected List<Vector3> createPositionList = new List<Vector3>();

        protected const int MAX_AREA_SEARCH_COUNT = 30;
        protected const float GENERATE_POS_Y = 20.0f;
        protected const float GENERATE_ROT_Y = 180.0f;
        protected Vector3 halfExtent = Vector3.zero;

        private const int MAX_AREA = 4;
        private const int MAX_ENERGY = 4;

        /// <summary>
        /// �G�l���M�[������ݒu����ꏊ���쐬
        /// </summary>
        protected void GeneratePosition()
        {
            int areaSearchCount = 0;
            int areaIndex = Random.Range(0, MAX_AREA);
            Vector3 genaratePos = Vector3.zero;
            const float GENERATE_POS_Y = 0.5f;
            genaratePos.y = GENERATE_POS_Y;
            while (areaSearchCount < MAX_AREA_SEARCH_COUNT)
            {
                genaratePos.x = Random.Range(createArea[areaIndex], createArea[areaIndex + 1]);
                genaratePos.z = Random.Range(generatePosMax.position.z, generatePosMin.position.z);
                if (!Physics.CheckBox(genaratePos, halfExtent))
                {
                    createPositionList.Add(genaratePos);
                    break;
                }
                areaSearchCount++;
            }
        }

        /// <summary>
        /// �G�l���M�[�����̐���
        /// </summary>
        protected void GenerateEnergy()
        {
            int energyType = createEnergyTypeList[0];
            GameObject[] generateEnergies = energiesList[energyType];
            GameObject generateEnergy = null;
            const int MAX_INDEX = 4;

            for (int i = 0; i < MAX_INDEX; ++i) {
                if (!generateEnergies[i].activeSelf)
                {
                    generateEnergy = generateEnergies[i];
                    break;
                }
            }

            Vector3 position = new Vector3(createPositionList[0].x, GENERATE_POS_Y, createPositionList[0].z);
            generateEnergy.transform.position = position;
            generateEnergy.SetActive(true);
        }

        /// <summary>
        /// ��������G�l���M�[�̎�ނƁA�ݒu�ꏊ�̃��X�g��0�Ԗڂ��폜����
        /// </summary>
        protected void RemoveList()
        {
            createEnergyTypeList.RemoveAt(0);
            createPositionList.RemoveAt(0);
        }

        /// <summary>
        /// ��������G�l���M�[�����̎�ނ�I������
        /// </summary>
        protected abstract void GenerateEnergyType();

        /// <summary>
        /// �G�l���M�[�����𐶐�����
        /// </summary>
        public abstract void GenerateEnergyResource();

        /// <summary>
        /// ����������
        /// </summary>
        public void Initialize()
        {
            const float HALF = 0.5f;
            halfExtent = largeEnergies[0].transform.localScale * HALF;

            //�G�l���M�[������ݒu����G���A�����X�g�ɒǉ�
            float fourDivide = (generatePosMax.position.x - generatePosMin.position.x) / MAX_AREA;
            for (int i = 0; i <= MAX_AREA; i++)
            {
                createArea[i] = generatePosMin.position.x + (fourDivide * i);
            }
            energiesList.Add(smallEnergies);
            energiesList.Add(mediumEnergies);
            energiesList.Add(largeEnergies);
        }


        /// <summary>
        /// �G�l���M�[�����Ɏg�p���郊�X�g������������
        /// </summary>
        public void ClearList()
        {
            createEnergyTypeList.Clear();
            createPositionList.Clear();
        }
    }
}