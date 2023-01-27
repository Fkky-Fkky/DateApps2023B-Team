using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreResult : MonoBehaviour
{
    private float scoreSecondsTime;
    TextMeshProUGUI timeTMP;

    // Start is called before the first frame update
    void Start()
    {
        timeTMP = GetComponent<TextMeshProUGUI>();
        scoreSecondsTime = TimeCount.GetTime();
        timeTMP.text = "Time  " + ((int)(scoreSecondsTime / 60)).ToString("00") + ":" + ((int)scoreSecondsTime % 60).ToString("00");
    }
}
