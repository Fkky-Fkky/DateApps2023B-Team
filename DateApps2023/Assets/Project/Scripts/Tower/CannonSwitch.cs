using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSwitch : MonoBehaviour
{
    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private CannonShot cannonShot = null;

    private BoxCollider boxCollider = null;

    private bool isShot = false;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        isShot = !cannonShot.IsShotting && energyCharge.IsEnergyCharge();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isShot)
            {
                cannonShot.Shot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Punch"))
        {
            return;
        }        

        if (isShot)
        {
            cannonShot.Shot();
        }
    }
}
