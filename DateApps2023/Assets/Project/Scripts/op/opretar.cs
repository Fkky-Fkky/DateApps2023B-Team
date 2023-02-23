using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] BossManager boss;

    bool op_flag = false;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState==gamestate.tutorial)
        {
            time+= Time.deltaTime;

            if(cannon.IsFirstCharge())
            {
                animator.SetTrigger("Button_ON");
            }

            if (time >= 45)
            {
                animator.SetTrigger("monster_light");
            }

            if (time >= 50)
            {
                animator.SetTrigger("monter_left");
            }
        }
        


        if (gameState == gamestate.game)
        {
            game();
        }
      
    }

    void tutorial_energy()
    {
        energy.FirstGenerate();
    }

    #region �Q�[�����̃I�y�q�̃Z���t
  
    void game()
    {
        if (op_flag)
        {
            //���^�{�X
            if (boss.BossType() == 1)
            {
                op_flag = false;
                summonboss();
            }
            //���^�{�X
            if (boss.BossType() == 2)
            {
                op_flag = false;
                summonminiboss();

            }
            //��^�{�X
            if (boss.BossType() == 3)
            {
                op_flag = false;
                summonbigboss();
            }
            //�{�X�̍U���`���[�W
            if (boss.Charge())
            {
                op_flag = false;
                boss_attck_charge();
            }
            //�{�X�ڋߎ�
            if (boss.Danger())
            {
                op_flag = false;
                Approach();
            }
            //�{�X����
            if (boss.IsBossKill())
            {
                op_flag = false;
                bosskill();
            }
        }

        if (!op_flag)
        {
            time += Time.deltaTime;

            if (time >= 2)
            {
                op_flag = true;
            }
        }
    }

    //�ʏ�{�X�o����
    public void summonboss()
    {
        //if (flag == false)
        //{
        //    flag = true;
        //}
        animator.SetTrigger("boss");
        op_text.Boss_text();
        //animator.ResetTrigger("boss");
    }

    //�~�j�{�X�o����
    public void summonminiboss()
    {
        animator.SetTrigger("miniboss");
        op_text.Mini_boss_text();
        //animator.ResetTrigger("miniboss");
    }

    //�r�b�O�{�X�o����
    public void summonbigboss()
    {
        animator.SetTrigger("bigboss");
        op_text.Bog_boss_text();
       // animator.ResetTrigger("bigboss");
    }

    //�{�X���j��
    public void bosskill()
    {
        animator.SetTrigger("kill");
        op_text.Boss_kill_text();
        //animator.ResetTrigger("kill");
    }
    //�{�X�ڋߎ�
    public void Approach()
    {
        animator.SetTrigger("Approach");
        //animator.ResetTrigger("Approach");
        op_text.Approach();
    }

    //�{�X�̍U���̃`���[�W
    public void boss_attck_charge()
    {
        animator.SetTrigger("charge");
        //animator.ResetTrigger("charge");
        op_text.Boss_attcK_text();
    }

    //�G�l���M�[�����o����
    public void energy_charge()
    {
        animator.SetTrigger("energycharge");
    }



    //��������g���ĂȂ����ǈꉞ�c���Ă��鏈��

    //�{�X�̍U���`���[�W�L�����Z��
    public void boss_attck_charge_stop()
    {
        animator.SetTrigger("charge stop");
    }
    //�{�X�̍U���L�����Z�������ɍU�������������ꍇ
    public void boss_charge_stop_miss()
    {
        animator.SetTrigger("charge stop miss");
    }
}
#endregion