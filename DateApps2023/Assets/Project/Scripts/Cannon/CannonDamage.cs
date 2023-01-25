using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDamage : MonoBehaviour
{
    [SerializeField]
    private EnergyCharge energyCharge = null;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("BossAttack"))
        {
            return;
        }
        energyCharge.DisChargeEnergy();
    }
}
