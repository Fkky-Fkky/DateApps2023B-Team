using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    #region
    [SerializeField]
    [Tooltip("ˆÚ“®‚Ì‘¬‚³")]
    private float moveSpeed = 250.0f;

    [SerializeField]
    private float carryOverSpeed = 0.1f;

    [SerializeField]
    private float animationSpeed = 0.001f;

    [SerializeField]
    private int carryTextOrderInLayer = 0;

    [SerializeField]
    private float[] smallCarrySpeed = null;

    [SerializeField]
    private float[] midiumCarrySpeed = null;

    [SerializeField]
    private float[] largeCarrySpeed = null;


    private Rigidbody rb = null;
    private TextMeshPro carryText = null;
    private Outline outline = null;

    private int itemSizeCount = 0;
    private int playerCount = 0;
    private int needCarryCount = 0;

    private float mySpeed = 1.0f;
    private float defaultMass = 1.0f;
    private float defaultCarryOverSpeed = 0.0f;

    private bool isControlFrag = false;
    private bool[] isGamepadFrag = { false, false, false, false };
    private Vector3 groupVec = Vector3.zero;

    private const string runAnimSpeed = "RunSpeed";


    public GameObject[] ChildPlayer = null;
    public Animator[] AnimationImage = null;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        rb.useGravity = false;
        defaultMass = rb.mass;

        defaultCarryOverSpeed = carryOverSpeed;
        itemSizeCount = 0;
        playerCount = 0;
        needCarryCount = 0;
        mySpeed = 1.0f;
        groupVec = Vector3.zero;

        isControlFrag = false;
        for(int i= 0;i<isGamepadFrag.Length;i++)
        {
            isGamepadFrag[i] = false;
        }

        Array.Resize(ref ChildPlayer, 4);
        Array.Resize(ref AnimationImage, ChildPlayer.Length);
    }

    private void FixedUpdate()
    {
        if (isControlFrag)
        {
            OnControllFrag();
            CheckOnlyChild();
        }
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

    void OnControllFrag()
    {
        Vector2[] before = { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) };

        for (int i = 0; i < isGamepadFrag.Length; i++)
        {
            if (isGamepadFrag[i])
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

                float walkSpeed = mySpeed * animationSpeed;
                AnimationImage[i].SetFloat(runAnimSpeed, walkSpeed);
            }
        }

        groupVec.x = (before[0].x + before[1].x + before[2].x + before[3].x) * carryOverSpeed;
        groupVec.z = (before[0].y + before[1].y + before[2].y + before[3].y) * carryOverSpeed;
        rb.velocity = groupVec;
    }

    void CheckOnlyChild()
    {
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

    public void GetItemSize(int itemSize, int itemType, GameObject gameObject)
    {
        itemSizeCount = itemSize;

        carryText = gameObject.GetComponentInChildren<TextMeshPro>();
        carryText.gameObject.GetComponent<MeshRenderer>().sortingOrder = carryTextOrderInLayer;
        outline = gameObject.GetComponentInChildren<Outline>();
        outline.enabled = false;
        if (itemType == 2)
        {
            rb.mass *= 10;
        }
        CheckPlayerCount();
    }

    public void GetMyNo(int childNo,GameObject gameObject)
    {
        ChildPlayer[childNo] = gameObject;
        AnimationImage[childNo] = gameObject.GetComponent<Animator>();

        isGamepadFrag[childNo] = true;
        playerCount++;
        isControlFrag = true;
        CheckPlayerCount();
    }

    public void ReleaseChild()
    {
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

    private void DamageChild()
    {
        for (int i = 0; i < ChildPlayer.Length; i++)
        {
            if (ChildPlayer[i] != null || AnimationImage[i] != null)
            {
                AnimationImage[i].SetBool("CarryMove", false);

                ChildPlayer[i] = null;
                AnimationImage[i] = null;
            }
        }
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag("item"))
            {
                transform.GetChild(i).gameObject.GetComponent<CarryEnergy>().OutGroup();
            }
            else if (transform.GetChild(i).gameObject.CompareTag("Cannon"))
            {
                transform.GetChild(i).gameObject.GetComponent<CarryCannon>().OutGroup();
            }
        }
        AllFragFalse();
    }

    public void PlayerOutGroup(int outChildNo)
    {
        ChildPlayer[outChildNo] = null;
        AnimationImage[outChildNo] = null;
        isGamepadFrag[outChildNo] = false;
        playerCount--;
        CheckPlayerCount();
    }

    void AllFragFalse()
    {
        for(int i = 0; i < isGamepadFrag.Length; i++)
        {
            isGamepadFrag[i] = false;
        }
        isControlFrag = false;
        playerCount = 0;
        outline.enabled = true;
        outline = null;
        CheckPlayerCount();
        carryText.text = null;
        carryText = null;
        rb.mass = defaultMass;
        groupVec = Vector3.zero;
        rb.velocity = groupVec;
    }

    void CheckNeedCarryCount()
    {
        if (itemSizeCount == 0)
        {
            needCarryCount = 1;
        }
        else if (itemSizeCount == 1)
        {
            needCarryCount = 2;
        }
        else if (itemSizeCount == 2)
        {
            needCarryCount = 4;
        }
    }

    void CheckCarryText()
    {
        carryText.text = playerCount.ToString("0") + "/" + needCarryCount.ToString("0");
        if (playerCount >= needCarryCount)
        {
            carryText.color = Color.white;
            for (int i = 0; i < ChildPlayer.Length; i++)
            {
                if (ChildPlayer[i] != null)
                {
                    ChildPlayer[i].GetComponent<PlayerMove>().EndCarryEmote();
                }
            }
        }
        else if (playerCount <= 0)
        {
            carryText.text = null;
        }
        else
        {
            carryText.color = Color.red;
            for (int i = 0; i < ChildPlayer.Length; i++)
            {
                if (ChildPlayer[i] != null)
                {
                    ChildPlayer[i].GetComponent<PlayerMove>().StartCarryEmote();
                }
            }
        }
    }

    void CheckCarryOver()
    {
        if (playerCount >= needCarryCount)
        {
            carryOverSpeed = 1.0f;
        }
        else
        {
            carryOverSpeed = defaultCarryOverSpeed;
        }
    }

    void CheckMySpeed()
    {
        if (itemSizeCount == 0)
        {
            mySpeed = (moveSpeed * smallCarrySpeed[playerCount]) / playerCount;
        }
        else if (itemSizeCount == 1)
        {
            mySpeed = (moveSpeed * midiumCarrySpeed[playerCount]) / playerCount;
        }
        else if (itemSizeCount == 2)
        {
            mySpeed = (moveSpeed * largeCarrySpeed[playerCount]) / playerCount;
        }
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

        CheckNeedCarryCount();

        if (carryText != null)
        {
            CheckCarryText();
        }
        
        CheckCarryOver();
        CheckMySpeed();
    }
}
