using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "New Scene";

    public void GameOverTransition()
    {
        SceneManager.LoadScene(sceneName);
    }

}
