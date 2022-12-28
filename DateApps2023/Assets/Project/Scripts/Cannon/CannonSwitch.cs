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
    private GameObject button = null;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetChild(0).gameObject;
        defaultScale = button.GetComponent<Transform>().localScale;
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
        Vector3 scale_ = defaultScale;
        scale_.z = 0.0f;
        button.transform.localScale = scale_;
    }

    private void SwitchOff()
    {
        Vector3 scale_ = defaultScale;
        button.transform.localScale = scale_;
    }
}
