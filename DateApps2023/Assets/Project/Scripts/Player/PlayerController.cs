using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�ړ��̑���")]
    private float moveSpeed = 1000.0f;

    private Rigidbody rb;

    private bool controlFrag = false;
    private bool[] gamepadFrag = { false, false, false, false };

    private int itemSizeCount = 0;
    private int playerCount = 0;
    private float mySpeed = 1.0f;

    private Vector3 groupVec = new Vector3(0, 0, 0);

    public GameObject[] ChildPlayer = null;
    public Animator[] AnimationImage = null;

    private const string ps_WalkSpeed = "RunSpeed";
    [SerializeField]
    private float AnimationSpeed = 0.001f;

    public bool HaveItem = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        rb.useGravity = false;

        Array.Resize(ref ChildPlayer, 4);
        Array.Resize(ref AnimationImage, ChildPlayer.Length);
    }

    private void FixedUpdate()
    {
        if (controlFrag)
        {
            Vector2[] before = { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) };

            if(playerCount >= 2)
            {
                for (int i = 0; i < gamepadFrag.Length; i++)
                {
                    if (gamepadFrag[i])
                    {
                        var leftStickValue = Gamepad.all[i].leftStick.ReadValue();

                        if (leftStickValue.x != 0.0f)
                        {
                            AnimationImage[i].SetBool("CarryMove", true);
                            before[i].x = mySpeed * Time.deltaTime * leftStickValue.x;
                        }
                        if (leftStickValue.y != 0.0f)
                        {
                            AnimationImage[i].SetBool("CarryMove", true);
                            before[i].y = mySpeed * Time.deltaTime * leftStickValue.y;
                        }

                        if (leftStickValue.x == 0.0f && leftStickValue.y == 0.0f)
                        {
                            AnimationImage[i].SetBool("CarryMove", false);
                            before[i] = Vector2.zero;
                        }

                        float walkSpeed = mySpeed * AnimationSpeed;
                        AnimationImage[i].SetFloat(ps_WalkSpeed, walkSpeed);
                    }
                }

                groupVec.x = before[0].x + before[1].x + before[2].x + before[3].x;
                groupVec.z = before[0].y + before[1].y + before[2].y + before[3].y;
            }
            else
            {
                groupVec = Vector3.zero;
            }
            rb.velocity = groupVec;

            if (transform.childCount <= 1)
            {
                if (transform.GetChild(0).gameObject.CompareTag("item"))
                {
                    transform.GetChild(0).gameObject.GetComponent<hantei>().OutGroup();
                }
                else if (transform.GetChild(0).gameObject.CompareTag("Cannon"))
                {
                    transform.GetChild(0).gameObject.GetComponent<CarryCannon>().OutGroup();
                }
                AllFragFalse();
            }
        }
    }

    public void GetMyNo(int childNo,GameObject gameObject)
    {
        ChildPlayer[childNo] = gameObject;
        AnimationImage[childNo] = gameObject.GetComponent<Animator>();

        gamepadFrag[childNo] = true;
        playerCount++;
        controlFrag = true;
        CheckPlayerCount();

    }

    public void GetItemSize(int itemSize, int itemType)
    {
        itemSizeCount = itemSize;
        if (itemType == 1) //�C��̃p�[�c
        {
            HaveItem = true;
        }
        CheckPlayerCount();

    }

    public void ReleaseChild()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag("item"))
            {
                transform.GetChild(i).gameObject.GetComponent<hantei>().DoHanteiEnter();
            }
            transform.GetChild(i).gameObject.transform.parent = null;
        }

        for (int i = 0; i < ChildPlayer.Length; i++)
        {
            if (ChildPlayer[i] != null || AnimationImage[i] != null)
            {
                AnimationImage[i].SetBool("CarryMove", false);
                ChildPlayer[i] = null;
                AnimationImage[i] = null;
            }
        }
        AllFragFalse();
    }

    public void DamageChild()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag("Player"))
            {
                transform.GetChild(i).gameObject.GetComponent<PlayerDamage>().CallDamage();
            }
            if (transform.GetChild(i).gameObject.CompareTag("item"))
            {
                transform.GetChild(i).gameObject.GetComponent<hantei>().OutGroup();
            }
            //transform.GetChild(i).gameObject.transform.parent = null;
        }
        for (int i = 0; i < ChildPlayer.Length; i++)
        {
            if (ChildPlayer[i] != null || AnimationImage[i] != null)
            {
                AnimationImage[i].SetBool("CarryMove", false);
                ChildPlayer[i] = null;
                AnimationImage[i] = null;
            }
        }
        AllFragFalse();
    }

    public void PlayerOutGroup(int outChildNo)
    {
        ChildPlayer[outChildNo] = null;
        AnimationImage[outChildNo] = null;
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
        HaveItem = false;
        groupVec = Vector3.zero;
        rb.velocity = groupVec;
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
