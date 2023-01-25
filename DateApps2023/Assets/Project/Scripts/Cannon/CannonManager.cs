using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField]
    private CannonConnect cannonConnect = null;

    [SerializeField]
    private CannonShot cannonShot = null;

    public bool IsShooting
    {
        get { return cannonShot.IsShotting; }
    }

    public int DoConnectingPos
    {
        get { return cannonConnect.ConnectingPos; }
    }
}
