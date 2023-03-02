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

    private bool isSceneChange = false;
    private bool isAnimation = false;

    [SerializeField]
    private Animator animationImage = null;

    [SerializeField]
    private float afterPressTime = 1.0f;

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
        isSceneChange = false;
        isAnimation = false;
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
        
        if (!isSceneChange)
        {
            NotSceneChange();
        }
        else
        {
           OnSceneChange();
        }
    }

    void NotSceneChange()
    {
        time += Time.deltaTime;
        if (time >= changeTime)
        {
            isSceneChange = true;
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
                isSceneChange = true;
                time = 0.0f;
            }
        }
    }

    void OnSceneChange()
    {
        time += Time.deltaTime;

        if (animationImage != null)
        {
            isAnimation = true;
            animationImage.SetTrigger("AcceptStart");
            if (time >= afterPressTime)
            {
                isAnimation = false;
                time = 0.0f;
            }
        }
        if (sceneVoice != null)
        {
            audioSource.volume = (float)(1.0 - time / afterPressTime);

        }
        if (!isAnimation)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
