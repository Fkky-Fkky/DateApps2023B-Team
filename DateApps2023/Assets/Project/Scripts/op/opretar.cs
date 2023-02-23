using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class opretar : MonoBehaviour
{
    Animator animator;

    [SerializeField] private Op_text op_text;

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
        if(op_flag)
        {
            //中型ボス
            if (boss.BossType()== 1 )
            {
                summonboss();
                op_flag = false;
            }
            //小型ボス
            if (boss.BossType() == 2 )
            {
                summonminiboss();
                op_flag = false;
            }
            //大型ボス
            if (boss.BossType() == 3 )
            {
                summonbigboss();
                op_flag = false;
            }
            //ボスの攻撃チャージ
            if (boss.Charge())
            {
                boss_attck_charge();
                op_flag = false;
            }
            //ボス接近時
            if (boss.Danger())
            {
                Approach();
                op_flag = false;
            }
            //ボス討伐
            if (boss.IsBossKill())
            {
                bosskill();
                op_flag = false;
            }
        }

        if(!op_flag)
        {
            time += Time.deltaTime;

            if(time>=2)
            {
                op_flag= true;
            }
        }
    }

    //通常ボス出現時
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

    //ミニボス出現時
    public void summonminiboss()
    {
        animator.SetTrigger("miniboss");
        op_text.Mini_boss_text();
        //animator.ResetTrigger("miniboss");
    }

    //ビッグボス出現時
    public void summonbigboss()
    {
        animator.SetTrigger("bigboss");
        op_text.Bog_boss_text();
       // animator.ResetTrigger("bigboss");
    }

    //ボス撃破時
    public void bosskill()
    {
        animator.SetTrigger("kill");
        op_text.Boss_kill_text();
        //animator.ResetTrigger("kill");
    }
    //ボス接近時
    public void Approach()
    {
        animator.SetTrigger("Approach");
        //animator.ResetTrigger("Approach");
        op_text.Approach();
    }

    //ボスの攻撃のチャージ
    public void boss_attck_charge()
    {
        animator.SetTrigger("charge");
        //animator.ResetTrigger("charge");
        op_text.Boss_attcK_text();
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
}
