using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    #region
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] bgm1;

    [SerializeField]
    private AudioClip[] bgm2;

    [SerializeField]
    private int changeKillCount = 5;

    [SerializeField]
    private float FadeOutTime = 5.0f;

    private bool FirstHalf = true;

    private bool IsFadeOut = true;
    private float fadeTime = 0.0f;
    private float defaultVol = 1.0f;
    private int number;
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultVol = audioSource.volume;
        PlayFirstBGM();
    }

    private void Update()
    {
        if (FirstHalf && BossCount.GetKillCount() >= changeKillCount)
        {
            if (IsFadeOut)
            {
                FadeOutBGM();
            }
            else
            {
                PlaySecondBGM();
                FirstHalf = false;
            }
        }
    }

    void FadeOutBGM()
    {
        fadeTime += Time.deltaTime;
        if (fadeTime >= FadeOutTime)
        {
            fadeTime = FadeOutTime;
            IsFadeOut = false;
        }
        audioSource.volume = (float)(1.0 - fadeTime / FadeOutTime);
    }

    void PlayFirstBGM()
    {
        number = Random.Range(0, bgm1.Length);
        audioSource.clip = bgm1[number];
        audioSource.Play();
    }

    public void PlaySecondBGM()
    {
        number= Random.Range(0, bgm2.Length);
        audioSource.clip = bgm2[number];
        audioSource.volume = defaultVol;
        audioSource.Play();
    }
}
