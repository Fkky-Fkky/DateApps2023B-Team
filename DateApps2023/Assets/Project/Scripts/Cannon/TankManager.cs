using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    [SerializeField]
    private EnergyCharge energyCharge = null;

    private int oldEnergy = 0;
    private TankCharge energyTank = null;

    private void Start()
    {
        energyTank = GetComponentInChildren<TankCharge>();
    }

    // Update is called once per frame
    void Update()
    {
        if (energyCharge.Energy > oldEnergy)
        {
            energyTank.Charge(energyCharge.ChrgeEnergyType);
            oldEnergy = energyCharge.Energy;
        }
        else if(energyCharge.Energy < oldEnergy)
        {
            energyTank.DisCharge();
            oldEnergy = energyCharge.Energy;
        }
    }
}
