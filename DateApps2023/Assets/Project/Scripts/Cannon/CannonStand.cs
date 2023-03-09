using UnityEngine;

/// <summary>
/// 大砲の発射台クラス
/// </summary>
public class CannonStand : MonoBehaviour
{
    [SerializeField]
    private STAND_POSITION standPosition = STAND_POSITION.NONE;

    /// <summary>
    /// 発射台の場所を返す
    /// </summary>
    public int ConnectingPos
    {
        get { return (int)standPosition; }
    }

    public enum STAND_POSITION
    {
        NONE = -1,
        LEFT,
        CENTRE,
        RIGHT
    }
}
