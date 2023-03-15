using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルシーンにおけるシーン移動の処理を行うクラス
/// それに伴うマニュアル画面(準備画面)の処理を含む
/// </summary>
public class TitleSceneMove : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "New Scene";

    [SerializeField]
    private Animator animationImage = null;

    [SerializeField]
    private CanvasGroup[] playerBackImage = null;

    [SerializeField]
    private CanvasGroup[] playerImage = null;

    [SerializeField]
    private AudioClip pressButtonSound = null;

    [SerializeField]
    private AudioClip[] acceptSound = null;

    [SerializeField]
    private AudioClip[] cancelSound = null;

    [SerializeField]
    private bool hasCanSkip = true;

    private AudioSource audioSource;
    private int acceptCount = 0;

    private bool isPlay = false;
    private bool isSkip = false;
    private bool[] isAccept = null;

    private const float ANIM_END_TIME = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        acceptCount = 0;

        isPlay = false;
        isSkip = false;
        Array.Resize(ref isAccept, playerImage.Length);
        for(int i = 0; i < playerImage.Length; i++)
        {
            isAccept[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            return;
        }
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

    /// <summary>
    /// タイトルロゴ画面でボタンを押したかどうかを判定する
    /// </summary>
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

    /// <summary>
    /// タイトルロゴ画面のフェードアウトをスキップするかどうかを判定する
    /// </summary>
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
        if (stateInfo.normalizedTime >= ANIM_END_TIME)
        {
            animationImage.SetTrigger("EndChangeScreen");
        }
    }

    /// <summary>
    /// マニュアル画面のフェードインをスキップするかどうかを判定する
    /// </summary>
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
        if (stateInfo.normalizedTime >= ANIM_END_TIME)
        {
            animationImage.SetTrigger("EndChangeScreen");
        }

    }

    /// <summary>
    /// プレイヤー全員が準備完了したかどうかを判定する
    /// </summary>
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

    /// <summary>
    /// マニュアル画面でプレイヤーがボタンを押した際の処理を行う
    /// </summary>
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

    /// <summary>
    /// メイン画面への遷移を行う
    /// </summary>
    private void InHideManual()
    {
        AnimatorStateInfo stateInfo = animationImage.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= ANIM_END_TIME)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// デモ動画を再生し始めた際に呼び出す
    /// </summary>
    public void OnTrueIsPlay()
    {
        isPlay = true;
    }

    /// <summary>
    /// デモ動画が再生し終わった際に呼び出す
    /// </summary>
    public void OnFalseIsPlay()
    {
        isPlay = false;
    }
}
