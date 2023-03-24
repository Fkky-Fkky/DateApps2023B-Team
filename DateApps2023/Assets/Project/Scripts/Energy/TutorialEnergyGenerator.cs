// �S���ҁF���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �`���[�g���A�����Ɏg�p����A�G�l���M�[�����̐����������s�� 
    /// </summary>
    public class TutorialEnergyGenerator : EnergyGeneratorBase
    {
        private bool isFirstGenerate = false;
        private bool isSetSecondGenerate = false;
        private GENERATE_TYPE generateType = 0;

        /// <summary>
        /// �W�F�l���[�^�[�̃^�C�v
        /// </summary>
        private enum GENERATE_TYPE
        {
            FIRST,
            SECOND
        }

        // Start is called before the first frame update
        void Start()
        {
            base.Initialize();
            isFirstGenerate = false;
            isSetSecondGenerate = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!isFirstGenerate)
            {
                return;
            }

            GenerateCheck();

            if (BossCount.GetKillCount() == 1)
            {
                SetSecondGenerate();
                return;
            }
        }

        /// <summary>
        /// �G�l���M�[�����̐���
        /// </summary>
        public override void GenerateEnergyResource()
        {
            if (!isFirstGenerate)
            {
                GenerateFirstEnergy();
                return;
            }
            GenerateEnergyType();
            GeneratePosition();
            base.GenerateEnergy();
            base.RemoveList();
        }

        /// <summary>
        /// ��������G�l���M�[�����̎�ނ�I��
        /// </summary>
        protected override void GenerateEnergyType()
        {
            switch (generateType)
            {
                case GENERATE_TYPE.FIRST:
                    createEnergyTypeList.Add((int)EnergyCharge.ENERGY_TYPE.SMALL);
                    break;

                case GENERATE_TYPE.SECOND:
                    createEnergyTypeList.Add((int)EnergyCharge.ENERGY_TYPE.MEDIUM);
                    break;
            }
        }

        /// <summary>
        /// ���񐶐�����
        /// </summary>
        private void GenerateFirstEnergy()
        {
            if (isFirstGenerate)
            {
                return;
            }
            isFirstGenerate = true;

            GenerateEnergyType();
            GenerateFirstPosition();
            int energyType = createEnergyTypeList[0];
            const int FIRST_GENERATE_NUM = 4;
            GameObject[] generateEnergies = energiesList[energyType];
            for (int i = 0; i < FIRST_GENERATE_NUM; i++)
            {
                Vector3 position = new Vector3(createPositionList[i].x, GENERATE_POS_Y, createPositionList[i].z);
                generateEnergies[i].transform.position = position;
                generateEnergies[i].SetActive(true);
            }
            createPositionList.Clear();
            createEnergyTypeList.Clear();
        }

        /// <summary>
        /// ���񐶐����̃G�l���M�[������ݒu������W����
        /// </summary>
        private void GenerateFirstPosition()
        {
            int miss = 0;
            int generateNum = 0;
            Vector3 genaratePos = Vector3.one;
            const int MAX_GENERATE = 4;
            while (generateNum < MAX_GENERATE)
            {
                genaratePos.x = Random.Range(createArea[generateNum], createArea[generateNum + 1]);
                genaratePos.z = Random.Range(generatePosMax.position.z, generatePosMin.position.z);
                if (!Physics.CheckBox(genaratePos, halfExtent))
                {
                    createPositionList.Add(genaratePos);
                    generateNum++;
                    miss = 0;
                    continue;
                }

                miss++;
                if (miss > MAX_MISS_COUNT)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// �`���[�g���A�����t�F�[�Y�ڍs����
        /// </summary>
        private void SetSecondGenerate()
        {
            if (isSetSecondGenerate)
            {
                return;
            }
            generateType = GENERATE_TYPE.SECOND;
            isSetSecondGenerate = true;
        }

        /// <summary>
        /// �G�l���M�[�������[���邩�`�F�b�N����
        /// </summary>
        private void GenerateCheck()
        {
            for (int i = 0; i < energiesList[(int)generateType].Length; i++)
            {
                if (energiesList[(int)generateType][i].activeSelf)
                {
                    return;
                }
            }

            const int FIRST_BOSS_KILL_COUNT = 1;
            const int TUTORIAL_END_BOSS_KILL_COUNT = 4;
            switch (generateType)
            {
                case GENERATE_TYPE.FIRST:
                    if (BossCount.GetKillCount() > FIRST_BOSS_KILL_COUNT)
                    {
                        return;
                    }
                    break;

                case GENERATE_TYPE.SECOND:
                    if (BossCount.GetKillCount() > TUTORIAL_END_BOSS_KILL_COUNT)
                    {
                        return;
                    }
                    break;
            }
            GenerateEnergyResource();
        }
    }
}