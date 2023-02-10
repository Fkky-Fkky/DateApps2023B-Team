using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharge : MonoBehaviour
{
    [SerializeField]
    private GameObject energyChargeEffect = null;

    [SerializeField]
    private AudioClip chargeSe = null;

    private BoxCollider boxCol = null;
    private AudioSource audioSource = null;
    private const int MAX_ENERGY = 1;
    private const int ADD_ENERGY = 1;
    
    public int Energy { get; private set; }
    public int ChrgeEnergyType { get; private set; }

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        audioSource = transform.parent.GetComponent<AudioSource>();
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
        
        if (other.GetComponent<CarryEnergy>().MyItemSizeCount == 1)
        {
            ChrgeEnergyType = 0;
        }
        else
        {
            ChrgeEnergyType = 1;
        }
        other.GetComponent<CarryEnergy>().DestroyMe();
        ChargeEnergy();
    }

    private void ChargeEnergy()
    {
        Energy = Mathf.Min(Energy + ADD_ENERGY, MAX_ENERGY);
        Instantiate(energyChargeEffect, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(chargeSe);
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

    public bool IsEnergyCharged()
    {
        return Energy > 0;
    }
}
