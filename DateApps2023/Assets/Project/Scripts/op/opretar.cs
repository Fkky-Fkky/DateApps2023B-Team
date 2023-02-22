using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opretar : MonoBehaviour
{
    Animator animator;

    [SerializeField]private Text text_;

    enum summon
    {
        stert,

        idle,

        boss_attck,

        boss_attck_stop,

        warning,

        end,
    }
    summon gameState = summon.stert;

    float timer = 0;

    int flag =0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //if (timer >= 10 && flag == 0)
        //{
        //    Approach();
        //    flag = 1;
        //}
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
    //ボスの攻撃のチャージ
    public void boss_attck_charge()
    {
        animator.SetTrigger("charge");
    }
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

    //エネルギー物資出現時
    public void Approach()
    {
        animator.SetTrigger("Approach");
    }

    //エネルギー物資出現時
    public void energy_charge()
    {
        animator.SetTrigger("energycharge");
    }

}
