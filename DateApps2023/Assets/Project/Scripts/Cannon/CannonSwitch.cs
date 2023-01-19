using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSwitch : MonoBehaviour
{
    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private CannonShot cannonShot = null;

    [SerializeField]
    private StandManager standManager = null;
    
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
        if (!IsShot())
        {
            SwitchOn();
        }
        else
        {
            SwitchOff();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            return;
        }

        if (IsShot())
        {
            cannonShot.Shot();
            SwitchOn();
        }
    }

    private bool IsShot()
    {
        if (cannonShot.IsShotting)
        {
            return false;
        }

        if (!energyCharge.IsEnergyCharge())
        {
            return false;
        }

        if (!standManager.IsConectingStand())
        {
            return false;
        }

        return true;
    }

    private void SwitchOn()
    {
        if (!boxCollider.enabled)
        {
            return;
        }
        boxCollider.enabled = false;
        button.transform.localScale = switchOnScale;
    }

    private void SwitchOff()
    {
        if (boxCollider.enabled)
        {
            return;
        }
        boxCollider.enabled = true;
        button.transform.localScale = defaultScale;
    }
}
