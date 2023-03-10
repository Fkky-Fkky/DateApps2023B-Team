using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroupMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 250.0f;

    [SerializeField]
    private float carryOverSpeed = 0.1f;

    [SerializeField]
    private float animationSpeed = 0.001f;

    [SerializeField]
    private float[] smallCarrySpeed = null;

    [SerializeField]
    private float[] midiumCarrySpeed = null;

    [SerializeField]
    private float[] largeCarrySpeed = null;

    private Rigidbody rb = null;
    private GroupManager groupManager = null;

    private int itemSizeCount = 0;
    private int playerCount = 0;
    private int needCarryCount = 0;

    private float mySpeed = 1.0f;
    private float defaultCarryOverSpeed = 0.0f;

    private bool isControlFrag = false;
    private bool[] isGamepadFrag = { false, false, false, false };
    private Vector3 groupVec = Vector3.zero;

    private const string runAnimSpeed = "RunSpeed";

    public GameObject[] ChildPlayer = null;
    public Animator[] AnimationImage = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        rb.useGravity = false;

        groupManager = GetComponent<GroupManager>();

        defaultCarryOverSpeed = carryOverSpeed;
        itemSizeCount = 0;
        playerCount = 0;
        needCarryCount = 0;
        mySpeed = 1.0f;
        groupVec = Vector3.zero;

        isControlFrag = false;
        for (int i = 0; i < isGamepadFrag.Length; i++)
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
            groupManager.CheckOnlyChild();
        }
    }

    /// <summary>
    /// �O���[�v�z���ɂ���v���C���[�ԍ��̃R���g���[���[�̓��͂��擾����
    /// ���ꂼ��̓��͂����v���đS�̂ňړ�����
    /// </summary>
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

    /// <summary>
    /// �^�����J�n����v���C���[�̏����擾����
    /// �v���C���[���O���[�v�z���ɓ���ۂɌĂяo��
    /// </summary>
    /// <param name="childNo">����v���C���[�̔ԍ�</param>
    /// <param name="gameObject">����v���C���[�̃Q�[���I�u�W�F�N�g</param>
    public void GetMyNo(int childNo, GameObject gameObject)
    {
        ChildPlayer[childNo] = gameObject;
        AnimationImage[childNo] = gameObject.GetComponent<Animator>();

        isGamepadFrag[childNo] = true;
        playerCount++;
        isControlFrag = true;
        CheckPlayerCount();
    }

    /// <summary>
    /// �^�����̃v���C���[���^�����I������ۂɌĂяo��
    /// </summary>
    /// <param name="outChildNo">������v���C���[�̔ԍ�</param>
    public void PlayerOutGroup(int outChildNo)
    {
        ChildPlayer[outChildNo] = null;
        AnimationImage[outChildNo] = null;
        isGamepadFrag[outChildNo] = false;
        playerCount--;
        CheckPlayerCount();
    }

    /// <summary>
    /// �O���[�v�z���̃v���C���[�̃A�j���[�V�������~�߂�
    /// </summary>
    public void SetNullPlayer()
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
    }

    /// <summary>
    /// �A�C�e���^���ɕK�v�Ȑl���Ɖ^�����̈ړ����x���m�F����
    /// </summary>
    void CheckMySpeed()
    {
        switch (itemSizeCount)
        {
            case 0:
                needCarryCount = 1;
                mySpeed = (moveSpeed * smallCarrySpeed[playerCount]) / playerCount;
                break;
            case 1:
                needCarryCount = 2;
                mySpeed = (moveSpeed * midiumCarrySpeed[playerCount]) / playerCount;
                break;
            case 2:
                needCarryCount = 4;
                mySpeed = (moveSpeed * largeCarrySpeed[playerCount]) / playerCount;
                break;
        }
    }

    /// <summary>
    /// �^�����̐l�����^���ɕK�v�Ȑl�����ǂ������m�F����
    /// </summary>
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

    /// <summary>
    /// �^�����̃v���C���[�̐l�����m�F����   
    /// </summary>
    public void CheckPlayerCount()
    {
        if (playerCount < 0)
        {
            playerCount = 0;
        }
        else if (playerCount > 4)
        {
            playerCount = 4;
        }

        CheckMySpeed();
        CheckCarryOver();
        groupManager.CheckCarryText(playerCount, needCarryCount);
    }

    /// <summary>
    /// �Q�[���p�b�h����̓��͂��~�߂鎞�ɌĂяo��
    /// </summary>
    public void FalseGamepad()
    {
        for (int i = 0; i < isGamepadFrag.Length; i++)
        {
            isGamepadFrag[i] = false;
        }
        isControlFrag = false;
        playerCount = 0;
    }

    /// <summary>
    /// �A�C�e���̏d����ݒ肷��ۂɌĂяo��
    /// </summary>
    /// <param name="itemSize">�A�C�e���̏d��</param>
    public void SetItenSizeCount(int itemSize)
    {
        itemSizeCount = itemSize;
    }
}
