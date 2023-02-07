using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField]
    private CannonConnect cannonConnect = null;

    [SerializeField]
    private CannonShot cannonShot = null;

    [SerializeField]
    private EnergyCharge energyCharge = null;

    public bool IsShooting
    {
        get { return cannonShot.IsNowShot; }
    }

    public int IsShotEnergyType
    {
        get { return energyCharge.ChrgeEnergyType; }
    }

    public int DoConnectingPos
    {
        get { return cannonConnect.ConnectingPos; }
    }
}
