using UnityEngine;

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
        normalGenerator.gameObject.SetActive(true);
        energyGenerator = normalGenerator;
        GenerateEnergy();
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
