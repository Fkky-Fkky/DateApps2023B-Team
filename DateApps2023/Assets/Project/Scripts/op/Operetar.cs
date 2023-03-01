using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Operetar : MonoBehaviour
{
    Animator animator;

    enum GAME_STATE
    {
        tutorial,

        game,
    }
    GAME_STATE gameState = GAME_STATE.tutorial;

    [SerializeField] private Op_text optext;

    [SerializeField] private EnergyGenerator energy;

    [SerializeField] private CannonManager cannon;

    [SerializeField] private BossCSVGenerator csv;

    [SerializeField] BossManager boss;

    private bool StartFlag = false;

    bool OpFlag = false;

    bool OneFlag = false;

    float Time = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region オペ子チュートリアル
        if (gameState == GAME_STATE.tutorial)
        {
            Time += UnityEngine.Time.deltaTime;

            if (cannon.IsFirstCharge())
            {
                animator.SetTrigger("Button_ON");
            }

            if (BossCount.GetKillCount() == 1)
            {
                animator.SetTrigger("firing");
            }
            
            if (BossCount.GetKillCount() == 2)
            {
                animator.SetTrigger("boss_second_kill");
            }
        }
        
        if (gameState == GAME_STATE.game)
        {
            game();
        }
    }
    //怪獣生成
    void First()
    {
        csv.FirstBossGanaretar();
    }
    //2回目の怪獣生成
    void scond()
    {
        csv.SecondBossGanaretar();
    }
    //チュートリアル終了のフラグ
    void tutorial_end()
    {
        energy.TutorialEnd();
        animator.SetTrigger("tutorial_end");
        gameState = GAME_STATE.game;
        StartFlag = true;
    }

    //チュートリアル
    public bool Getstartflag()
    {
        return StartFlag;
    }

    //2回目のエネルギー物資生成
    void Second_energy_generate()
    {
         energy.SecondGenerate();
    }

    #endregion

    #region ゲーム中のオペ子のセリフ

    void game()
    {
        if (OpFlag)
        {
            //中型ボス
            if (boss.BossType() == 1&& OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                summonboss();
            }
            //小型ボス
            if (boss.BossType() == 2&& OneFlag == false)
            {
               OpFlag = false; 
               OneFlag = true;
               summonminiboss();
            }
            //大型ボス
            if (boss.BossType() == 3 && OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                summonbigboss();
            }
            //ボスの攻撃チャージ
            if (boss.Charge() && OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                boss_attck_charge();
            }
            //ボス接近時
            if (boss.Danger() && OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                Approach();
            }
            //ボス討伐
            if (boss.IsBossKill() && OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                bosskill();
            }
        }

        if (!OpFlag)
        {
            Time += UnityEngine.Time.deltaTime;

            if (Time >= 2)
            {
                OpFlag = true;
                OneFlag = false;
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
}
#endregion