using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
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


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultVol = audioSource.volume;
        number = Random.Range(0, bgm1.Length);
        audioSource.clip = bgm1[number];
        audioSource.Play();
    }

    private void Update()
    {
        if (FirstHalf)
        {
            if (BossCount.GetKillCount() >= changeKillCount)
            {
                if (IsFadeOut)
                {
                    fadeTime += Time.deltaTime;
                    if (fadeTime >= FadeOutTime)
                    {
                        fadeTime = FadeOutTime;
                        IsFadeOut = false;
                    }
                    audioSource.volume = (float)(1.0 - fadeTime / FadeOutTime);
                }
                else
                {
                    PlayBGM2();
                    FirstHalf = false;
                }
            }
        }
        
    }

    public void PlayBGM2()
    {
        number= Random.Range(0, bgm2.Length);
        audioSource.clip = bgm2[number];
        audioSource.volume = defaultVol;
        audioSource.Play();
    }
}
