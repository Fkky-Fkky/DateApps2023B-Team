using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            const int ADD_GENERATE = 2;
            for (int i = 0; i < ADD_GENERATE; ++i)
            {
                Generate();
            }
            IsGeneratorChange = true;
        }
    }

    public void Generate()
    {
        generator.Generate();
    }
}
