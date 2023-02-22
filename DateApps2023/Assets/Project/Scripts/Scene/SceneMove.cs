using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "New Scene";

    private bool SceneChangeFlag = false;
    private bool IsAnimation = false;
    private bool IsPlay = false;

    [SerializeField]
    private Animator AnimationImage = null;

    [SerializeField]
    private float AfterPressTime = 1.0f;

    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlay)
        {
            if (!SceneChangeFlag)
            {
                for (int i = 0; i < Gamepad.all.Count; i++)
                {
                    var gamepad = Gamepad.all[i];
                    if (gamepad.aButton.wasPressedThisFrame)
                    {
                        SceneChangeFlag = true;
                    }
                }
            }
            else
            {
                if (AnimationImage != null)
                {
                    IsAnimation = true;
                    AnimationImage.SetTrigger("AcceptStart");
                    time += Time.deltaTime;
                    if (time >= AfterPressTime)
                    {
                        IsAnimation = false;
                        time = 0.0f;
                    }
                }
                if (!IsAnimation)
                {
                    SceneManager.LoadScene(sceneName);
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
