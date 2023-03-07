using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネルギージェネレーターの基底クラス
/// </summary>
public abstract class EnergyGeneratorBase : MonoBehaviour
{
    [SerializeField]
    protected private GameObject[] energies = new GameObject[3];

    [SerializeField]
    protected private Transform generatePosMin = null;

    [SerializeField]
    protected private Transform generatePosMax = null;

    protected float[] createArea = new float[5];
    protected Vector3[] halfExtents = new Vector3[3];
    protected List<int> createEnergyTypeList = new List<int>();
    protected List<Vector3> createPositionList = new List<Vector3>();

    protected const int MAX_MISS_COUNT = 30;
    protected const float GENERATE_POS_Y = 20.0f;
    protected const float GENERATE_ROT_Y = 180.0f;

    private const int MAX_AREA = 4;

    /// <summary>
    /// 継承先のStartで呼ばれる初期化処理
    /// </summary>
    protected void Initialize()
    {
        SortEnergies();

        const int HALF = 2;
        for (int i = 0; i < energies.Length; i++)
        {
            halfExtents[i] = energies[i].transform.localScale / HALF;
        }

        float fourDivide = (generatePosMax.position.x - generatePosMin.position.x) / MAX_AREA;
        for (int i = 0; i <= MAX_AREA; i++)
        {
            createArea[i] = generatePosMin.position.x + (fourDivide * i);
        }
    }

    /// <summary>
    /// エネルギー物資をサイズ順に並べ替える
    /// </summary>
    private void SortEnergies()
    {
        for (int i = 0; i < energies.Length - 1; i++)
        {
            int mySize = energies[i].GetComponent<CarryEnergy>().MyItemSizeCount;
            for (int j = i + 1; j < energies.Length; j++)
            {
                int nextSize = energies[j].GetComponent<CarryEnergy>().MyItemSizeCount;
                if (mySize > nextSize)
                {
                    GameObject index = energies[i];
                    energies[i] = energies[j];
                    energies[j] = index;
                }
            }
        }
    }

    /// <summary>
    /// エネルギー物資を設置する場所を作成
    /// </summary>
    protected void GeneratePosition()
    {
        int miss = 0;
        int energyTypeIndex = createEnergyTypeList.Count - 1;
        int energyType = createEnergyTypeList[energyTypeIndex];
        int areaIndex  = Random.Range(0, MAX_AREA);
        Vector3 genaratePos = Vector3.zero;
        genaratePos.y = 0.5f;
        while (miss < MAX_MISS_COUNT)
        {
            genaratePos.x = Random.Range(createArea[areaIndex], createArea[areaIndex + 1]);
            genaratePos.z = Random.Range(generatePosMax.position.z, generatePosMin.position.z);
            if (!Physics.CheckBox(genaratePos, halfExtents[energyType]))
            {
                createPositionList.Add(genaratePos);
                break;
            }
            miss++;
        }
    }

    /// <summary>
    /// エネルギー物資の生成
    /// </summary>
    protected void EnergyGenerate()
    {
        Vector3 position = new Vector3(createPositionList[0].x, GENERATE_POS_Y, createPositionList[0].z);
        Instantiate(energies[createEnergyTypeList[0]], position, Quaternion.Euler(0.0f, GENERATE_ROT_Y, 0.0f));
    }

    /// <summary>
    /// 生成するエネルギーの種類と、設置場所のリストの0番目を削除する
    /// </summary>
    protected void RemoveList()
    {
        createEnergyTypeList.RemoveAt(0);
        createPositionList.RemoveAt(0);
    }

    /// <summary>
    /// 生成するエネルギー物資の種類を選択する
    /// </summary>
    protected abstract void GenerateEnergyType();
    
    /// <summary>
    /// エネルギー物資を生成する
    /// </summary>
    public abstract void Generate();
}
