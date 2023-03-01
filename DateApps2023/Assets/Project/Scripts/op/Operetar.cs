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
        #region �I�y�q�`���[�g���A��
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
    //���b����
    void First()
    {
        csv.FirstBossGanaretar();
    }
    //2��ڂ̉��b����
    void scond()
    {
        csv.SecondBossGanaretar();
    }
    //�`���[�g���A���I���̃t���O
    void tutorial_end()
    {
        energy.TutorialEnd();
        animator.SetTrigger("tutorial_end");
        gameState = GAME_STATE.game;
        StartFlag = true;
    }

    //�`���[�g���A��
    public bool Getstartflag()
    {
        return StartFlag;
    }

    //2��ڂ̃G�l���M�[��������
    void Second_energy_generate()
    {
         energy.SecondGenerate();
    }

    #endregion

    #region �Q�[�����̃I�y�q�̃Z���t

    void game()
    {
        if (OpFlag)
        {
            //���^�{�X
            if (boss.BossType() == 1&& OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                summonboss();
            }
            //���^�{�X
            if (boss.BossType() == 2&& OneFlag == false)
            {
               OpFlag = false; 
               OneFlag = true;
               summonminiboss();
            }
            //��^�{�X
            if (boss.BossType() == 3 && OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                summonbigboss();
            }
            //�{�X�̍U���`���[�W
            if (boss.Charge() && OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                boss_attck_charge();
            }
            //�{�X�ڋߎ�
            if (boss.Danger() && OneFlag == false)
            {
                OpFlag = false;
                OneFlag = true;
                Approach();
            }
            //�{�X����
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

    //�ʏ�{�X�o����
    public void summonboss()
    { 
        animator.SetTrigger("boss");  
    }

    //�~�j�{�X�o����
    public void summonminiboss()
    {
        animator.SetTrigger("miniboss");
    }

    //�r�b�O�{�X�o����
    public void summonbigboss()
    {
        animator.SetTrigger("bigboss");  
    }

    //�{�X���j��
    public void bosskill()
    {
        animator.SetTrigger("kill");  
    }
    //�{�X�ڋߎ�
    public void Approach()
    {
        animator.SetTrigger("Approach");
    }

    //�{�X�̍U���̃`���[�W
    public void boss_attck_charge()
    {
        animator.SetTrigger("charge");
    }

    //�G�l���M�[�����o����
    public void energy_charge()
    {
        animator.SetTrigger("energycharge");
    }
}
#endregion