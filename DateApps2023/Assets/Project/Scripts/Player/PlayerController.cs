using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Metadata;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ˆÚ“®‚Ì‘¬‚³")]
    private float moveSpeed = 2000.0f;

    private Rigidbody rb;

    private bool controlFrag = false;
    private bool[] gamepadFrag = { false, false, false, false };


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (controlFrag)
        {
            Vector3 vec = new Vector3(0, 0, 0);
            Vector2 before1 = new Vector2(0, 0);
            Vector2 before2 = new Vector2(0, 0);
            Vector2 before3 = new Vector2(0, 0);
            Vector2 before4 = new Vector2(0, 0);

            if (gamepadFrag[0])
            {
                var leftStickValue = Gamepad.all[0].leftStick.ReadValue();

                if (leftStickValue.x != 0.0f)
                {
                    before1.x = moveSpeed * Time.deltaTime * leftStickValue.x;
                }
                if (leftStickValue.y != 0.0f)
                {
                    before1.y = moveSpeed * Time.deltaTime * leftStickValue.y;
                }
            }
            if (gamepadFrag[1])
            {
                var leftStickValue = Gamepad.all[1].leftStick.ReadValue();

                if (leftStickValue.x != 0.0f)
                {
                    before2.x = moveSpeed * Time.deltaTime * leftStickValue.x;
                }
                if (leftStickValue.y != 0.0f)
                {
                    before2.y = moveSpeed * Time.deltaTime * leftStickValue.y;
                }
            }
            if (gamepadFrag[2])
            {
                var leftStickValue = Gamepad.all[2].leftStick.ReadValue();

                if (leftStickValue.x != 0.0f)
                {
                    before3.x = moveSpeed * Time.deltaTime * leftStickValue.x;
                }
                if (leftStickValue.y != 0.0f)
                {
                    before3.y = moveSpeed * Time.deltaTime * leftStickValue.y;
                }
            }
            if (gamepadFrag[3])
            {
                var leftStickValue = Gamepad.all[3].leftStick.ReadValue();

                if (leftStickValue.x != 0.0f)
                {
                    before4.x = moveSpeed * Time.deltaTime * leftStickValue.x;
                }
                if (leftStickValue.y != 0.0f)
                {
                    before4.y = moveSpeed * Time.deltaTime * leftStickValue.y;
                }
            }

            vec.x = before1.x + before2.x + before3.x + before4.x;
            vec.z = before1.y + before2.y + before3.y + before4.y;
            rb.velocity = vec;

            if (transform.childCount == 1)
            {
                transform.GetChild(0).gameObject.GetComponent<hantei>().OutGroup();
                AllFragFalse();
            }
            else if(transform.childCount < 1)
            {
                AllFragFalse();
            }
        }
    }

    public void GetMyNo(int childNo)
    {
        gamepadFrag[childNo] = true;
        controlFrag = true;
        
    }

    public void ReleaseChild()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<hantei>().DoHanteiEnter();
            transform.GetChild(i).gameObject.transform.parent = null;
        }

        AllFragFalse();
    }

    public void OutGroup(int outChildNo)
    {
        gamepadFrag[outChildNo] = false;
    }

    void AllFragFalse()
    {
        for(int i = 0; i < gamepadFrag.Length; i++)
        {
            gamepadFrag[i] = false;
        }
        controlFrag = false;
    }
}
