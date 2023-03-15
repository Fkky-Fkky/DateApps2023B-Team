using UnityEngine;

/// <summary>
/// BGM�Đ��Ɋւ���N���X
/// </summary>
public class AudioMaster : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] firstBGM = null;

    [SerializeField]
    private AudioClip[] secondBGM = null;

    [SerializeField]
    private int changeKillCount = 5;

    [SerializeField]
    private float fadeOutTime = 5.0f;

    private AudioSource audioSource = null;

    private int number = 0;
    private float fadeTime = 0.0f;
    private float defaultVol = 1.0f;

    private bool isFirstHalf = true;
    private bool isFadeOut = true;
    private bool isEnd = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        number = Random.Range(0, firstBGM.Length);
        fadeTime = 0.0f;
        defaultVol = audioSource.volume;

        isFirstHalf = true;
        isFadeOut = true;
        isEnd = false;

        audioSource.clip = firstBGM[number];
        audioSource.Play();
    }

    private void Update()
    {
        if (isFirstHalf)
        {
            OnFirstHalf();
        }

        if (isEnd)
        {
            FadeOutBGM();
        }
    }

    /// <summary>
    /// �O����BGM�Ɋւ��鏈�����s��
    /// </summary>
    void OnFirstHalf()
    {
        if (BossCount.GetKillCount() < changeKillCount)
        {
            return;
        }

        if (isFadeOut)
        {
            FadeOutBGM();
        }
        else
        {
            PlaySecondBGM();
            isFirstHalf = false;
        }
    }

    /// <summary>
    /// �㔼���BGM���Đ�����
    /// </summary>
    public void PlaySecondBGM()
    {
        number= Random.Range(0, secondBGM.Length);
        audioSource.clip = secondBGM[number];
        audioSource.volume = defaultVol;
        audioSource.Play();
    }

    /// <summary>
    /// BGM�̉��ʂ����X�ɏ���������
    /// </summary>
    void FadeOutBGM()
    {
        fadeTime += Time.deltaTime;
        if (fadeTime >= fadeOutTime)
        {
            fadeTime = fadeOutTime;
            isFadeOut = false;
        }
        audioSource.volume = (float)(1.0 - fadeTime / fadeOutTime);
        if(audioSource.volume < 0)
        {
            audioSource.volume = 0;
        }
    }

    /// <summary>
    /// �Q�[���N���A�̍ۂɌĂяo��
    /// </summary>
    public void OnEndScene()
    {
        isEnd = true;
    }
}
