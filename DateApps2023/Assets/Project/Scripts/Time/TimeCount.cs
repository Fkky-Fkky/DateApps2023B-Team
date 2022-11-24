using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;

public class TimeCount : MonoBehaviour
{
    [SerializeField]
    private int minutesCount = 3;
    private float secondsCount;

    [SerializeField]
    TextMeshProUGUI timeCdTMP;

    [SerializeField]
    private string sceneName = "New Scene";

    [SerializeField] Animator AnimationImage = null;

    // Start is called before the first frame update
    void Start()
    {
        secondsCount = minutesCount/* * 60*/;
    }

    // Update is called once per frame
    void Update()
    {
        secondsCount -= Time.deltaTime;
        timeCdTMP.text = ((int)(secondsCount / 60)).ToString("00") + ":" + ((int)secondsCount % 60).ToString("00"); ;

        if (secondsCount <= 0)
        {
            AnimationImage.SetBool("Die", true);

            float animTime = AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 1.0f)
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("Die") && AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
        {
            AnimationImage.SetBool("Die", false);
        }

    }
}
