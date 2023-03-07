using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCount : MonoBehaviour
{
    public int BossKillCount = 0;
    public static int KillCount;

    void Start()
    {
        KillCount = BossKillCount;
    }


    public void SetBossKillCount()
    {
        KillCount++;
    }

    public static int GetKillCount()
    {
        return KillCount;
    }
}
