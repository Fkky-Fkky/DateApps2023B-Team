using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarryDown : MonoBehaviour
{
    #region
    private bool isCarry = false;
    private GameObject carryItem = null;

    private Rigidbody rb;
    private BoxCollider myCol;

    private bool isCanUsed = false;

    private int myPlayerNo = 5;

    private PlayerMove playermove = null;
    private CarryEnergy energyItem;
    private CarryCannon cannonItem;

    private int myGroupNo = 1;

    private bool isCarryDamage = false;

    #endregion

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        playermove = GetComponentInParent<PlayerMove>();

        myCol = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (!isCarryDamage)
        {
            if (Gamepad.all[myPlayerNo].bButton.wasPressedThisFrame)
            {
                OnPressCarryButton();
            }
            if (Gamepad.all[myPlayerNo].bButton.wasReleasedThisFrame)
            {
                OnReleaseCarryButton();
            }

        }

        if (isCarry)
        {
            myCol.enabled = false;
        }
        if (carryItem == null)
        {
            isCarry = false;
            isCanUsed = false;
            myCol.enabled = true;
        }
    }

    void CheckItemTag()
    {
        if (carryItem.CompareTag("item"))
        {
            energyItem = carryItem.GetComponent<CarryEnergy>();
            energyItem.GetGrabPoint(this.gameObject);
            myGroupNo = energyItem.groupNumber;
            isCarry = true;
            isCanUsed = false;
            playermove.GetItem(myGroupNo);
        }
        if (carryItem.CompareTag("Cannon"))
        {
            if (!carryItem.GetComponent<CannonShot>().IsShotting)
            {
                cannonItem = carryItem.GetComponent<CarryCannon>();
                cannonItem.GetGrabPoint(this.gameObject);
                myGroupNo = cannonItem.groupNumber;
                isCarry = true;
                isCanUsed = false;
                playermove.GetItem(myGroupNo);
            }
        }
    }

    void OnPressCarryButton()
    {
        if (!isCarry)
        {
            if (isCanUsed)
            {
                CheckItemTag();
            }
        }
    }

    void OnReleaseCarryButton()
    {
        if (isCarry)
        {
            CarryCancel();
        }
    }


    void OnTriggerStay(Collider collision)
    {
        if (!isCarry)
        {
            if (collision.gameObject.CompareTag("item")
                || collision.gameObject.CompareTag("Cannon"))
            {
                isCanUsed = true;
                carryItem = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!isCarry)
        {
            if (collision.gameObject.CompareTag("item")
                || collision.gameObject.CompareTag("Cannon"))
            {
                isCanUsed = false;
                carryItem = null;
            }
        }
    }

    void CarryStart()
    {
        if (!isCarry && isCanUsed)
        {
            if (carryItem.CompareTag("item"))
            {
                CarryEnergyTag();
            }
            if (carryItem.CompareTag("Cannon"))
            {
                CarryCannonTag();
            }
        }
    }

    void CarryEnergyTag()
    {
        energyItem = carryItem.GetComponent<CarryEnergy>();
        energyItem.GetGrabPoint(this.gameObject);
        myGroupNo = energyItem.groupNumber;
        isCarry = true;
        isCanUsed = false;
        playermove.GetItem(myGroupNo);
    }

    void CarryCannonTag()
    {
        if (!carryItem.GetComponent<CannonShot>().IsShotting)
        {
            cannonItem = carryItem.GetComponent<CarryCannon>();
            cannonItem.GetGrabPoint(this.gameObject);
            myGroupNo = cannonItem.groupNumber;
            isCarry = true;
            isCanUsed = false;
            playermove.GetItem(myGroupNo);
        }
    }

    public void CarryCancel()
    {
        playermove.RemoveItem();
        rb = GetComponentInParent<Rigidbody>();

        isCarry = false;
        isCanUsed = false;
        carryItem = null;
        energyItem = null;
        cannonItem = null;
        myCol.enabled = true;
    }

    public void GetPlayerNo(int parentNumber)
    {
        myPlayerNo = parentNumber;
    }

    public void OnCarryDamage()
    {
        isCarryDamage = true;
    }

    public void OffCarryDamage()
    {
        isCarryDamage = false;
    }

}
