using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TitleVideoManager : MonoBehaviour
{
    [SerializeField]
    private Animator AnimationImage = null;

    [SerializeField]
    private float NonPlayTime = 11.0f;
    private float time = 0.0f;

    private SceneMove sceneMove;

    // Start is called before the first frame update
    void Start()
    {
        sceneMove = GetComponent<SceneMove>();
        sceneMove.OnTrueIsPlay();
    }

    // Update is called once per frame
    void Update()
    {

        if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            AnimatorStateInfo stateInfo = AnimationImage.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1.0f)
            {
                AnimationImage.SetTrigger("EndLogoAnim");
            }
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad.aButton.wasPressedThisFrame)
                {
                    AnimationImage.Play(stateInfo.fullPathHash, 0, 1);
                }
            }
        }
        else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            time += Time.deltaTime;
            if (time >= NonPlayTime)
            {
                sceneMove.OnTrueIsPlay();
                AnimationImage.SetBool("EndVideo", false);
                AnimationImage.SetTrigger("StartVideo");
                time = 0.0f;
            }
            else
            {
                sceneMove.OnFalseIsPlay();
            }
        }
        else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("StartVideo") || AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("PlayVideo"))
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad.aButton.wasPressedThisFrame)
                {
                    AnimatorStateInfo stateInfo = AnimationImage.GetCurrentAnimatorStateInfo(0);
                    AnimationImage.Play(stateInfo.fullPathHash, 0, 1);
                    AnimationImage.SetBool("EndVideo",true);
                }
            }
        }


    }
}
