using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stagePattern;

    [SerializeField]
    private Vector3 generatePos = Vector3.zero;

    private int number;

    // Start is called before the first frame update
    void Start()
    {
        OnGenerate();
    }

    void OnGenerate()
    {
        number = Random.Range(0, stagePattern.Length);
        Instantiate(stagePattern[number], generatePos, Quaternion.identity);
    }
}
