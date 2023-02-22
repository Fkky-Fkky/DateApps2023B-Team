using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCount : MonoBehaviour
{
    public int bossKillCount = 0;
    public static int killCount;

    void Start()
    {
        killCount = bossKillCount;
    }


    public void SetBossKillCount()
    {
        killCount++;
    }

    public static int GetKillCount()
    {
        return killCount;
    }
}
