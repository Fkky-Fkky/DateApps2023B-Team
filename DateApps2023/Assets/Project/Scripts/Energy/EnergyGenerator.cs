// 担当者：吹上純平
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// エネルギー物資の生成をするクラス
    /// </summary>
    public class EnergyGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager = null;

        [SerializeField]
        private NormalEnergyGenerator normalGenerator = null;

        [SerializeField]
        private TutorialEnergyGenerator tutorialGenerator = null;

        private bool isChangeGenerator = false;
        private EnergyGeneratorBase energyGenerator = null;

        // Start is called before the first frame update
        void Start()
        {
            energyGenerator = tutorialGenerator;
            normalGenerator.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (isChangeGenerator)
            {
                return;
            }

            if (gameManager.IsGameStart)
            {
                ChangeEnergyGenerator();
            }
        }

        /// <summary>
        /// エネルギージェネレーターを切り替える
        /// </summary>
        private void ChangeEnergyGenerator()
        {
            const int ADD_ENERGY = 2;
            normalGenerator.gameObject.SetActive(true);
            energyGenerator = normalGenerator;
            for (int i = 0; i < ADD_ENERGY; i++)
            {
                GenerateEnergy();
            }
            isChangeGenerator = true;
            tutorialGenerator.gameObject.SetActive(false);
        }

        /// <summary>
        /// エネルギー物資の生成
        /// </summary>
        public void GenerateEnergy()
        {
            if (gameManager.IsGameOver)
            {
                return;
            }
            energyGenerator.GenerateEnergyResource();
        }
    }
}