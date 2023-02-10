using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "New Scene";
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Center"|| other.gameObject.tag == "Left" || other.gameObject.tag == "Right")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
