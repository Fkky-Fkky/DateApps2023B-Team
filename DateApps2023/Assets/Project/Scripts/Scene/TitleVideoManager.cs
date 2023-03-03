using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class TitleVideoManager : MonoBehaviour
{
    #region
    [SerializeField]
    private Animator animationImage = null;

    [SerializeField]
    private RawImage playVideoScreen;

    [SerializeField]
    private Vector2 videoSize = new Vector2(1920.0f, 1080.0f);

    [SerializeField]
    private int screenDepth = 24;

    [SerializeField]
    private float standingTime = 11.0f;

    [SerializeField]
    private bool hasLogoSkip = true;

    private TitleSceneMove titleSceneMove;
    private VideoPlayer videoPlayer;
    private RenderTexture renderTexture = null;

    private float time = 0.0f;

    private bool isPlaying = false;
    private bool isFinished = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        titleSceneMove = GetComponent<TitleSceneMove>();
        titleSceneMove.OnTrueIsPlay();

        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Stop();

        time = 0.0f;
        isPlaying = false; 
        isFinished = false;
       
        SetRenderTexture();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            InStart();
        }
        else if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            InIdle();
        }
        else if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("StartVideo"))
        {
            InStartVideo();
        }
        else if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("PlayVideo"))
        {
            InPlayVideo();
        }
    }

    private void InStart()
    {
        AnimatorStateInfo stateInfo = animationImage.GetCurrentAnimatorStateInfo(0);
        if (isFinished)
        {
            animationImage.Play(stateInfo.fullPathHash, 0, 0);
            OnEndVideo();
            SetRenderTexture();
            isFinished = false;
        }
        if (stateInfo.normalizedTime >= 1.0f)
        {
            animationImage.SetTrigger("EndLogoAnim");
        }
        if (hasLogoSkip)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad.bButton.wasPressedThisFrame)
                {
                    animationImage.Play(stateInfo.fullPathHash, 0, 1);
                }
            }
        }
    }

    private void InIdle()
    {
        time += Time.deltaTime;
        if (time >= standingTime)
        {
            titleSceneMove.OnTrueIsPlay();
            isPlaying = false;
            animationImage.SetTrigger("StartVideo");
            time = 0.0f;
        }
        else
        {
            titleSceneMove.OnFalseIsPlay();
        }
    }

    private void InStartVideo()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var gamepad = Gamepad.all[i];
            if (gamepad.bButton.wasPressedThisFrame)
            {
                AnimatorStateInfo stateInfo = animationImage.GetCurrentAnimatorStateInfo(0);
                animationImage.Play(stateInfo.fullPathHash, 0, 1);
                animationImage.SetTrigger("EndVideo");
            }
        }
    }

    private void InPlayVideo()
    {
        if (isFinished)
        {
            animationImage.SetTrigger("EndVideo");

        }
        if (!isPlaying)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += FinishPlayingVideo;

            isPlaying = true;
        }

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var gamepad = Gamepad.all[i];
            if (gamepad.bButton.wasPressedThisFrame)
            {
                if (isPlaying)
                {
                    OnEndVideo();
                }
                isFinished = true;
            }
        }
    }

    public void FinishPlayingVideo(VideoPlayer vp)
    {
        OnEndVideo();
        isFinished = true;
    }

    private void SetRenderTexture()
    {
        renderTexture = new RenderTexture((int)videoSize.x, (int)videoSize.y, screenDepth);
        videoPlayer.targetTexture = renderTexture;
        playVideoScreen.texture = renderTexture;
    }

    private void OnEndVideo()
    {
        videoPlayer.Stop();
        videoPlayer.targetTexture = null;
        playVideoScreen.texture = null;
        renderTexture = null;
    }
}
