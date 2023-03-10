using TMPro;
using UnityEngine;

/// <summary>
/// スコアリザルトの表示に関するクラス
/// </summary>
public class ScoreResult : MonoBehaviour
{
    private TextMeshProUGUI scoreTMP = null;
    private int killCount = 0;
    private float scoreSecondsTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        killCount = BossCount.GetKillCount();
        scoreSecondsTime = TimeCount.GetTime();

        scoreTMP = GetComponent<TextMeshProUGUI>();
        scoreTMP.text = "Time  " + ((int)(scoreSecondsTime / 60)).ToString("00") + ":" + ((int)scoreSecondsTime % 60).ToString("00") 
               + "\n" + "Boss  " + ((int)killCount).ToString("00");
    }
}
