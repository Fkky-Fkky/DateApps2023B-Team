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

    private int number;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        number = Random.Range(0, bgm1.Length);
        audioSource.clip = bgm1[number];
        audioSource.Play();
    }

    public void PlayBGM2()
    {
        number= Random.Range(0, bgm2.Length);
        audioSource.clip = bgm2[number];
        audioSource.Play();
    }
}
