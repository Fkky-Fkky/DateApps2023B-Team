using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

/// <summary>
/// �Q�[���{�҂̎��ԕ\���Ɋւ���N���X
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
    /// �֐����Ăяo�����O���̃X�N���v�g�Ɍo�ߎ��Ԃ̏��𑗂�
    /// </summary>
    /// <returns></returns>
    public static float GetTime()
    {
        return SecondsCount;
    }
}
