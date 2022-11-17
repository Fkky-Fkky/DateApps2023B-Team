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
            Debug.Log("you push button");

            if (isCarry)
            {
                Debug.Log("you release item");

                HanteiEnter();
            }
            else
            {
                if (canUsed)
                {
                    Debug.Log("you get item");
                    isCarry = true;
                    canUsed = false;
                    //rb = null;
                    hanteiItem = carryItem.GetComponent<hantei>();
                    hanteiItem.GetGrabPoint(this.gameObject);
                    myGroupNo = hanteiItem.groupNumber;
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
            )
            {
                canUsed = true;
                carryItem = collision.gameObject;
                Debug.Log("OnTriggerStay");
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("OnTriggerExit");
        if (!isCarry)
        {
            if (collision.gameObject.CompareTag("item")
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4")
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
