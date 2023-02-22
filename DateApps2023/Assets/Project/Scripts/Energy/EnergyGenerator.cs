using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] energies = new GameObject[3];

    [SerializeField]
    private Transform generatePosMin = null;

    [SerializeField]
    private Transform generatePosMax = null;

    [SerializeField]
    float GENERATE_INTERVAL_TIME = 10.0f;

    private float[] createArea = new float[5];
    private bool isFirstGenerate = false;
    private bool isGenerate = false;
    private Vector3 halfExtents = Vector3.zero;
    private List<int> createEnergyTypeList = new List<int>();
    private List<float> createTimeList = new List<float>();
    private List<Vector3> createPositionList = new List<Vector3>();

    const int MAX_GENERATE = 4;
    const int MAX_AREA = 5;
    const int RANDOM_MAX = 100;
    const int MEDIUM_MIN = 40;
    const int MEDIUM_MAX = 90;
    const int MAX_MISS_COUNT = 30;
    const float GENERATE_POS_Y = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < energies.Length -1; i++)
        {
            int mySize = energies[i].GetComponent<CarryEnergy>().MyItemSizeCount;
            for (int j = i + 1; j < energies.Length; j++)
            {
                int nextSize = energies[j].GetComponent<CarryEnergy>().MyItemSizeCount;
                if(mySize > nextSize)
                {
                    GameObject index = energies[i];
                    energies[i] = energies[j];
                    energies[j] = index;
                }
            }
        }
        halfExtents = energies[(int)EnergyCharge.EnergyType.LARGE].transform.localScale / 2;

        float fourDivide = ((generatePosMin.position.x - generatePosMax.position.x) / MAX_GENERATE) * -1;
        for (int i = 0; i < MAX_AREA; i++)
        {
            createArea[i] = generatePosMin.position.x + (fourDivide * i);
        }
        FirstGenerate();
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

    public void Generate()
    {
        GenerateEnergyType();
        GeneratePosition();
        createTimeList.Add(GENERATE_INTERVAL_TIME);
    }

    private void GenerateEnergyType()
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

    private void GeneratePosition()
    {
        int generateNum = Random.Range(0, MAX_GENERATE);
        Vector3 genaratePos;
        int miss = 0;
        while (miss < MAX_MISS_COUNT)
        {
            float x = Random.Range(createArea[generateNum], createArea[generateNum + 1]);
            float z = Random.Range(generatePosMax.position.z, generatePosMin.position.z);
            genaratePos = new Vector3(x, generatePosMax.position.y, z);
            if (!Physics.CheckBox(genaratePos, halfExtents))
            {
                createPositionList.Add(genaratePos);
                break;
            }
            miss++;
        }
        Debug.Log(miss);
    }

    private void EnergyGenerate()
    {
        Vector3 position = new Vector3(createPositionList[0].x, GENERATE_POS_Y, createPositionList[0].z);
        Instantiate(energies[createEnergyTypeList[0]], position, Quaternion.Euler(0.0f, 180.0f, 0.0f));
        isGenerate = true;

    }

    private void RemoveList()
    {
        createTimeList.RemoveAt(0);
        createEnergyTypeList.RemoveAt(0);
        createPositionList.RemoveAt(0);
        isGenerate = false;
    }

    public void FirstGenerate()
    {
        if (isFirstGenerate)
        {
            return;
        }
        FirstGeneratePosition();
        FirstEnergyGenerate();
        isFirstGenerate = true;
    }

    private void FirstGeneratePosition()
    {
        int generateNum = 0;
        int miss = 0;
        Vector3 genaratePos;
        while (generateNum < MAX_GENERATE)
        {
            float x = Random.Range(createArea[generateNum], createArea[generateNum + 1]);
            float z = Random.Range(generatePosMax.position.z, generatePosMin.position.z);
            genaratePos = new Vector3(x, generatePosMax.position.y, z);
            if (!Physics.CheckBox(genaratePos, halfExtents))
            {
                createPositionList.Add(genaratePos);
                generateNum++;
                continue;
            }

            miss++;
            if (miss > MAX_MISS_COUNT)
            {
                break;
            }
        }
        Debug.Log(miss);
    }

    private void FirstEnergyGenerate()
    {
        for (int i = 0; i < createPositionList.Count; i++)
        {
            Vector3 position = new Vector3(createPositionList[i].x, GENERATE_POS_Y, createPositionList[i].z);
            Instantiate(energies[(int)EnergyCharge.EnergyType.SMALL], position, Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
        createPositionList.Clear();
    }
}
