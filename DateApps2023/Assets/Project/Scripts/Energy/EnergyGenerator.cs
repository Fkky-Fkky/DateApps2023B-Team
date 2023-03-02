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

    private EnergyGeneratorBase generator;
        
    private bool IsGeneratorChange = false;

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
            for (int i = 0; i < 2; ++i) {
                Generate();
            }
            generator = normalGenerator;
            IsGeneratorChange = true;
        }
    }

    public void Generate()
    {
        generator.Generate();
    }
}
