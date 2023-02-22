using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class Text : MonoBehaviour
{
    private Image op_text_image;
    private Sprite[] op_text;
    private Sprite stert_text;

    private Text PlayerDamage;

    // Start is called before the first frame update
    void Start()
    {
        op_text_image = GetComponent<Image>();
        op_text[0] = Resources.Load<Sprite>("k_text");
        op_text[1] = Resources.Load<Sprite>("k_text");
        op_text[2] = Resources.Load<Sprite>("k_text");
        op_text[3] = Resources.Load<Sprite>("k_text");
        op_text[4] = Resources.Load<Sprite>("k_text");
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void boss_text()
    {
        op_text_image.sprite= op_text[0];
    }
    public void mini_boss_text()
    {
        op_text_image.sprite = op_text[1];
    }
    public void bog_boss_text()
    {
        op_text_image.sprite = op_text[2];
    }
    public void boss_kill_text()
    {
        op_text_image.sprite = op_text[3];
    }
    public void Approach()
    {
        op_text_image.sprite = op_text[4];
    }
    public void boss_attcK_text()
    {
        op_text_image.sprite = op_text[5];
    }
}
