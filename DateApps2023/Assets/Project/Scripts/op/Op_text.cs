using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class Op_text : MonoBehaviour
{
    private Image op_text_image;

    public Sprite[] op_boss_text;

    public Sprite[] tutorial_text;

    //bool text_flag = false;

    



    void Start()
    {
        op_text_image = GetComponent<Image>();

        //for (int i = 0;i>=10;)
        //{
        //    boss_attck_text[i] = GetComponent<Sprite>();
        //}
    }

    void Update()
    {

    }

    public void Approach()
    {
       // boss_attck_text[0] = Resources.Load<Sprite>("op_text/OP_ptx_16");
        op_text_image.sprite = op_boss_text[0];
    }
    public void Boss_kill_text()
    {
        //boss_attck_text[1] = Resources.Load<Sprite>("op_text/OP_ptx_17");
        op_text_image.sprite = op_boss_text[1];
    }

    public void Boss_text()
    {
        op_text_image.sprite = op_boss_text[2];
    }
    public void Mini_boss_text()
    {
        //boss_attck_text[3] = Resources.Load<Sprite>("op_text/OP_ptx_19");
        op_text_image.sprite = op_boss_text[3];
    }
    public void Bog_boss_text()
    {
        //boss_attck_text[4] = Resources.Load<Sprite>("op_text/OP_ptx_20");
        op_text_image.sprite = op_boss_text[4];
    }
    
    public void Boss_attcK_text()
    {
        //boss_attck_text[5] = Resources.Load<Sprite>("op_text/OP_ptx_21");
        op_text_image.sprite = op_boss_text[5];
    }
}
