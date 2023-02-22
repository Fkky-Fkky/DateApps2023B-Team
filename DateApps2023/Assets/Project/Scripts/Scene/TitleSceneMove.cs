using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleSceneMove : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "New Scene";

    private bool ChangeScreenFlag = false;
    private bool IsAllReady = false;
    private bool IsPlay = false;

    [SerializeField]
    private Animator AnimationImage = null;

    [SerializeField]
    private GameObject[] PlayerImage = null;
    private bool[] IsAccept = null;
    private int acceptCount = 0;



    // Start is called before the first frame update
    void Start()
    {
        ChangeScreenFlag = false;
        IsAllReady = false;
        IsPlay = false;
        acceptCount = 0;
        Array.Resize(ref IsAccept, PlayerImage.Length);
        for(int i = 0; i < IsAccept.Length; i++)
        {
            IsAccept[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlay)
        {
            if (!ChangeScreenFlag)
            {
                for (int i = 0; i < Gamepad.all.Count; i++)
                {
                    var gamepad = Gamepad.all[i];
                    if (gamepad.aButton.wasPressedThisFrame)
                    {
                        ChangeScreenFlag = true;
                        AnimationImage.SetTrigger("AcceptStart");
                    }
                }
            }
            else
            {
                if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("WaitPlayer"))
                {
                    if (acceptCount >= PlayerImage.Length)
                    {
                        AnimationImage.SetTrigger("AllAccept");
                    }

                    for (int i = 0; i < Gamepad.all.Count; i++)
                    {
                        var gamepad = Gamepad.all[i];
                        if (gamepad.aButton.wasPressedThisFrame)
                        {
                            if (!IsAccept[i])
                            {
                                PlayerImage[i].SetActive(true);
                                acceptCount++;

                                if (acceptCount > PlayerImage.Length)
                                {
                                    acceptCount = PlayerImage.Length;
                                }
                                IsAccept[i] = true;
                            }
                            else
                            {
                                PlayerImage[i].SetActive(false);
                                acceptCount--;

                                if (acceptCount < 0)
                                {
                                    acceptCount = 0;
                                }
                                IsAccept[i] = false;
                            }
                        }

                    }
                }
                else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("HideManual"))
                {
                    AnimatorStateInfo stateInfo = AnimationImage.GetCurrentAnimatorStateInfo(0);

                    if (IsAllReady)
                    {
                        if (stateInfo.normalizedTime >= 1.0f)
                        {
                            SceneManager.LoadScene(sceneName);
                            IsAllReady = false;
                        }
                    }
                }


                        
            }
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
