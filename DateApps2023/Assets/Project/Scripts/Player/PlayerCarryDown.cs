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
    private SabotageItem sabotageItem;

    private int myGroupNo = 1;

    #endregion

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        playermove = GetComponentInParent<PlayerMove>();

        myCol = GetComponent<BoxCollider>();
    }
    void Update()
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
                    if (carryItem.CompareTag("CloneSabotageItem"))
                    {
                        sabotageItem = carryItem.GetComponent<SabotageItem>();
                        sabotageItem.GetGrabPoint(this.gameObject);
                        myGroupNo = sabotageItem.groupNumber;
                    }
                    else if(carryItem.CompareTag("item")
                         || carryItem.CompareTag("item2")
                         || carryItem.CompareTag("item3")
                         || carryItem.CompareTag("item4"))
                    {
                        hanteiItem = carryItem.GetComponent<hantei>();
                        hanteiItem.GetGrabPoint(this.gameObject);
                        myGroupNo = hanteiItem.groupNumber;
                    }
                    playermove.GetItem(myGroupNo);
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
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4")
            || collision.gameObject.CompareTag("CloneSabotageItem")
            )
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
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4")
            || collision.gameObject.CompareTag("CloneSabotageItem")
            )
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
