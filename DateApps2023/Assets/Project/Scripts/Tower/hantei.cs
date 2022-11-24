using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;
using static PlayerMove;
using static UnityEditor.PlayerSettings;

public class hantei : MonoBehaviour
{
    #region
    public GameObject[] myGrabPoint = null;
    public PlayerCarryDown[] playerCarryDowns = null;
    private PlayerController playercontroller;
    int number = 0;
    public int groupNumber = 1;
    private bool InGroup = false;

    BoxCollider boxCol;
    BoxCollider childBoxCol;
    private Vector3 sizeCount;

    enum ItemSize
    {
        Small,
        Medium,
        Large
    }

    [SerializeField]
    ItemSize myItemSize = ItemSize.Small;
    private int itemSizeCount = 0;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();

        switch (myItemSize)
        {
            case ItemSize.Small:
                itemSizeCount = (int)ItemSize.Small;
                break;
            case ItemSize.Medium:
                itemSizeCount = (int)ItemSize.Medium;
                break;
            case ItemSize.Large:
                itemSizeCount = (int)ItemSize.Large;
                break;
        }
    }

    public void GetGrabPoint(GameObject thisGrabPoint)
    {
        Array.Resize(ref myGrabPoint, myGrabPoint.Length + 1);
        Array.Resize(ref playerCarryDowns, myGrabPoint.Length);
        myGrabPoint[number] = thisGrabPoint;
        playerCarryDowns[number] = thisGrabPoint.GetComponent<PlayerCarryDown>();
        number++;

        while (!InGroup)
        {
            GameObject group = GameObject.Find("Group" + groupNumber);
            if (group.transform.childCount <= 0)
            {
                gameObject.transform.SetParent(group.gameObject.transform);
                playercontroller = group.GetComponent<PlayerController>();
                playercontroller.GetItemSize(itemSizeCount);
                InGroup = true;
            }
            else
            {
                groupNumber += 1;
                if (groupNumber > 4)
                {
                    groupNumber = 1;
                }
            }
        }

        sizeCount = boxCol.size;
    }

    public void OutGroup()
    {
        InGroup = false;
        gameObject.transform.parent = null;
        boxCol.size = sizeCount;

    }

    public void DestroyMe()
    {
        playercontroller.ReleaseChild();

        DoHanteiEnter();
        Destroy(gameObject);
    }

    public void DoHanteiEnter()
    {
        for (int i = 0; i < myGrabPoint.Length; i++)
        {
            playerCarryDowns[i].HanteiEnter();
        }
    }
    

}
