using UnityEngine;

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

    private bool IsGeneratorChange = false;
    private EnergyGeneratorBase generator = null;

    // Start is called before the first frame update
    void Start()
    {
        generator = tutorialGenerator;
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }

        if (IsGeneratorChange)
        {
            return;
        }

        if (gameManager.IsGameStart)
        {
            generator = normalGenerator;
            Generate();
            IsGeneratorChange = true;
            tutorialGenerator.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// エネルギー物資の生成
    /// </summary>
    public void Generate()
    {
        generator.Generate();
    }
}
