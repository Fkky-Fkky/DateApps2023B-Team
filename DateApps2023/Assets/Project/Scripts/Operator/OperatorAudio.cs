//担当者:丸子羚
using UnityEngine;

/// <summary>
/// オペレーターのボイスのクラス
/// </summary>
public class OperatorAudio : MonoBehaviour
{
    [SerializeField] 
    AudioClip[] tutorialVoice = null;

    [SerializeField]
    AudioClip[] gameVoice = null;

    private AudioSource source = null;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponents<AudioSource>()[0];
    }
    
    /// <summary>
    ///オペレーターのボイスの再生を止める関数 
    /// </summary>
    void VoiceStop()
    {
        source.Stop();
    }

    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice1()
    {
        source.PlayOneShot(tutorialVoice[0]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice2()
    {
        source.PlayOneShot(tutorialVoice[1]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice3()
    {
        source.PlayOneShot(tutorialVoice[2]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice4()
    {
        source.PlayOneShot(tutorialVoice[3]);
    }

    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice5()
    {
        source.PlayOneShot(tutorialVoice[4]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice6()
    {
        source.PlayOneShot(tutorialVoice[5]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice7()
    {
        source.PlayOneShot(tutorialVoice[6]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice8()
    {
        source.PlayOneShot(tutorialVoice[7]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice9()
    {
        source.PlayOneShot(tutorialVoice[8]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice10()
    {
        source.PlayOneShot(tutorialVoice[9]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice11()
    {
        source.PlayOneShot(tutorialVoice[10]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice12()
    {
        source.PlayOneShot(tutorialVoice[11]);
    } /// <summary>
      /// オペレーターのボイスのアニメーター呼び出しのトリガー
      /// </summary>
    void OpVice13()
    {
        source.PlayOneShot(tutorialVoice[12]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void OpVice14()
    {
        source.PlayOneShot(tutorialVoice[13]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void GameVice1()
    {
        source.PlayOneShot(gameVoice[0]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void GameVice2()
    {
        source.PlayOneShot(gameVoice[1]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void GameVice3()
    {
        source.PlayOneShot(gameVoice[2]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void GameVice4()
    {
        source.PlayOneShot(gameVoice[3]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void GameVice5()
    {
        source.PlayOneShot(gameVoice[4]);
    }
    /// <summary>
    /// オペレーターのボイスのアニメーター呼び出しのトリガー
    /// </summary>
    void GameVice6()
    {
        source.PlayOneShot(gameVoice[5]);
    }
}
