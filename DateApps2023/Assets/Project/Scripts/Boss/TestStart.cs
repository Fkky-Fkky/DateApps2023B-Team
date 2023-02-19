using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class TestStart : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "New Scene";

    private bool SceneChangeFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneChangeFlag)
        {

            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneManager.LoadScene(sceneName);
                SceneChangeFlag = true;
            }
        }

    }
}
