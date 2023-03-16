//担当者:吉田理紗
using System;
using UnityEngine;

/// <summary>
/// 大砲の運搬に関するクラス
/// </summary>
public class CarryCannon : MonoBehaviour
{
    [SerializeField]
    private float defaultPosY = 51;

    [SerializeField]
    private float carryPosY = 60;

    [SerializeField]
    ItemSize myItemSize = ItemSize.Small;

    private BoxCollider boxCol = null;
    private GroupManager groupManager;
    private GameObject[] myGrabPoint = null;
    private PlayerCarryDown[] playerCarryDowns = null;

    private int myItemSizeCount = 0;
    private int number = 0;
    private bool isGroup = false;

    private const int MAX_GROUP_NUMBER = 4;
    private const int FIRST_GROUP_NUMBER = 1;

    public int GroupNumber = 1;

    /// <summary>
    /// アイテムの大きさ
    /// </summary>
    enum ItemSize
    {
        Small,
        Medium,
        Large
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        groupManager = null;
        Array.Resize(ref myGrabPoint, 0);
        Array.Resize(ref playerCarryDowns, myGrabPoint.Length);

        switch (myItemSize)
        {
            default:
                myItemSizeCount = (int)myItemSize;
                break;
        }
        number = 0;
        isGroup = false;

        GroupNumber = 1;
    }

    /// <summary>
    /// プレイヤーが自身の運搬を開始した際に呼び出す
    /// </summary>
    /// <param name="thisGrabPoint">プレイヤーの掴みポイントのゲームオブジェクト</param>
    public void GetGrabPoint(GameObject thisGrabPoint)
    {
        Array.Resize(ref myGrabPoint, myGrabPoint.Length + 1);
        Array.Resize(ref playerCarryDowns, myGrabPoint.Length);
        myGrabPoint[number] = thisGrabPoint;
        playerCarryDowns[number] = thisGrabPoint.GetComponent<PlayerCarryDown>();
        number++;

        boxCol.isTrigger = false;

        while (!isGroup)
        {
            GameObject group = GameObject.FindWithTag("Group" + GroupNumber);
            groupManager = group.GetComponent<GroupManager>();

            if (group.transform.childCount <= 0)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    carryPosY,
                    this.gameObject.transform.position.z
                    );
                gameObject.transform.SetParent(group.gameObject.transform);
                groupManager = group.GetComponent<GroupManager>();
                groupManager.GetItemSize(myItemSizeCount, this.gameObject);

                isGroup = true;
                break;
            }
            else
            {
                GroupNumber += FIRST_GROUP_NUMBER;
                if (GroupNumber > MAX_GROUP_NUMBER)
                {
                    GroupNumber = FIRST_GROUP_NUMBER;
                }
                groupManager = null;
            }
        }
    }

    /// <summary>
    /// 自身の運搬が終了した際に呼び出す
    /// </summary>
    public void OutGroup()
    {
        isGroup = false;
        gameObject.transform.parent = null;
        DoCarryCancel();
        
        this.gameObject.transform.position = new Vector3(
            this.gameObject.transform.position.x,
            defaultPosY,
            this.gameObject.transform.position.z
            );
        Array.Resize(ref myGrabPoint, 0);
        Array.Resize(ref playerCarryDowns, myGrabPoint.Length);
        number = 0;

    }

    /// <summary>
    /// 自身を運搬していたプレイヤーの関数を呼び出す処理を行う
    /// </summary>
    public void DoCarryCancel()
    {
        for (int i = 0; i < myGrabPoint.Length; i++)
        {
            playerCarryDowns[i].CarryCancel();
        }
    }
}
