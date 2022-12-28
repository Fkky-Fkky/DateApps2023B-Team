using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryCannon : MonoBehaviour
{
    #region
    public GameObject[] myGrabPoint = null;
    public PlayerCarryDown[] playerCarryDowns = null;
    private PlayerController playercontroller;
    int number = 0;
    public int groupNumber = 1;
    private bool InGroup = false;

    [SerializeField]
    private float defaultPosY = 51;

    [SerializeField]
    private float carryPosY = 60;

    BoxCollider boxCol = null;


    enum ItemSize
    {
        Small,
        Medium,
        Large
    }

    [SerializeField]
    ItemSize myItemSize = ItemSize.Small;
    private int myItemSizeCount = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();

        switch (myItemSize)
        {
            case ItemSize.Small:
                myItemSizeCount = (int)ItemSize.Small;
                break;
            case ItemSize.Medium:
                myItemSizeCount = (int)ItemSize.Medium;
                break;
            case ItemSize.Large:
                myItemSizeCount = (int)ItemSize.Large;
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


        boxCol.isTrigger = false;

        while (!InGroup)
        {
            GameObject group = GameObject.FindWithTag("Group" + groupNumber);
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
                playercontroller.GetItemSize(myItemSizeCount, 1);

                InGroup = true;
                break;
            }
            else
            {
                groupNumber += 1;
                if (groupNumber > 4)
                {
                    groupNumber = 1;
                }
                playercontroller = null;
            }
        }
    }

    public void OutGroup()
    {
        InGroup = false;
        gameObject.transform.parent = null;
        DoHanteiEnter();

        this.gameObject.transform.position = new Vector3(
            this.gameObject.transform.position.x,
            defaultPosY,
            this.gameObject.transform.position.z
            );

    }

    public void DoHanteiEnter()
    {
        for (int i = 0; i < myGrabPoint.Length; i++)
        {
            playerCarryDowns[i].HanteiEnter();
        }
    }
}
