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
    /// グループ配下にあるプレイヤー番号のコントローラーの入力を取得する
    /// それぞれの入力を合計して全体で移動する
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
    /// 運搬を開始するプレイヤーの情報を取得する
    /// プレイヤーがグループ配下に入る際に呼び出す
    /// </summary>
    /// <param name="childNo">入るプレイヤーの番号</param>
    /// <param name="gameObject">入るプレイヤーのゲームオブジェクト</param>
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
    /// 運搬中のプレイヤーが運搬を終了する際に呼び出す
    /// </summary>
    /// <param name="outChildNo">抜けるプレイヤーの番号</param>
    public void PlayerOutGroup(int outChildNo)
    {
        ChildPlayer[outChildNo] = null;
        AnimationImage[outChildNo] = null;
        isGamepadFrag[outChildNo] = false;
        playerCount--;
        CheckPlayerCount();
    }

    /// <summary>
    /// グループ配下のプレイヤーのアニメーションを止める
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
    /// アイテム運搬に必要な人数と運搬中の移動速度を確認する
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
    /// 運搬中の人数が運搬に必要な人数かどうかを確認する
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
    /// 運搬中のプレイヤーの人数を確認する   
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
    /// ゲームパッドからの入力を止める時に呼び出す
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
    /// アイテムの重さを設定する際に呼び出す
    /// </summary>
    /// <param name="itemSize">アイテムの重さ</param>
    public void SetItenSizeCount(int itemSize)
    {
        itemSizeCount = itemSize;
    }
}
