using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class Op_text : MonoBehaviour
{
    private Image op_text_image;

    Sprite op_text;
    //private Sprite stert_text;

    // Start is called before the first frame update
    void Start()
    {
        op_text_image = GetComponent<Image>();
        op_text = Resources.Load<Sprite>("op/k_text");
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Boss_text()
    {
        op_text_image.sprite= op_text;
    }
    //public void Mini_boss_text()
    //{
    //    op_text_image.sprite = op_text[1];
    //}
    //public void Bog_boss_text()
    //{
    //    op_text_image.sprite = op_text[2];
    //}
    //public void Boss_kill_text()
    //{
    //    op_text_image.sprite = op_text[3];
    //}
    //public void Approach()
    //{
    //    op_text_image.sprite = op_text[4];
    //}
    //public void Boss_attcK_text()
    //{
    //    op_text_image.sprite = op_text[5];
    //}
}
