using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g���V�[���ɂ�����V�[���ړ��̏������s���N���X
/// ����ɔ����}�j���A�����(�������)�̏������܂�
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
    /// �^�C�g�����S��ʂŃ{�^�������������ǂ����𔻒肷��
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
    /// �^�C�g�����S��ʂ̃t�F�[�h�A�E�g���X�L�b�v���邩�ǂ����𔻒肷��
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
    /// �}�j���A����ʂ̃t�F�[�h�C�����X�L�b�v���邩�ǂ����𔻒肷��
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
    /// �v���C���[�S�������������������ǂ����𔻒肷��
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
    /// �}�j���A����ʂŃv���C���[���{�^�����������ۂ̏������s��
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
    /// ���C����ʂւ̑J�ڂ��s��
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
    /// �f��������Đ����n�߂��ۂɌĂяo��
    /// </summary>
    public void OnTrueIsPlay()
    {
        isPlay = true;
    }

    /// <summary>
    /// �f�����悪�Đ����I������ۂɌĂяo��
    /// </summary>
    public void OnFalseIsPlay()
    {
        isPlay = false;
    }
}
