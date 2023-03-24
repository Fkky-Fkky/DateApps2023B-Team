// 担当者：吹上純平
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// チュートリアル中に使用する、エネルギー物資の生成処理を行う 
    /// </summary>
    public class TutorialEnergyGenerator : EnergyGeneratorBase
    {
        private bool isFirstGenerate = false;
        private bool isSetSecondGenerate = false;
        private GENERATE_TYPE generateType = 0;

        /// <summary>
        /// ジェネレーターのタイプ
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
        /// エネルギー物資の生成
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
        /// 生成するエネルギー物資の種類を選択
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
        /// 初回生成処理
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
        /// 初回生成時のエネルギー物資を設置する座標生成
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
        /// チュートリアル第二フェーズ移行処理
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
        /// エネルギー物資を補充するかチェックする
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