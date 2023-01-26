using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnergy : MonoBehaviour
{
    [SerializeField]
    private GameObject energy = null;

    [SerializeField]
    private Transform generatePosMin = null;

    [SerializeField]
    private Transform generatePosMax = null;

    private float[] createArea = new float[6];
    private Vector3 halfExtents = Vector3.zero;
    private List<Vector3> createPositionList = new List<Vector3>();

    const int MAX_GENERATE = 5;
    const int MAX_AREA = 6;
    const float GENERATE_POS_Y = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        halfExtents = energy.transform.localScale / 2;
        float fiveDivide = ((generatePosMin.position.x - generatePosMax.position.x) / 5) * -1;
        for (int i = 0; i < MAX_AREA; i++)
        {
            createArea[i] = generatePosMin.position.x + (fiveDivide * i);
        }
        Generate();
    }

    public void Generate()
    {
        GeneratePosition();
        EnergyGenerate();
    }

    private void GeneratePosition()
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
            }
            else
            {
                miss++;
            }

            if (miss > 30)
            {
                break;
            }
        }
        Debug.Log(miss);
    }

    private void EnergyGenerate()
    {
        for (int i = 0; i < createPositionList.Count; i++)
        {
            Vector3 position = new Vector3(createPositionList[i].x, GENERATE_POS_Y, createPositionList[i].z);
            Instantiate(energy, position, Quaternion.identity);
        }
        createPositionList.Clear();
    }
}
