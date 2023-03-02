using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnergyGenerator : EnergyGeneratorBase
{
    private bool isFirstGenerate = false;
    private bool isSecondGenerate = false;
    private bool isSecondGenerateSet = false;

    private const int FIRST_GENERATE_NUM = 2;
    private const int MAX_GENERATE = 4;

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        isFirstGenerate = false;
        isSecondGenerate = false;
        isSecondGenerateSet = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (BossCount.GetKillCount() == 1)
        {
            SecondGenerateSet();
        }
    }

    public override void Generate()
    {
        if (!isFirstGenerate)
        {
            FirstGenerate();
            return;
        }
        GenerateEnergyType();
        GeneratePosition();
        EnergyGenerate();
    }

    protected override void GenerateEnergyType()
    {
        if (isSecondGenerate)
        {
            createEnergyTypeList.Add((int)EnergyCharge.EnergyType.MEDIUM);
            return;
        }

        if (isFirstGenerate)
        {
            createEnergyTypeList.Add((int)EnergyCharge.EnergyType.SMALL);
            return;
        }
    }

    private void EnergyGenerate()
    {
        base.EnergyGenerate();
        base.RemoveList();
    }

    private void FirstGenerate()
    {
        if (isFirstGenerate)
        {
            return;
        }
        isFirstGenerate = true;

        GenerateEnergyType();
        FirstGeneratePosition();
        for (int i = 0; i < FIRST_GENERATE_NUM; i++)
        {
            Vector3 position = new Vector3(createPositionList[i + 1].x, GENERATE_POS_Y, createPositionList[i + 1].z);
            Instantiate(energies[createEnergyTypeList[0]], position, Quaternion.Euler(0.0f, GENERATE_ROT_Y, 0.0f));
        }
        createPositionList.Clear();
        createEnergyTypeList.Clear();
    }

    private void FirstGeneratePosition()
    {
        int generateNum = 0;
        int miss = 0;
        Vector3 genaratePos = Vector3.one;
        while (generateNum < MAX_GENERATE)
        {
            genaratePos.x = Random.Range(createArea[generateNum], createArea[generateNum + 1]);
            genaratePos.z = Random.Range(generatePosMax.position.z, generatePosMin.position.z);
            if (!Physics.CheckBox(genaratePos, halfExtents))
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

    private void SecondGenerateSet()
    {
        if (isSecondGenerateSet)
        {
            return;
        }
        isSecondGenerateSet = true;
        isSecondGenerate = true;
    }
}
