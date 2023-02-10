using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCount : MonoBehaviour
{
    public int bossKillCount = 0;
    public static int killCount;

    public BossDamage bossDamage = null;


    void Start()
    {
        killCount = bossKillCount;
    }

    // Update is called once per frame

    public void SetBossKillCount()
    {
        killCount++;
    }

    public static int GetKillCount()
    {
        return killCount;
    }
}
