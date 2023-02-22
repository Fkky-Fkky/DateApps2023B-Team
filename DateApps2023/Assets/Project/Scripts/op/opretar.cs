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

        if (timer >= 3 && flag == 0)
        {
            Approach();
            flag = 1;
        }
    }

    //�ʏ�{�X�o����
    public void summonboss()
    {
        animator.SetTrigger("boss");
        op_text.Boss_text();
        animator.ResetTrigger("boss");
    }

    //�~�j�{�X�o����
    public void summonminiboss()
    {
        animator.SetTrigger("miniboss");
        op_text.Mini_boss_text();
        animator.ResetTrigger("miniboss");
    }

    //�r�b�O�{�X�o����
    public void summonbigboss()
    {
        animator.SetTrigger("bigboss");
        op_text.Bog_boss_text();
        animator.ResetTrigger("bigboss");
    }

    //�{�X���j��
    public void bosskill()
    {
        animator.SetTrigger("kill");
        op_text.Boss_kill_text();
        animator.ResetTrigger("kill");
    }
    //�{�X�̍U���̃`���[�W
    public void boss_attck_charge()
    {
        animator.SetTrigger("charge");
        op_text.Boss_attcK_text();
        animator.ResetTrigger("charge");
    }
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

    //�{�X�ڋߎ�
    public void Approach()
    {
        animator.SetTrigger("Approach");
        op_text.Approach();
        animator.ResetTrigger("Approach");
    }

    //�G�l���M�[�����o����
    public void energy_charge()
    {
        animator.SetTrigger("energycharge");
    }

}
