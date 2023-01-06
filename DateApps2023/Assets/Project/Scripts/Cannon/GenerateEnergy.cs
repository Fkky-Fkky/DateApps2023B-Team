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

    private Vector3 halfExtents;

    const int MAX_GENERATE = 5;

    // Start is called before the first frame update
    void Start()
    {
        halfExtents = energy.transform.localScale / 2;
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        int generateNum = 0;
        Vector3 genaratePos;
        while (generateNum < MAX_GENERATE)
        {
            float x = Random.Range(generatePosMin.position.x, generatePosMax.position.x);
            float z = Random.Range(generatePosMin.position.z, generatePosMax.position.z);
            genaratePos = new Vector3(x, generatePosMin.position.y, z);
            if (!Physics.CheckBox(genaratePos, halfExtents))
            {
                Instantiate(energy, genaratePos, Quaternion.identity);
                generateNum++;
            }
        }
    }
}
