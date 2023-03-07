using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Op_text : MonoBehaviour
{
    private Image textimage;

    public Sprite[] bosstext;

    void Start()
    {
        textimage = GetComponent<Image>();
    }

    public void Approach()
    {
        textimage.sprite = bosstext[0];
    }
    public void Boss_kill_text()
    {
        textimage.sprite = bosstext[1];
    }

    public void Boss_text()
    {
        textimage.sprite = bosstext[2];
    }
    public void Mini_boss_text()
    {
        textimage.sprite = bosstext[3];
    }
    public void Bog_boss_text()
    {
        textimage.sprite = bosstext[4];
    }
    
    public void Boss_attcK_text()
    {
        textimage.sprite = bosstext[5];
    }
}
