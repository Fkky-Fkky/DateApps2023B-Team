using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCount : MonoBehaviour
{
    public int bossKillCount = 0;
    public static int killCount;
    private opretar opretar;

    private int messageCount = 0;

    private void Awake()
    {
        opretar = GameObject.Find("opretar").GetComponent<opretar>();
    }

    void Start()
    {
        killCount = bossKillCount;
        messageCount = 0;
    }


    public void SetBossKillCount()
    {
        if (messageCount < 1)
        {
            opretar.bosskill();
            messageCount++;
        }
        killCount++;
    }

    public static int GetKillCount()
    {
        return killCount;
    }
}
