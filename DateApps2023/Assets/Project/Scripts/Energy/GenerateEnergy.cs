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
    private Vector3 halfExtents;

    const int MAX_GENERATE = 5;

    // Start is called before the first frame update
    void Start()
    {
        halfExtents = energy.transform.localScale / 2;
        float fiveDivide = ((generatePosMin.position.x - generatePosMax.position.x) / 5) * -1;
        for (int i = 0; i < 6; i++)
        {
            createArea[i] = generatePosMin.position.x + (fiveDivide * i);
        }
        Generate();
    }

    public void Generate()
    {
        int generateNum = 0;
        Vector3 genaratePos;
        int miss = 0;
        //while (generateNum < MAX_GENERATE)
        //{
        //    float x = Random.Range(generatePosMin.position.x, generatePosMax.position.x);
        //    float z = Random.Range(generatePosMin.position.z, generatePosMax.position.z);
        //    genaratePos = new Vector3(x, generatePosMin.position.y, z);
        //    if (!Physics.CheckBox(genaratePos, halfExtents))
        //    {
        //        Instantiate(energy, genaratePos, Quaternion.identity);
        //        generateNum++;
        //    }
        //    else
        //    {
        //        miss++;
        //    }
        //    if (miss > 10)
        //    {
        //        break;
        //    }
        //}

        while (generateNum < MAX_GENERATE)
        {
            float x = Random.Range(createArea[generateNum], createArea[generateNum + 1]);
            float z = Random.Range(generatePosMax.position.z, generatePosMin.position.z);
            genaratePos = new Vector3(x, generatePosMin.position.y, z);
            if (!Physics.CheckBox(genaratePos, halfExtents))
            {
                Instantiate(energy, genaratePos, Quaternion.identity);
                generateNum++;
            }
            else
            {
                miss++;
            }

            if (miss > 10)
            {
                break;
            }
        }
        Debug.Log(miss);
    }
}
