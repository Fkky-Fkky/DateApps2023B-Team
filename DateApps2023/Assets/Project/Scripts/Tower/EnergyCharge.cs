using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharge : MonoBehaviour
{
    private GameObject destroyItem = null;
    private BoxCollider boxCol = null;
    const int MAX_ENERGY = 3;
    
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
        destroyItem = other.gameObject;
        destroyItem.GetComponent<hantei>().DestroyMe();
        ChargeEnergy();
    }

    private void ChargeEnergy()
    {
        Energy = Mathf.Min(Energy + 1, MAX_ENERGY);
        if (Energy > MAX_ENERGY)
        {
            boxCol.enabled = false;
        }
    }

    public void DisChargeEnergy()
    {
        Energy = Mathf.Max(Energy - 1, 0);
    }

    public bool IsEnergyCharge()
    {
        return Energy > 0;
    }
}
