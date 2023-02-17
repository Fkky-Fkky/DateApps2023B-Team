using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("à⁄ìÆÇÃë¨Ç≥")]
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

    private TextMeshPro carryText = null;
    private Outline outline = null;

    private float defaultMass;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        rb.useGravity = false;
        defaultMass = rb.mass;

        Array.Resize(ref ChildPlayer, 4);
        Array.Resize(ref AnimationImage, ChildPlayer.Length);
    }

    private void FixedUpdate()
    {
        if (controlFrag)
        {
            Vector2[] before = { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) };

            if(playerCount >= itemSizeCount + 1)
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
                    transform.GetChild(0).gameObject.GetComponent<CarryEnergy>().OutGroup();
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

    public void GetItemSize(int itemSize, int itemType, GameObject gameObject)
    {
        itemSizeCount = itemSize;
        carryText = gameObject.GetComponentInChildren<TextMeshPro>();
        outline = gameObject.GetComponentInChildren<Outline>();
        outline.enabled = false;
        if (itemType == 1) //ñCë‰ÇÃÉpÅ[Éc
        {
            HaveItem = true;
        }
        else if(itemType == 2)
        {
            HaveItem = true;
            rb.mass *= 10;
        }
        CheckPlayerCount();

    }

    public void ReleaseChild()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag("item"))
            {
                transform.GetChild(i).gameObject.GetComponent<CarryEnergy>().DoHanteiEnter();
            }
            if (transform.GetChild(i).gameObject.CompareTag("Cannon"))
            {
                transform.GetChild(i).gameObject.GetComponent<CarryCannon>().OutGroup();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.CompareTag("Player"))
                {
                    transform.GetChild(i).gameObject.GetComponent<PlayerDamage>().JudgeCapture(other.gameObject);
                }
            }
        }
        if (other.gameObject.CompareTag("BossAttack"))
        {
            DamageChild();
        }
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
                transform.GetChild(i).gameObject.GetComponent<CarryEnergy>().OutGroup();
            }
            if (transform.GetChild(i).gameObject.CompareTag("Cannon"))
            {
                transform.GetChild(i).gameObject.GetComponent<CarryCannon>().OutGroup();
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
        outline.enabled = true;
        outline = null;
        carryText.text = null;
        carryText = null;
        rb.mass = defaultMass;
        groupVec = Vector3.zero;
        rb.velocity = groupVec;
    }

    void CheckPlayerCount()
    {
        if(playerCount < 0)
        {
            playerCount = 0;
        }
        else if (playerCount > 4)
        {
            playerCount = 4;
        }

        if(carryText != null)
        {
            carryText.text = playerCount.ToString("0") + "/" + (itemSizeCount + 1).ToString("0");
        }

        if (itemSizeCount == 0)
        {
            if(playerCount == 1)
            {
                mySpeed = (moveSpeed * 1.0f) / playerCount;
            }
            else if (playerCount == 2)
            {
                mySpeed = (moveSpeed * 1.3f) / playerCount;
            }
            else if (playerCount == 3)
            {
                mySpeed = (moveSpeed * 1.7f) / playerCount;
            }
            else if (playerCount == 4)
            {
                mySpeed = (moveSpeed * 2.3f) / playerCount;
            }
        }
        else if (itemSizeCount == 1)
        {
            if (playerCount == 1)
            {
                mySpeed = (moveSpeed * 0.5f) / playerCount;
            }
            else if (playerCount == 2)
            {
                mySpeed = (moveSpeed * 1.0f) / playerCount;
            }
            else if (playerCount == 3)
            {
                mySpeed = (moveSpeed * 1.5f) / playerCount;
            }
            else if (playerCount == 4)
            {
                mySpeed = (moveSpeed * 2.0f) / playerCount;
            }
        }
        else if (itemSizeCount == 2)
        {
            if (playerCount == 1)
            {
                mySpeed = (moveSpeed * 0.3f) / playerCount;
            }
            else if (playerCount == 2)
            {
                mySpeed = (moveSpeed * 0.5f) / playerCount;
            }
            else if (playerCount == 3)
            {
                mySpeed = (moveSpeed * 1.0f) / playerCount;
            }
            else if (playerCount == 4)
            {
                mySpeed = (moveSpeed * 2.5f) / playerCount;
            }
        }
        else if (itemSizeCount == 3)
        {
            if (playerCount == 1)
            {
                mySpeed = (moveSpeed * 0.2f) / playerCount;
            }
            else if (playerCount == 2)
            {
                mySpeed = (moveSpeed * 0.4f) / playerCount;
            }
            else if (playerCount == 3)
            {
                mySpeed = (moveSpeed * 0.8f) / playerCount;
            }
            else if (playerCount == 4)
            {
                mySpeed = (moveSpeed * 1.6f) / playerCount;
            }
        }
    }
}
