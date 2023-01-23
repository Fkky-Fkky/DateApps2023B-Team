using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharge : MonoBehaviour
{
    private BoxCollider boxCol = null;
    const int MAX_ENERGY = 3;
    const int ADD_ENERGY = 1;
    
    public int Energy { get; private set; }

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        Energy = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("item"))
        {
            return;
        }

        if (other.transform.parent == null)
        {
            return;
        }
        other.GetComponent<hantei>().DestroyMe();
        ChargeEnergy();
    }

    private void ChargeEnergy()
    {
        Energy = Mathf.Min(Energy + ADD_ENERGY, MAX_ENERGY);
        if (Energy >= MAX_ENERGY)
        {
            boxCol.enabled = false;
        }
    }

    public void DisChargeEnergy()
    {
        Energy = Mathf.Max(Energy - ADD_ENERGY, 0);
        if (boxCol.enabled)
        {
            return;
        }
        boxCol.enabled = true;
    }

    public bool IsEnergyCharge()
    {
        return Energy > 0;
    }
}
