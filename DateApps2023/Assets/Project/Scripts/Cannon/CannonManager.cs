using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField]
    private CannonConnect[] cannonConnect = new CannonConnect[2];

    [SerializeField]
    private CannonShot[] cannonShot = new CannonShot[2];

    [SerializeField]
    private EnergyCharge[] energyCharge = new EnergyCharge[2];

    private List<int>  isShotEnergyTypeList = new List<int>();
    private List<int>  connectingPosList    = new List<int>();
    private List<bool> isShootingList       = new List<bool>();

    const int CANNON_MAX = 1;

    public int CanonMax { get { return CANNON_MAX; } }

    private void Start()
    {
        for (int i = 0; i < CANNON_MAX; i++)
        {
            isShootingList.Add(cannonShot[i].IsNowShot);
            isShotEnergyTypeList.Add(energyCharge[i].ChrgeEnergyType);
            connectingPosList.Add(cannonConnect[i].ConnectingPos);
        }
    }

    public List<bool> IsShooting
    {
        get
        {
            for (int i = 0; i < CANNON_MAX; i++)
            {
                isShootingList[i] = cannonShot[i].IsNowShot;
            }
            return isShootingList;
        }
    }

    public List<int> IsShotEnergyType
    {
        get
        {
            for (int i = 0; i < CANNON_MAX; i++)
            {
                isShotEnergyTypeList[i] = energyCharge[i].ChrgeEnergyType;
            }
            return isShotEnergyTypeList;
        }
    }

    public List<int> DoConnectingPos
    {
        get
        {
            for (int i = 0; i < CANNON_MAX; i++)
            {
                connectingPosList[i] = cannonConnect[i].ConnectingPos;
            }
            return connectingPosList;
        }
    }

    public bool IsFirstCharge()
    {
        return energyCharge[0].IsEnergyCharged();
    }
}
