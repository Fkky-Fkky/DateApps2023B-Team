using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[�����Ɏg�p����A�G�l���M�[�����̐����������s���N���X
/// </summary>
public class NormalEnergyGenerator : EnergyGeneratorBase
{
    [SerializeField]
    private float GENERATE_INTERVAL_TIME = 10.0f;

    private bool isGenerate = false;
    private List<float> createTimeList = new List<float>();

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

    /// <summary>
    /// �G�l���M�[�����𐶐�����
    /// </summary>
    public override void Generate()
    {
        GenerateEnergyType();
        GeneratePosition();
        createTimeList.Add(GENERATE_INTERVAL_TIME);
    }

    /// <summary>
    /// ��������G�l���M�[�����̎�ނ�I������
    /// </summary>
    protected override void GenerateEnergyType()
    {
        const int RANDOM_MAX = 100;
        const int MEDIUM_MIN = 40;
        const int MEDIUM_MAX = 90;
        int type = (int)EnergyCharge.ENERGY_TYPE.SMALL;
        int energyNum = Random.Range(0, RANDOM_MAX);
        if (energyNum >= MEDIUM_MIN && energyNum < MEDIUM_MAX)
        {
            type = (int)EnergyCharge.ENERGY_TYPE.MEDIUM;
        }
        else if (energyNum >= MEDIUM_MAX && energyNum < RANDOM_MAX)
        {
            type = (int)EnergyCharge.ENERGY_TYPE.LARGE;
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
