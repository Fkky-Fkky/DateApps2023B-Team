using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class hantei : MonoBehaviour
{
    public GameObject[] myGrabPoint = null;
    public PlayerCarryDown[] playerCarryDowns = null;
    private PlayerController playercontroller;
    int number = 0;
    public int groupNumber = 1;
    private bool InGroup = false;

    BoxCollider boxCol;
    private Vector3 sizeCount;

    [SerializeField]
    private Vector3 afterSizeCount = new Vector3(4, 1, 4);

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        
        
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
        //boxCol.size = afterSizeCount;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        boxCol.size = sizeCount;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        boxCol.size = afterSizeCount;
    //    }
    //}

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
