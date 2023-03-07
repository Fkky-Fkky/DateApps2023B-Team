using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

/// <summary>
/// ゲーム本編の時間表示に関するクラス
/// </summary>
public class TimeCount : MonoBehaviour
{
    #region
    [SerializeField]
    private string sceneName = "New Scene";

    private TextMeshProUGUI timeCdTMP = null;
    private bool isMain = true;

    public static float SecondsCount = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        timeCdTMP = GetComponent<TextMeshProUGUI>();
        SecondsCount = 0.0f;
        isMain = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMain)
        {
            SecondsCount += Time.deltaTime;
            timeCdTMP.text = ((int)(SecondsCount / 60)).ToString("00") + ":" + ((int)SecondsCount % 60).ToString("00");
        }
    }

    /// <summary>
    /// 関数を呼び出した外部のスクリプトに経過時間の情報を送る
    /// </summary>
    /// <returns></returns>
    public static float GetTime()
    {
        return SecondsCount;
    }
}
