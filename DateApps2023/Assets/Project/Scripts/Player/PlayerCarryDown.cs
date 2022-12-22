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
    private hantei hanteiItem;

    private int myGroupNo = 1;

    public bool carryDamage = false;

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
                if (isCarry)
                {
                    HanteiEnter();
                }
                else
                {
                    if (canUsed)
                    {
                        isCarry = true;
                        canUsed = false;
                        if (carryItem.CompareTag("item"))
                        {
                            hanteiItem = carryItem.GetComponent<hantei>();
                            hanteiItem.GetGrabPoint(this.gameObject);
                            myGroupNo = hanteiItem.groupNumber;
                        }
                        playermove.GetItem(myGroupNo);
                    }
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
            if (collision.gameObject.CompareTag("item"))
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
            if (collision.gameObject.CompareTag("item"))
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
        myCol.enabled = true;
    }

    public void GetPlayerNo(int parentNumber)
    {
        myPlayerNo = parentNumber;
    }

}
