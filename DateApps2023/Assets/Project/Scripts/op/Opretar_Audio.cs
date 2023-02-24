using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opretar_Audio : MonoBehaviour
{

    protected AudioSource Source;

    [SerializeField] AudioClip[] Tutorial_Voice;

    [SerializeField] AudioClip[] Game_voice;


    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void voice_stop()
    {
        Source.Stop();
    }

    #region É{ÉCÉXçƒê∂
    void Op_vice1()
    {
        Source.PlayOneShot(Tutorial_Voice[0]);
    }

    void Op_vice2()
    {
        Source.PlayOneShot(Tutorial_Voice[1]);
    }

    void Op_vice3()
    {
        Source.PlayOneShot(Tutorial_Voice[2]);
    }

    void Op_vice4()
    {
        Source.PlayOneShot(Tutorial_Voice[3]);
    }
    void Op_vice5()
    {
        Source.PlayOneShot(Tutorial_Voice[4]);
    }
    void Op_vice6()
    {
        Source.PlayOneShot(Tutorial_Voice[5]);
    }
    void Op_vice7()
    {
        Source.PlayOneShot(Tutorial_Voice[6]);
    }
    void Op_vice8()
    {
        Source.PlayOneShot(Tutorial_Voice[7]);
    }
    void Op_vice9()
    {
        Source.PlayOneShot(Tutorial_Voice[8]);
    }
    void Op_vice10()
    {
        Source.PlayOneShot(Tutorial_Voice[9]);
    }

    void Op_vice11()
    {
        Source.PlayOneShot(Tutorial_Voice[10]);
    }
    void Op_vice12()
    {
        Source.PlayOneShot(Tutorial_Voice[11]);
    }

    void Game_vice1()
    {
        Source.PlayOneShot(Game_voice[0]);
    }
    void Game_vice2()
    {
        Source.PlayOneShot(Game_voice[1]);
    }
    void Game_vice3()
    {
        Source.PlayOneShot(Game_voice[2]);
    }
    void Game_vice4()
    {
        Source.PlayOneShot(Game_voice[3]);
    }

    void Game_vice5()
    {
        Source.PlayOneShot(Game_voice[4]);
    }
    void Game_vice6()
    {
        Source.PlayOneShot(Game_voice[5]);
    }
#endregion
}
