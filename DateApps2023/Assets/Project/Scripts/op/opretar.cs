using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class opretar : MonoBehaviour
{
    Animator animator;

    enum gamestate
    {
        tutorial,

        game,
    }
    gamestate gameState = gamestate.tutorial;

    [SerializeField] private Op_text op_text;

    [SerializeField] private EnergyGenerator energy;

    [SerializeField] private CannonManager cannon;

    [SerializeField] private BossCSVGenerator csv;

    //[SerializeField] private BossCount bosscount;

    [SerializeField] BossManager boss;

    private bool start_flag = false;

    bool op_flag = false;

    bool game_one_flag = false;

    float time = 0;

    //[SerializeField] float game_stert_time = 100;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region オペ子チュートリアル
        if (gameState == gamestate.tutorial)
        {
            time += Time.deltaTime;

            if (cannon.IsFirstCharge())
            {
                animator.SetTrigger("Button_ON");
            }

            if (BossCount.GetKillCount() == 1)
            {
                animator.SetTrigger("firing");
            }
            


            //if (time >= 45)
            //{
            //    animator.SetTrigger("monster_light");
            //}

            //if (time >= 50)
            //{
            //    animator.SetTrigger("monter_left");
            //}


            if (BossCount.GetKillCount() == 2)
            {
                animator.SetTrigger("boss_second_kill");
            }
        }
        
        if (gameState == gamestate.game)
        {
            game();
        }
        #endregion
    }

    void First()
    {
        csv.FirstBossGanaretar();
    }

    void scond()
    {
        csv.SecondBossGanaretar();
    }

    void tutorial_end()
    {
        //energy.TutorialEnd();
        animator.SetTrigger("tutorial_end");
        gameState = gamestate.game;
        start_flag = true;
    }

    public bool Getstartflag()
    {
        return start_flag;
    }

    void Second_energy_generate()
    {
         //energy.SecondGenerate();
    }

    #region ゲーム中のオペ子のセリフ

    void game()
    {
        if (op_flag)
        {
            //中型ボス
            if (boss.BossType() == 1)
            {
                op_flag = false;
                if (game_one_flag == false)
                {
                    game_one_flag = true;
                    summonboss();
                }
            }
            //小型ボス
            if (boss.BossType() == 2)
            {
                op_flag = false;
                if (game_one_flag == false)
                {
                    game_one_flag = true;
                    summonminiboss();
                }

            }
            //大型ボス
            if (boss.BossType() == 3)
            {
                op_flag = false;
                if (game_one_flag == false)
                {
                    game_one_flag = true;
                    summonbigboss();
                }
            }
            //ボスの攻撃チャージ
            if (boss.Charge())
            {
                op_flag = false;
                if (game_one_flag == false)
                {
                    game_one_flag = true;
                    boss_attck_charge();
                }
            }
            //ボス接近時
            if (boss.Danger())
            {
                op_flag = false;
                if (game_one_flag == false)
                {
                    game_one_flag = true;
                    Approach();
                }
            }
            //ボス討伐
            if (boss.IsBossKill())
            {
                op_flag = false;
                if (game_one_flag == false)
                {
                    game_one_flag = true;
                    bosskill();
                }
            }
        }

        if (!op_flag)
        {
            time += Time.deltaTime;

            if (time >= 2)
            {
                op_flag = true;
                game_one_flag = false;
            }
        }
    }

    //通常ボス出現時
    public void summonboss()
    { 
        animator.SetTrigger("boss");  
    }

    //ミニボス出現時
    public void summonminiboss()
    {
        animator.SetTrigger("miniboss");
    }

    //ビッグボス出現時
    public void summonbigboss()
    {
        animator.SetTrigger("bigboss");  
    }

    //ボス撃破時
    public void bosskill()
    {
        animator.SetTrigger("kill");  
    }
    //ボス接近時
    public void Approach()
    {
        animator.SetTrigger("Approach");
    }

    //ボスの攻撃のチャージ
    public void boss_attck_charge()
    {
        animator.SetTrigger("charge");
    }

    //エネルギー物資出現時
    public void energy_charge()
    {
        animator.SetTrigger("energycharge");
    }



    //ここから使ってないけど一応残している処理

    //ボスの攻撃チャージキャンセル
    public void boss_attck_charge_stop()
    {
        animator.SetTrigger("charge stop");
    }
    //ボスの攻撃キャンセルせずに攻撃が発動した場合
    public void boss_charge_stop_miss()
    {
        animator.SetTrigger("charge stop miss");
    }

    void tutorial_energy()
    {
        energy.Generate();
    }
}
#endregion