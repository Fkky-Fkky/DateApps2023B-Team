// �S���ҁF���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �G�l���M�[�����̐���������N���X
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
        /// �G�l���M�[�W�F�l���[�^�[��؂�ւ���
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
        /// �G�l���M�[�����̐���
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