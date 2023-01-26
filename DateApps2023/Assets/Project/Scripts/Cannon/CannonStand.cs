using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStand : MonoBehaviour
{
    [SerializeField]
    private STAND_POSITION standPosition = STAND_POSITION.NONE;

    public int ConnectingPos
    {
        get { return (int)standPosition; }
    }

    enum STAND_POSITION
    {
        LEFT,
        CENTRE,
        RIGHT,
        NONE
    }
}
