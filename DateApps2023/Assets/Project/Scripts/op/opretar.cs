using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opretar : MonoBehaviour
{
    Animator animator;

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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    public void boss()
    {
        animator.SetTrigger("boss");
    }
}
