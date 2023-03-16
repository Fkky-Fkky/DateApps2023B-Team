//担当者:武田碧
using UnityEngine;

/// <summary>
/// ボスが倒れたかカウントする
/// </summary>
public class BossCount : MonoBehaviour
{
    public int BossKillCount = 0;
    public static int KillCount;

    void Start()
    {
        KillCount = BossKillCount;
    }

    /// <summary>
    /// ボスが倒れたらカウントを増やす
    /// </summary>
    public void SetBossKillCount()
    {
        KillCount++;
    }
    /// <summary>
    /// ボスが倒れたカウントを返す
    /// </summary>
    /// <returns>ボスを倒したカウントの値</returns>
    public static int GetKillCount()
    {
        return KillCount;
    }
}
