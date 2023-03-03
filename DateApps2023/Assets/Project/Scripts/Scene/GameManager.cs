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

    [SerializeField]
    private float AfterTime = 2.0f;
    private float time = 0.0f;
    private bool IsFade = false;
    [SerializeField]
    private Animator FadeOutAnimator = null;

    [SerializeField]
    opretar myOperator = null;

    private float sceneMoveTime = 0.0f;
    public bool IsGameOver { get { return bossManager.IsGameOver(); } }
    public bool IsGammeStart { get { return myOperator.Getstartflag(); } }

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
            if (!IsFade)
            {
                FadeOutAnimator.SetTrigger("FadeOut");
                IsFade = true;
            }
            else
            {
                time += Time.deltaTime;
                if (time >= AfterTime)
                {
                    SceneManager.LoadScene("ClearScene");
                }
            }
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
