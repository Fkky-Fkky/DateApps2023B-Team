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

    [SerializeField]
    private float defaultPosY = 51;

    [SerializeField]
    private float carryPosY = 60;


    enum ItemSize
    {
        Small,
        Medium,
        Large
    }

    [SerializeField]
    ItemSize myItemSize = ItemSize.Small;
    private int itemSizeCount = 0;

    private GameObject sabotageObject;
    //[SerializeField]
    //private Vector2 rimitPosX = Vector2.zero;
    //[SerializeField]
    //private Vector2 rimitPosZ = Vector2.zero;
    //[SerializeField]
    //private Vector2 respownPos = Vector2.zero;

    //float time = 0;
    //[SerializeField]
    //private float respawnTime = 1.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
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

    //private void Update()
    //{
    //    if (this.gameObject.transform.position.x < rimitPosX.x || this.gameObject.transform.position.x > rimitPosX.y)
    //    {
    //        time += Time.deltaTime;
    //        if (time > respawnTime)
    //        {
    //            this.gameObject.transform.position = new Vector3(respownPos.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    //            time = 0;
    //        }
    //    }
    //    if (this.gameObject.transform.position.z < rimitPosZ.x || this.gameObject.transform.position.z > rimitPosZ.y)
    //    {
    //        time += Time.deltaTime;
    //        if (time > respawnTime)
    //        {
    //            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, respownPos.y);
    //            time = 0;
    //        }
    //    }
    //}

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
                playercontroller.GetItemSize(itemSizeCount, 1);
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

        this.gameObject.transform.position = new Vector3(
          this.gameObject.transform.position.x,
          carryPosY,
          this.gameObject.transform.position.z
          );

    }

    //public void AvoidSabotageItem()
    //{
    //    var heading = this.gameObject.transform.position - sabotageObject.transform.position;
    //    this.gameObject.transform.position += new Vector3(heading.x * 1.5f, 0.0f, heading.z * 1.5f);

    //}

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

    public void SetSabotageObject(GameObject setObject)
    {
        sabotageObject = setObject;
    }
}
