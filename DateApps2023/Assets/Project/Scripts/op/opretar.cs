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
    //�{�X�̍U���̃`���[�W
    public void boss_attck_charge()
    {
        animator.SetTrigger("charge");
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

    //�G�l���M�[�����o����
    public void Approach()
    {
        animator.SetTrigger("Approach");
    }

    //�G�l���M�[�����o����
    public void energy_charge()
    {
        animator.SetTrigger("energycharge");
    }

}
