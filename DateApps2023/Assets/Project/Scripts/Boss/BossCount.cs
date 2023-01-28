using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCount : MonoBehaviour
{
    public int bossKillCount;
    public static int killCount;

    // Start is called before the first frame update
    void Start()
    {
        bossKillCount = 0;
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
