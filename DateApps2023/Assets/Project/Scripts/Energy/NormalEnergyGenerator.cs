using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnergyGenerator : EnergyGeneratorBase
{
    [SerializeField]
    private float GENERATE_INTERVAL_TIME = 10.0f;

    private List<float> createTimeList = new List<float>();

    private bool isGenerate = false;
    const int RANDOM_MAX = 100;
    const int MEDIUM_MIN = 40;
    const int MEDIUM_MAX = 90;

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        isGenerate = false;
    }

    private void Update()
    {
        for (int i = 0; i < createTimeList.Count; i++)
        {
            createTimeList[i] -= Time.deltaTime;
            if (createTimeList[i] <= 0)
            {
                EnergyGenerate();
            }
        }

        if (isGenerate)
        {
            RemoveList();
        }
    }

    public override void Generate()
    {
        GenerateEnergyType();
        GeneratePosition();
        createTimeList.Add(GENERATE_INTERVAL_TIME);
    }

    protected override void GenerateEnergyType()
    {
        int type = (int)EnergyCharge.EnergyType.SMALL;
        int energyNum = Random.Range(0, RANDOM_MAX);
        if (energyNum >= MEDIUM_MIN && energyNum < MEDIUM_MAX)
        {
            type = (int)EnergyCharge.EnergyType.MEDIUM;
        }
        else if (energyNum >= MEDIUM_MAX && energyNum < RANDOM_MAX)
        {
            type = (int)EnergyCharge.EnergyType.LARGE;
        }
        createEnergyTypeList.Add(type);
    }

    protected void EnergyGenerate()
    {
        base.EnergyGenerate();
        isGenerate = true;
    }

    private void RemoveList()
    {
        base.RemoveList();
        createTimeList.RemoveAt(0);
        isGenerate = false;
    }
}
