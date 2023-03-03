using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class TimeCount : MonoBehaviour
{
    #region
    [SerializeField]
    private string sceneName = "New Scene";

    private TextMeshProUGUI timeCdTMP = null;
    private bool isMain = true;

    public static float secondsCount;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        timeCdTMP = GetComponent<TextMeshProUGUI>();
        secondsCount = 0.0f;
        isMain = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMain)
        {
            secondsCount += Time.deltaTime;
            timeCdTMP.text = ((int)(secondsCount / 60)).ToString("00") + ":" + ((int)secondsCount % 60).ToString("00");
        }
    }

    public static float GetTime()
    {
        return secondsCount;
    }
}
