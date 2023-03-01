using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreResult : MonoBehaviour
{
    private float scoreSecondsTime;
    private int killCount;
    private TextMeshProUGUI scoreTMP;

    // Start is called before the first frame update
    void Start()
    {
        scoreTMP = GetComponent<TextMeshProUGUI>();
        scoreSecondsTime = TimeCount.GetTime();
        killCount = BossCount.GetKillCount();
        scoreTMP.text = 
            "Time  " + ((int)(scoreSecondsTime / 60)).ToString("00") + ":" + ((int)scoreSecondsTime % 60).ToString("00") 
            + "\n" + "Boss  " + ((int)killCount).ToString("00");
    }
}
