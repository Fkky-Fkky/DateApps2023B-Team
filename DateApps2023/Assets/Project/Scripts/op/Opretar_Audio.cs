using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opretar_Audio : MonoBehaviour
{

    protected AudioSource Source;

    [SerializeField] AudioClip[] TutorialVoice;

    [SerializeField] AudioClip[] GameVoice;


    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponents<AudioSource>()[0];
    }
    
    //ボイスの再生を止める関数
    void voice_stop()
    {
        Source.Stop();
    }

    #region ボイス再生
    void Op_vice1()
    {
        Source.PlayOneShot(TutorialVoice[0]);
    }

    void Op_vice2()
    {
        Source.PlayOneShot(TutorialVoice[1]);
    }

    void Op_vice3()
    {
        Source.PlayOneShot(TutorialVoice[2]);
    }

    void Op_vice4()
    {
        Source.PlayOneShot(TutorialVoice[3]);
    }
    void Op_vice5()
    {
        Source.PlayOneShot(TutorialVoice[4]);
    }
    void Op_vice6()
    {
        Source.PlayOneShot(TutorialVoice[5]);
    }
    void Op_vice7()
    {
        Source.PlayOneShot(TutorialVoice[6]);
    }
    void Op_vice8()
    {
        Source.PlayOneShot(TutorialVoice[7]);
    }
    void Op_vice9()
    {
        Source.PlayOneShot(TutorialVoice[8]);
    }
    void Op_vice10()
    {
        Source.PlayOneShot(TutorialVoice[9]);
    }

    void Op_vice11()
    {
        Source.PlayOneShot(TutorialVoice[10]);
    }
    void Op_vice12()
    {
        Source.PlayOneShot(TutorialVoice[11]);
    }

    void Op_vice13()
    {
        Source.PlayOneShot(TutorialVoice[12]);
    }

    void Op_vice14()
    {
        Source.PlayOneShot(TutorialVoice[13]);
    }

    void Game_vice1()
    {
        Source.PlayOneShot(GameVoice[0]);
    }
    void Game_vice2()
    {
        Source.PlayOneShot(GameVoice[1]);
    }
    void Game_vice3()
    {
        Source.PlayOneShot(GameVoice[2]);
    }
    void Game_vice4()
    {
        Source.PlayOneShot(GameVoice[3]);
    }

    void Game_vice5()
    {
        Source.PlayOneShot(GameVoice[4]);
    }
    void Game_vice6()
    {
        Source.PlayOneShot(GameVoice[5]);
    }
#endregion
}
