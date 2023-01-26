using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public BossDamage bossDamage;

    private void Start()
    {
        
    }

    void Update()
    {
        Debugging();
    }

    void Debugging()
    {
        //デバック用
        if (Input.GetKeyDown(KeyCode.K))//<-ifの中に大砲のフラグ
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Center");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Left");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Right");
            foreach (GameObject boss in objects)
            {
                boss.GetComponent<BossDamage>().KnockbackTrueSub();
            }
        }

    }
}
