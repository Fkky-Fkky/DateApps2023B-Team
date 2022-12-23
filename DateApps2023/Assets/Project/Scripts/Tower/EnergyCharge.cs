using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharge : MonoBehaviour
{
    private GameObject destroyItem = null;
    private CreateRandomPosition createRandomPosition = null;
    private BoxCollider boxCol = null;
    const int MAX_ENERGY = 3;
    
    public int Energy { get; private set; }

    private void Start()
    {
        createRandomPosition = GetComponentInParent<CreateRandomPosition>();
        boxCol = GetComponent<BoxCollider>();
        Energy = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            destroyItem = other.gameObject;
            destroyItem.GetComponent<hantei>().DestroyMe();
            ChargeEnergy();
            createRandomPosition.Settower_bild_flag();
        }
    }

    private void ChargeEnergy()
    {
        if(Energy > MAX_ENERGY)
        {
            boxCol.enabled = false;
        }
        Energy++;
    }
}
