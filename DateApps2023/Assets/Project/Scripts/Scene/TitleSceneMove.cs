using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneMove : MonoBehaviour
{
    #region
    [SerializeField]
    private string sceneName = "New Scene";

    private bool isPlay = false;
    private bool isSkip = false;

    [SerializeField]
    private Animator animationImage = null;

    [SerializeField]
    private CanvasGroup[] playerBackImage = null;

    [SerializeField]
    private CanvasGroup[] playerImage = null;

    private bool[] isAccept = null;
    private int acceptCount = 0;

    [SerializeField]
    private bool hasCanSkip = true;

    [SerializeField]
    private AudioClip pressButtonSound = null;

    [SerializeField]
    private AudioClip[] acceptSound = null;

    [SerializeField]
    private AudioClip[] cancelSound = null;

    private AudioSource audioSource;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        isPlay = false;
        acceptCount = 0;
        Array.Resize(ref isAccept, playerImage.Length);
        for(int i = 0; i < playerImage.Length; i++)
        {
            isAccept[i] = false;
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlay)
        {
            if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                InIdle();
            }
            else if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("PressButton"))
            {
                InPressButton();
            }
            else if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("ShowManual"))
            {
                InShowManual();
            }
            else if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("WaitPlayer"))
            {
                InWaitPlayer();
            }
            else if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("HideManual"))
            {
                InHideManual();
            }
        }
    }

    private void InIdle()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var gamepad = Gamepad.all[i];
            if (gamepad.bButton.wasPressedThisFrame)
            {
                animationImage.SetTrigger("AcceptStart");
                audioSource.PlayOneShot(pressButtonSound);
            }
        }
    }

    private void InPressButton()
    {
        AnimatorStateInfo stateInfo = animationImage.GetCurrentAnimatorStateInfo(0);
        if (hasCanSkip)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad.bButton.wasPressedThisFrame)
                {
                    animationImage.Play(stateInfo.fullPathHash, 0, 1);
                    isSkip = true;
                }
            }
        }
        if (stateInfo.normalizedTime >= 1.0f)
        {
            animationImage.SetTrigger("EndChangeScreen");
        }
    }

    private void InShowManual()
    {
        AnimatorStateInfo stateInfo = animationImage.GetCurrentAnimatorStateInfo(0);
        if (hasCanSkip)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad.bButton.wasPressedThisFrame)
                {
                    isSkip = true;
                }
            }
            if (isSkip)
            {
                animationImage.Play(stateInfo.fullPathHash, 0, 1);
                isSkip = false;
            }
        }
        if (stateInfo.normalizedTime >= 1.0f)
        {
            animationImage.SetTrigger("EndChangeScreen");
        }

    }

    private void InWaitPlayer()
    {
        if (acceptCount >= playerImage.Length)
        {
            animationImage.SetTrigger("AllAccept");
        }

        if (acceptCount > playerImage.Length)
        {
            acceptCount = playerImage.Length;
        }
        else if (acceptCount < 0)
        {
            acceptCount = 0;
        }

       PressPlayer();
    }

    void PressPlayer()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var gamepad = Gamepad.all[i];
            if (gamepad.aButton.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (gamepad.bButton.wasPressedThisFrame)
            {
                if (!isAccept[i])
                {
                    audioSource.PlayOneShot(acceptSound[i]);
                    animationImage.SetBool("ShowP" + (i + 1), true);

                    acceptCount++;
                    isAccept[i] = true;
                }
                else
                {
                    audioSource.PlayOneShot(cancelSound[i]);
                    animationImage.SetBool("ShowP" + (i + 1), false);

                    acceptCount--;
                    isAccept[i] = false;
                }
            }
        }
    }

    private void InHideManual()
    {
        AnimatorStateInfo stateInfo = animationImage.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1.0f)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnTrueIsPlay()
    {
        isPlay = true;
    }

    public void OnFalseIsPlay()
    {
        isPlay = false;
    }
}
