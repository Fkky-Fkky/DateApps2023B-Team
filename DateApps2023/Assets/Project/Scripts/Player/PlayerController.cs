using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Metadata;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ˆÚ“®‚Ì‘¬‚³")]
    private float moveSpeed = 1000.0f;

    private Rigidbody rb;

    private bool controlFrag = false;
    private bool[] gamepadFrag = { false, false, false, false };

    private int itemSizeCount = 0;
    private int playerCount = 0;
    private float mySpeed = 1.0f;
    public float sentSpeed = 1.0f;

    private Vector3 groupVec = new Vector3(0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
    }

    private void FixedUpdate()
    {
        if (controlFrag)
        {
            Vector2[] before = { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) };

            CheckPlayerCount();
            sentSpeed = (mySpeed * moveSpeed) / 2;
            
            for(int i = 0; i < gamepadFrag.Length; i++)
            {
                if (gamepadFrag[i])
                {
                    var leftStickValue = Gamepad.all[i].leftStick.ReadValue();

                    if (leftStickValue.x != 0.0f)
                    {
                        before[i].x = mySpeed * Time.deltaTime * leftStickValue.x;
                    }
                    if (leftStickValue.y != 0.0f)
                    {
                        before[i].y = mySpeed * Time.deltaTime * leftStickValue.y;
                    }
                }
            }

            groupVec.x = before[0].x + before[1].x + before[2].x + before[3].x;
            groupVec.z = before[0].y + before[1].y + before[2].y + before[3].y;
            rb.velocity = groupVec;

            if (transform.childCount == 1)
            {
                transform.GetChild(0).gameObject.GetComponent<hantei>().OutGroup();
            }
            else if(transform.childCount <= 1)
            {
                AllFragFalse();
            }
        }
    }

    public void GetMyNo(int childNo)
    {
        gamepadFrag[childNo] = true;
        playerCount++;
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
        playerCount--;
    }

    void AllFragFalse()
    {
        for(int i = 0; i < gamepadFrag.Length; i++)
        {
            gamepadFrag[i] = false;
        }
        controlFrag = false;
        playerCount = 0;
    }

    public void GetItemSize(int itemSize)
    {
        itemSizeCount = itemSize;
    }

    void CheckPlayerCount()
    {
        if(playerCount < 0)
        {
            playerCount = 0;
        }
        if (playerCount > 4)
        {
            playerCount = 4;
        }

        if (itemSizeCount == 0)
        {
            if(playerCount == 1)
            {
                mySpeed = (moveSpeed * 1.0f) / playerCount;
            }
            if (playerCount == 2)
            {
                mySpeed = (moveSpeed * 1.3f) / playerCount;
            }
            if (playerCount == 3)
            {
                mySpeed = (moveSpeed * 1.7f) / playerCount;
            }
            if (playerCount == 4)
            {
                mySpeed = (moveSpeed * 2.3f) / playerCount;
            }
        }
        if (itemSizeCount == 1)
        {
            if (playerCount == 1)
            {
                mySpeed = (moveSpeed * 0.5f) / playerCount;
            }
            if (playerCount == 2)
            {
                mySpeed = (moveSpeed * 1.0f) / playerCount;
            }
            if (playerCount == 3)
            {
                mySpeed = (moveSpeed * 1.5f) / playerCount;
            }
            if (playerCount == 4)
            {
                mySpeed = (moveSpeed * 2.0f) / playerCount;
            }
        }
        if (itemSizeCount == 2)
        {
            //mySpeed = 0.25f;
            if (playerCount == 1)
            {
                mySpeed = (moveSpeed * 0.3f) / playerCount;
            }
            if (playerCount == 2)
            {
                mySpeed = (moveSpeed * 0.5f) / playerCount;
            }
            if (playerCount == 3)
            {
                mySpeed = (moveSpeed * 1.0f) / playerCount;
            }
            if (playerCount == 4)
            {
                mySpeed = (moveSpeed * 2.5f) / playerCount;
            }
        }
    }
}
