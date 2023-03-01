using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    #region
    [SerializeField]
    private string sceneName = "New Scene";

    private bool SceneChangeFlag = false;
    private bool IsAnimation = false;

    [SerializeField]
    private Animator AnimationImage = null;

    [SerializeField]
    private float AfterPressTime = 1.0f;

    private float time = 0.0f;

    [SerializeField]
    private AudioClip sceneVoice = null;
    private AudioSource audioSource;

    private bool isPlaying = false;

    [SerializeField]
    private float beforeVoiceTime = 1.0f;

    [SerializeField]
    private float changeTime = 20.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SceneChangeFlag = false;
        IsAnimation = false;
        time = 0.0f;
        if(sceneVoice != null )
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 1.0f;
            isPlaying = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!SceneChangeFlag)
        {
            NotPressButton();
        }
        else
        {
            OnPressButton();
        }
    }

    void NotPressButton()
    {
        time += Time.deltaTime;
        if (time >= changeTime)
        {
            SceneChangeFlag = true;
            time = 0.0f;
        }
        if (isPlaying)
        {
            if (time >= beforeVoiceTime)
            {
                audioSource.PlayOneShot(sceneVoice);
                isPlaying = false;
            }
        }
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var gamepad = Gamepad.all[i];
            if (gamepad.bButton.wasPressedThisFrame)
            {
                SceneChangeFlag = true;
                time = 0.0f;
            }
        }
    }

    void OnPressButton()
    {
        time += Time.deltaTime;

        if (AnimationImage != null)
        {
            IsAnimation = true;
            AnimationImage.SetTrigger("AcceptStart");
            if (time >= AfterPressTime)
            {
                IsAnimation = false;
                time = 0.0f;
            }
        }
        if (sceneVoice != null)
        {
            audioSource.volume = (float)(1.0 - time / AfterPressTime);

        }
        if (!IsAnimation)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
