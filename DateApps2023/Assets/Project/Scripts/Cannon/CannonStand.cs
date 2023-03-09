using UnityEngine;

/// <summary>
/// ‘å–C‚Ì”­Ë‘äƒNƒ‰ƒX
/// </summary>
public class CannonStand : MonoBehaviour
{
    [SerializeField]
    private STAND_POSITION standPosition = STAND_POSITION.NONE;

    /// <summary>
    /// ”­Ë‘ä‚ÌêŠ‚ğ•Ô‚·
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
