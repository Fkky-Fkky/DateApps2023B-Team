using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryCannon : MonoBehaviour
{
    /// <summary>
    /// 大砲の運搬に関する処理を行う
    /// </summary>
    #region
    [SerializeField]
    private float defaultPosY = 51;

    [SerializeField]
    private float carryPosY = 60;

    [SerializeField]
    ItemSize myItemSize = ItemSize.Small;

    private BoxCollider boxCol = null;
    private PlayerController playercontroller;
    private GameObject[] myGrabPoint = null;
    private PlayerCarryDown[] playerCarryDowns = null;

    private int myItemSizeCount = 0;
    private int number = 0;
    private bool isGroup = false;

    public int GroupNumber = 1;

    enum ItemSize
    {
        Small,
        Medium,
        Large
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        playercontroller = null;
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
            playercontroller = group.GetComponent<PlayerController>();

            if (group.transform.childCount <= 0)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    carryPosY,
                    this.gameObject.transform.position.z
                    );
                gameObject.transform.SetParent(group.gameObject.transform);
                playercontroller = group.GetComponent<PlayerController>();
                playercontroller.GetItemSize(myItemSizeCount, 2, this.gameObject);

                isGroup = true;
                break;
            }
            else
            {
                GroupNumber += 1;
                if (GroupNumber > 4)
                {
                    GroupNumber = 1;
                }
                playercontroller = null;
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
