using UnityEngine;

public class OpretarAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] tutorialVoice = null;

    [SerializeField] AudioClip[] gameVoice = null;

    private AudioSource source = null;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponents<AudioSource>()[0];
    }
    
    //ボイスの再生を止める関数
    void VoiceStop()
    {
        source.Stop();
    }

    #region ボイス再生
    void OpVice1()
    {
        source.PlayOneShot(tutorialVoice[0]);
    }

    void OpVice2()
    {
        source.PlayOneShot(tutorialVoice[1]);
    }

    void OpVice3()
    {
        source.PlayOneShot(tutorialVoice[2]);
    }

    void OpVice4()
    {
        source.PlayOneShot(tutorialVoice[3]);
    }
    void OpVice5()
    {
        source.PlayOneShot(tutorialVoice[4]);
    }
    void OpVice6()
    {
        source.PlayOneShot(tutorialVoice[5]);
    }
    void OpVice7()
    {
        source.PlayOneShot(tutorialVoice[6]);
    }
    void OpVice8()
    {
        source.PlayOneShot(tutorialVoice[7]);
    }
    void OpVice9()
    {
        source.PlayOneShot(tutorialVoice[8]);
    }
    void OpVice10()
    {
        source.PlayOneShot(tutorialVoice[9]);
    }

    void OpVice11()
    {
        source.PlayOneShot(tutorialVoice[10]);
    }
    void OpVice12()
    {
        source.PlayOneShot(tutorialVoice[11]);
    }

    void OpVice13()
    {
        source.PlayOneShot(tutorialVoice[12]);
    }

    void OpVice14()
    {
        source.PlayOneShot(tutorialVoice[13]);
    }

    void GameVice1()
    {
        source.PlayOneShot(gameVoice[0]);
    }
    void GameVice2()
    {
        source.PlayOneShot(gameVoice[1]);
    }
    void GameVice3()
    {
        source.PlayOneShot(gameVoice[2]);
    }
    void GameVice4()
    {
        source.PlayOneShot(gameVoice[3]);
    }

    void GameVice5()
    {
        source.PlayOneShot(gameVoice[4]);
    }
    void GameVice6()
    {
        source.PlayOneShot(gameVoice[5]);
    }
#endregion
}
