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

    private bool FirstHalf = true;
    private int number;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                PlayBGM2();
                FirstHalf = false;
            }
        }
        
    }

    public void PlayBGM2()
    {
        number= Random.Range(0, bgm2.Length);
        audioSource.clip = bgm2[number];
        audioSource.Play();
    }
}
