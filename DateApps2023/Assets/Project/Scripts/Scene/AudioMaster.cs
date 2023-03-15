using UnityEngine;

/// <summary>
/// BGM再生に関するクラス
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
    /// 前半のBGMに関する処理を行う
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
    /// 後半戦のBGMを再生する
    /// </summary>
    public void PlaySecondBGM()
    {
        number= Random.Range(0, secondBGM.Length);
        audioSource.clip = secondBGM[number];
        audioSource.volume = defaultVol;
        audioSource.Play();
    }

    /// <summary>
    /// BGMの音量を徐々に小さくする
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
    /// ゲームクリアの際に呼び出す
    /// </summary>
    public void OnEndScene()
    {
        isEnd = true;
    }
}
