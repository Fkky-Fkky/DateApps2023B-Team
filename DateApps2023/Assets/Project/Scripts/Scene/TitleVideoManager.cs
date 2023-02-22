using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TitleVideoManager : MonoBehaviour
{
    [SerializeField]
    private Animator AnimationImage = null;

    [SerializeField]
    private float NonPlayTime = 11.0f;
    private float time = 0.0f;

    private SceneMove sceneMove;
    private VideoPlayer videoPlayer;

    [SerializeField]
    private GameObject videoImage;

    [SerializeField]
    private Vector2 videoSize = new Vector2(1920.0f, 1080.0f);

    [SerializeField]
    private int screenDepth = 24;

    private bool isPlaying = false;
    private bool isFinished = false;
    private RenderTexture renderTexture = null;
    private RawImage playVideoScreen;


    // Start is called before the first frame update
    void Start()
    {
        sceneMove = GetComponent<SceneMove>();
        videoPlayer = GetComponent<VideoPlayer>();
        sceneMove.OnTrueIsPlay();
        videoPlayer.Stop();
        playVideoScreen = videoImage.GetComponent<RawImage>();
        SetRenderTexture();

    }

    // Update is called once per frame
    void Update()
    {

        if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            AnimatorStateInfo stateInfo = AnimationImage.GetCurrentAnimatorStateInfo(0);
            if (isFinished)
            {
                AnimationImage.Play(stateInfo.fullPathHash, 0, 0);
                OnEndVideo();
                SetRenderTexture();
                isFinished = false;
            }
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
                isPlaying = false;
                AnimationImage.SetTrigger("StartVideo");
                time = 0.0f;
            }
            else
            {
                sceneMove.OnFalseIsPlay();
            }
        }
        else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("StartVideo"))
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad.aButton.wasPressedThisFrame)
                {
                    AnimatorStateInfo stateInfo = AnimationImage.GetCurrentAnimatorStateInfo(0);
                    AnimationImage.Play(stateInfo.fullPathHash, 0, 1);
                    AnimationImage.SetTrigger("EndVideo");
                }
            }
        }
        else if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("PlayVideo"))
        {
            if (isFinished)
            {
                AnimationImage.SetTrigger("EndVideo");

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
                if (gamepad.aButton.wasPressedThisFrame)
                {
                    if (isPlaying)
                    {
                        OnEndVideo();
                    }
                    isFinished = true;
                }
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
