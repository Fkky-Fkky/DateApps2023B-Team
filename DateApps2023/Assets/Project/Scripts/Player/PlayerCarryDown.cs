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

    private bool canUsed = false;

    private int myPlayerNo = 5;

    private PlayerMove playermove = null;
    private CarryEnergy energyItem;
    private CarryCannon cannonItem;

    private int myGroupNo = 1;

    private bool carryDamage = false;

    #endregion

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        playermove = GetComponentInParent<PlayerMove>();

        myCol = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if (!carryDamage)
        {
            if (Gamepad.all[myPlayerNo].bButton.wasPressedThisFrame)
            {
                if(!isCarry)
                {
                    if (canUsed)
                    {
                        isCarry = true;
                        canUsed = false;
                        if (carryItem.CompareTag("item"))
                        {
                            energyItem = carryItem.GetComponent<CarryEnergy>();
                            energyItem.GetGrabPoint(this.gameObject);
                            myGroupNo = energyItem.groupNumber;
                        }
                        if (carryItem.CompareTag("Cannon"))
                        {
                            cannonItem = carryItem.GetComponent<CarryCannon>();
                            cannonItem.GetGrabPoint(this.gameObject);
                            myGroupNo = cannonItem.groupNumber;
                        }
                        playermove.GetItem(myGroupNo);
                    }
                }
            }
            if (Gamepad.all[myPlayerNo].bButton.wasReleasedThisFrame)
            {
                if (isCarry)
                {
                    HanteiEnter();
                }
            }

        }

        if (isCarry)
        {
            myCol.enabled = false;
        }
        if (carryItem == null)
        {
            isCarry = false;
            canUsed = false;
            myCol.enabled = true;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (!isCarry)
        {
            if (collision.gameObject.CompareTag("item")
                || collision.gameObject.CompareTag("Cannon"))
            {
                canUsed = true;
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
                canUsed = false;
                carryItem = null;
            }
        }
    }

    public void HanteiEnter()
    {
        playermove.RemoveItem(myGroupNo);
        rb = GetComponentInParent<Rigidbody>();

        isCarry = false;
        canUsed = false;
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
        carryDamage = true;
    }

    public void OffCarryDamage()
    {
        carryDamage = false;
    }

}
