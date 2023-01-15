using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSwitch : MonoBehaviour
{
    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private CannonShot cannonShot = null;

    private bool isShot = false;
    private Vector3 defaultScale;
    private Vector3 switchOnScale;
    private GameObject button = null;
    private BoxCollider boxCollider = null;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetChild(0).gameObject;
        defaultScale = button.GetComponent<Transform>().localScale;
        switchOnScale = new Vector3(defaultScale.x, defaultScale.y, 0.0f);
        boxCollider = GetComponent<BoxCollider>();
        SwitchOn();
    }

    // Update is called once per frame
    void Update()
    {
        isShot = !cannonShot.IsShotting && energyCharge.IsEnergyCharge();
        if (!isShot)
        {
            return;
        }
        SwitchOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            return;
        }

        if (isShot)
        {
            cannonShot.Shot();
            SwitchOn();
        }
    }

    private void SwitchOn()
    {
        button.transform.localScale = switchOnScale;
        boxCollider.enabled = false;
    }

    private void SwitchOff()
    {
        button.transform.localScale = defaultScale;
        if (!boxCollider.enabled)
        {
            boxCollider.enabled = true;
        }
    }
}
