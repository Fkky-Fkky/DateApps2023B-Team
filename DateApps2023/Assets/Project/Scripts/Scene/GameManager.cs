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

    public bool IsGameOver { get { return bossManager.IsGameOver(); } }

    // Update is called once per frame
    void Update()
    {
        if(BossCount.GetKillCount() >= MoveKillCount)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
