using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarryDown : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isCarry = false;
    private Collision carryItem = null;

    private bool canUsed = false;


    void Start()
    {

    }
    void Update()
    {
        if (Gamepad.current.bButton.wasPressedThisFrame ||
            Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (isCarry)
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    GameObject child = transform.GetChild(i).gameObject;
                    child.transform.parent = null;
                }
                canUsed = false;
                isCarry = false;
            }
            else
            {
                if (canUsed)
                {
                    carryItem.transform.SetParent(transform);
                    isCarry = true;
                    canUsed = false;
                }
            }

        }
    }

    void OnCollisionEnter(Collision collision)
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
                carryItem = collision;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isCarry)
        {
            if (collision.gameObject.CompareTag("item")
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4")
            )
            {
                canUsed = false;
                //collision.gameObject.transform.parent = null;
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hantei"))
        {
            isCarry = false;
            canUsed = false;
        }
    }
}
