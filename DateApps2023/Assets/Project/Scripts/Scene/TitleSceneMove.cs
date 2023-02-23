using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneMove : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "New Scene";

    private bool IsPlay = false;
    private bool IsSkip = false;

    [SerializeField]
    private Animator AnimationImage = null;

    [SerializeField]
    private CanvasGroup[] PlayerImage = null;

    private bool[] IsAccept = null;
    private int acceptCount = 0;

    [SerializeField]
    private bool CanSkip = true;


    // Start is called before the first frame update
    void Start()
    {
        IsPlay = false;
        acceptCount = 0;
        Array.Resize(ref IsAccept, PlayerImage.Length);
        for(int i = 0; i < PlayerImage.Length; i++)
        {
            PlayerImage[i].alpha = 0;
            IsAccept[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(acceptCount);
        if (!IsPlay)
        {
            if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                InIdle();
            }
            else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("PressButton"))
            {
                InPressButton();
            }
            else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("ShowManual"))
            {
                InShowManual();
            }
            else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("WaitPlayer"))
            {
                InWaitPlayer();
            }
            else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("HideManual"))
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
                AnimationImage.SetTrigger("AcceptStart");
            }
        }
    }

    private void InPressButton()
    {
        AnimatorStateInfo stateInfo = AnimationImage.GetCurrentAnimatorStateInfo(0);
        if (CanSkip)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad.bButton.wasPressedThisFrame)
                {
                    AnimationImage.Play(stateInfo.fullPathHash, 0, 1);
                    IsSkip = true;
                }
            }
        }
        if (stateInfo.normalizedTime >= 1.0f)
        {
            AnimationImage.SetTrigger("EndChangeScreen");
        }
    }

    private void InShowManual()
    {
        AnimatorStateInfo stateInfo = AnimationImage.GetCurrentAnimatorStateInfo(0);
        if (CanSkip)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad.bButton.wasPressedThisFrame)
                {
                    IsSkip = true;
                }
            }
            if (IsSkip)
            {
                AnimationImage.Play(stateInfo.fullPathHash, 0, 1);
                IsSkip = false;
            }
        }
        if (stateInfo.normalizedTime >= 1.0f)
        {
            AnimationImage.SetTrigger("EndChangeScreen");
        }

    }

    private void InWaitPlayer()
    {
        if (acceptCount >= PlayerImage.Length)
        {
            AnimationImage.SetTrigger("AllAccept");
        }

        if (acceptCount > PlayerImage.Length)
        {
            acceptCount = PlayerImage.Length;
        }
        else if (acceptCount < 0)
        {
            acceptCount = 0;
        }

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var gamepad = Gamepad.all[i];
            if (gamepad.bButton.wasPressedThisFrame)
            {
                if (!IsAccept[i])
                {
                    PlayerImage[i].alpha = 1;

                    acceptCount++;
                    IsAccept[i] = true;
                }
                else
                {
                    PlayerImage[i].alpha = 0;

                    acceptCount--;
                    IsAccept[i] = false;
                }
            }
        }

        
    }

    private void InHideManual()
    {
        AnimatorStateInfo stateInfo = AnimationImage.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1.0f)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnTrueIsPlay()
    {
        IsPlay = true;
    }

    public void OnFalseIsPlay()
    {
        IsPlay = false;
    }
}
