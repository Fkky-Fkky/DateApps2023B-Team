using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;

public class CarryEnergy : MonoBehaviour
{
    #region
    private GameObject[] myGrabPoint = null;
    private PlayerCarryDown[] playerCarryDowns = null;
    private PlayerController playercontroller;
    private int number = 0;
    public int groupNumber = 1;
    private bool isInGroup = false;

    [SerializeField]
    private float defaultPosY = 51;

    [SerializeField]
    private float carryPosY = 60;

    private BoxCollider boxCol = null;

    public enum ItemSize
    {
        Small,
        Medium,
        Large
    }

    [SerializeField]
    ItemSize myItemSize = ItemSize.Small;
    public int MyItemSizeCount
    {
        get { return (int)myItemSize; }
        private set { MyItemSizeCount = (int)myItemSize; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        Array.Resize(ref myGrabPoint, 0);
        Array.Resize(ref playerCarryDowns, myGrabPoint.Length);
    }

    public void GetGrabPoint(GameObject thisGrabPoint)
    {
        Array.Resize(ref myGrabPoint, myGrabPoint.Length + 1);
        Array.Resize(ref playerCarryDowns, myGrabPoint.Length);
        myGrabPoint[number] = thisGrabPoint;
        playerCarryDowns[number] = thisGrabPoint.GetComponent<PlayerCarryDown>();
        number++;

        
        boxCol.isTrigger = false;

        while (!isInGroup)
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
                playercontroller.GetItemSize(MyItemSizeCount, 1, this.gameObject);
                
                isInGroup = true;
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
        isInGroup = false;
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

    public void DestroyMe()
    {
        playercontroller.ReleaseChild();

        DoCarryCancel();
        Destroy(gameObject);
    }

    public void DoCarryCancel()
    {
        for (int i = 0; i < myGrabPoint.Length; i++)
        {
            playerCarryDowns[i].CarryCancel();
        }
    }

}
