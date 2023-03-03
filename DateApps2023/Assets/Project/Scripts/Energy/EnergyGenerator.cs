using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager = null;

    [SerializeField]
    private NormalEnergyGenerator normalGenerator;

    [SerializeField]
    private TutorialEnergyGenerator tutorialGenerator;

    private bool IsGeneratorChange = false;
    private EnergyGeneratorBase generator;

    private const int ADD_GENERATE = 2;

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

        if (gameManager.IsGammeStart)
        {
            generator = normalGenerator;
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
