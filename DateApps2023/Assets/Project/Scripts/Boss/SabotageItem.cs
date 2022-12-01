using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageItem : MonoBehaviour
{
    #region
    public GameObject[] myGrabPoint = null;
    public PlayerCarryDown[] playerCarryDowns = null;
    private PlayerController playercontroller;
    int number = 0;
    public int groupNumber = 1;
    private bool InGroup = false;

    MeshCollider meshCol;
    private Rigidbody rb;
    public bool AtackTiming = true;
    public bool AvoidPlayer = true;

    enum ItemSize
    {
        Small,
        Medium,
        Large
    }

    [SerializeField]
    ItemSize myItemSize = ItemSize.Small;
    private int itemSizeCount = 0;

    private float fallY = 56.0f;
    public float Multiplier = 1f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        meshCol = GetComponent<MeshCollider>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;

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

    private void Update()
    {
        if(this.gameObject.transform.position.y < fallY)
        {
            AvoidPlayer = false;
            AtackTiming = false;

            this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    fallY,
                    this.gameObject.transform.position.z);
        }

        if (AvoidPlayer)
        {
            rb = this.gameObject.AddComponent<Rigidbody>();
            rb = this.gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce((Multiplier - 1f) * Physics.gravity, ForceMode.Acceleration);

        }
        else
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            Destroy(rigidbody);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (AvoidPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerDamage>().SetSabotageObject(this.gameObject);
                collision.gameObject.GetComponent<PlayerDamage>().AvoidObject();
            }
            if (collision.gameObject.CompareTag("item")
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4"))
            {
                collision.gameObject.GetComponent<hantei>().SetSabotageObject(this.gameObject);
                collision.gameObject.GetComponent<hantei>().AvoidSabotageItem();
            }
            if (collision.gameObject.CompareTag("CloneSabotageItem"))
            {
                AvoidPlayer = false;
            }
        }
        if (AtackTiming)
        {
            if (collision.gameObject.CompareTag("Group"))
            {
                Debug.Log("AtackGroup");
                collision.gameObject.GetComponent<PlayerController>().SetSabotageItem(this.gameObject);
                collision.gameObject.GetComponent<PlayerController>().DamageChild();

            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("AtackPlayer");
                collision.gameObject.GetComponent<PlayerDamage>().CallDamage();
            }
            AtackTiming = false;
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

        rb = GetComponentInParent<Rigidbody>();

        this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    55,
                    this.gameObject.transform.position.z);
        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        meshCol.isTrigger = true;
    }

    public void OutGroup()
    {
        InGroup = false;
        gameObject.transform.parent = null;
        meshCol.isTrigger = false;
        DoHanteiEnter();


        rb = this.gameObject.AddComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    public void DestroyMe()
    {
        playercontroller.ReleaseChild();
        GameObject Boss = GameObject.Find("Boss");
        if (Boss.GetComponent<BossAttack>().instantCloneValue > 0)
        {
            Boss.GetComponent<BossAttack>().instantCloneValue--;
        }

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
