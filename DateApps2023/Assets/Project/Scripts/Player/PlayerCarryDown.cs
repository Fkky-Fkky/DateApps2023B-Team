using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarryDown : MonoBehaviour
{
	// Start is called before the first frame update
    private bool isCarry = false;
    private Collision carryItem = null;

	void Start()
	{
		
	}
	void Update()
	{
        if (Gamepad.current.bButton.wasPressedThisFrame||
            Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (isCarry)
            {
                isCarry = false;
                carryItem.transform.parent = null;
            }
            
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (isCarry)
        {
            return;
        }
        if (collision.gameObject.CompareTag("item")
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4")
            )
        {
            carryItem = collision;
            carryItem.transform.SetParent(transform);
            isCarry = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("item")
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4")
            )
        {

            collision.gameObject.transform.parent = null;
        }
    }
 }
