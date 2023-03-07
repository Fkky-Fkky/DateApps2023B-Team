using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreResult : MonoBehaviour
{
    /// <summary>
    /// ゲームクリア時とゲームオーバー時のスコアを表示する
    /// </summary>
    #region
    private TextMeshProUGUI scoreTMP = null;
    private int killCount = 0;
    private float scoreSecondsTime = 0;
    #endregion

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
