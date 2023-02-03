using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCount : MonoBehaviour
{
    public int bossKillCount;
    public static int killCount;

    public BossDamage bossDamage = null;

    void Start()
    {
        bossKillCount = 0;
    }

    private void Update()
    {
        if(bossDamage.isBossFellDown)
        {
            bossKillCount++;
            bossDamage.isBossFellDown = false;
        }
    }

    // Update is called once per frame

    public void SetBossKillCount()
    {
        killCount = bossKillCount;
    }

    public static int GetKillCount()
    {
        return killCount;
    }
}
