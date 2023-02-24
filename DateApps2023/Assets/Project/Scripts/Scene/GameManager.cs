using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int MoveKillCount = 10;

    [SerializeField]
    private BossManager bossManager = null;

    private float sceneMoveTime = 0.0f;
    public bool IsGameOver { get { return bossManager.IsGameOver(); } }

    const float SCENE_MOVE_TIME = 5.0f;

    private void Start()
    {
        sceneMoveTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(BossCount.GetKillCount() >= MoveKillCount)
        {
            SceneManager.LoadScene("ClearScene");
        }

        if (IsGameOver)
        {
            sceneMoveTime += Time.deltaTime;
        }

        if(sceneMoveTime > SCENE_MOVE_TIME)
        {
            SceneManager.LoadScene("GameoverScene");
        }
    }
}
