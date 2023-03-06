using UnityEngine;

public class CannonStand : MonoBehaviour
{
    [SerializeField]
    private STAND_POSITION standPosition = STAND_POSITION.NONE;

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
