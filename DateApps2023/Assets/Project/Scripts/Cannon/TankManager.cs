using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private TankCharge[] energyTanks = new TankCharge[3];

    private int oldEnergy = 0;

    // Update is called once per frame
    void Update()
    {
        if (energyCharge.Energy > oldEnergy)
        {
            energyTanks[oldEnergy].Charge();
            oldEnergy = energyCharge.Energy;
        }
        else if(energyCharge.Energy < oldEnergy)
        {
            energyTanks[energyCharge.Energy].DisCharge();
            oldEnergy = energyCharge.Energy;
        }
    }
}
