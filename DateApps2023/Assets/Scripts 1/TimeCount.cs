using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCount : MonoBehaviour
{
    [SerializeField]
    private int minutesCount = 3;
    private float secondsCount;

    [SerializeField]
    TextMeshProUGUI timeCdTMP;

    [SerializeField]
    private string sceneName = "New Scene";

    // Start is called before the first frame update
    void Start()
    {
        secondsCount = minutesCount * 60;
    }

    // Update is called once per frame
    void Update()
    {
        secondsCount -= Time.deltaTime;
        timeCdTMP.text = ((int)(secondsCount / 60)).ToString("00") + ":" + ((int)secondsCount % 60).ToString("00"); ;

        if (secondsCount <= 0)
        {
            SceneManager.LoadScene(sceneName);

        }

    }
}
