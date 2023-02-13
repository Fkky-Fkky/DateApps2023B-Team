using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharge : MonoBehaviour
{
    [SerializeField]
    private GameObject[] energyChargeEffect = new GameObject[3];

    [SerializeField]
    private AudioClip chargeSe = null;

    private BoxCollider boxCol = null;
    private AudioSource audioSource = null;
    private const int MAX_ENERGY = 1;
    private const int ADD_ENERGY = 1;
    
    public int Energy { get; private set; }
    public int ChrgeEnergyType { get; private set; }

    public enum EnergyType
    {
        SMALL,
        MEDIUM,
        LARGE,
    }

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

        int itemSize = other.GetComponent<CarryEnergy>().MyItemSizeCount;
        switch (itemSize)
        {
            case (int)CarryEnergy.ItemSize.Small:
                ChrgeEnergyType = (int)EnergyType.SMALL;
                break;

            case (int)CarryEnergy.ItemSize.Medium:
                ChrgeEnergyType = (int)EnergyType.MEDIUM;
                break;

            case (int)CarryEnergy.ItemSize.XL:
                ChrgeEnergyType = (int)EnergyType.LARGE;
                break;
        }
        other.GetComponent<CarryEnergy>().DestroyMe();
        ChargeEnergy();
    }

    private void ChargeEnergy()
    {
        Energy = Mathf.Min(Energy + ADD_ENERGY, MAX_ENERGY);
        Instantiate(energyChargeEffect[ChrgeEnergyType], transform.position, Quaternion.identity);
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
