using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class opretar : MonoBehaviour
{
    Animator animator;

    [SerializeField] private Op_text op_text;

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

        if (timer >= 8 && flag == 0)
        {
            Approach();
            op_text.Boss_text();
            flag = 1;
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
