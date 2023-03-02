using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    #region
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] firstBGM;

    [SerializeField]
    private AudioClip[] secondBGM;

    [SerializeField]
    private int changeKillCount = 5;

    [SerializeField]
    private float fadeOutTime = 5.0f;

    private bool isFirstHalf = true;
    private bool isFadeOut = true;
    private float fadeTime = 0.0f;
    private float defaultVol = 1.0f;
    private int number;
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultVol = audioSource.volume;
        number = Random.Range(0, firstBGM.Length);
        audioSource.clip = firstBGM[number];
        audioSource.Play();
    }

    private void Update()
    {
        if (isFirstHalf)
        {
            OnFirstHalf();
        }
    }

    void OnFirstHalf()
    {
        if (BossCount.GetKillCount() >= changeKillCount)
        {
            if (isFadeOut)
            {
                fadeTime += Time.deltaTime;
                if (fadeTime >= fadeOutTime)
                {
                    fadeTime = fadeOutTime;
                    isFadeOut = false;
                }
                audioSource.volume = (float)(1.0 - fadeTime / fadeOutTime);
            }
            else
            {
                PlaySecondBGM();
                isFirstHalf = false;
            }
        }
    }

    public void PlaySecondBGM()
    {
        number= Random.Range(0, secondBGM.Length);
        audioSource.clip = secondBGM[number];
        audioSource.volume = defaultVol;
        audioSource.Play();
    }
}
