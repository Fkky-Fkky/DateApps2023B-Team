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

    BoxCollider boxCol;
    private Rigidbody rb;
    private bool AtackTiming = true;
    private bool AvoidPlayer = true;

    private MeshRenderer mesh;

    enum ItemSize
    {
        Small,
        Medium,
        Large
    }

    [SerializeField]
    private ItemSize myItemSize = ItemSize.Small;
    private int itemSizeCount = 0;

    private float fallY = 55f;
    public float Multiplier = 1f;

    [SerializeField]
    private float destroyTime = 2.0f;
    private bool isDestroy = false;
    private float currentTime = 0;

    [SerializeField]
    private Transform wavePoint = null;

    [SerializeField]
    private GameObject damageWaveEffect = null;
    private bool firstEffect = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();

        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = mesh.material.color - new Color(0, 0, 0, 0);

        rb = this.gameObject.AddComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;

        AvoidPlayer = true;
        AtackTiming = true;

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
            if(firstEffect)
            {
                Instantiate(damageWaveEffect, wavePoint.position, Quaternion.identity);
                GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("ShockWaveEffect");
                foreach (GameObject clone_shockEffect in cloneItem)
                {
                    Destroy(clone_shockEffect, 5);
                }
                firstEffect = false;
            }

            this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    fallY,
                    this.gameObject.transform.position.z);
        }
        if (this.gameObject.transform.position.y >= fallY + 50)
        {
            firstEffect = true;
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

        if (isDestroy)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= destroyTime)
            {
                playercontroller.ReleaseChild();
                DoHanteiEnter();
                isDestroy = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (AtackTiming)
        {
            if (collision.gameObject.CompareTag("Group"))
            {
                //collision.gameObject.GetComponent<PlayerController>().SetSabotageItem(this.gameObject);
                collision.gameObject.GetComponent<PlayerController>().DamageChild();
                if (firstEffect)
                {
                    Instantiate(damageWaveEffect, wavePoint.position, Quaternion.identity);
                    firstEffect = false;
                }
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerDamage>().CallDamage();
                if (firstEffect)
                {
                    Instantiate(damageWaveEffect, wavePoint.position, Quaternion.identity);
                    firstEffect = false;
                }
            }
            AtackTiming = false;
        }
        if (AvoidPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerDamage>().SetSabotageObject(this.gameObject);
                //collision.gameObject.GetComponent<PlayerDamage>().AvoidObject();
            }
            if (collision.gameObject.CompareTag("item")
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4"))
            {
                collision.gameObject.GetComponent<hantei>().SetSabotageObject(this.gameObject);
                //collision.gameObject.GetComponent<hantei>().AvoidSabotageItem();
            }
            if (firstEffect)
            {
                Instantiate(damageWaveEffect, wavePoint.position, Quaternion.identity);
                GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("ShockWaveEffect");
                foreach (GameObject clone_shockEffect in cloneItem)
                {
                    Destroy(clone_shockEffect, 5);
                }
                firstEffect = false;
            }

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
                playercontroller.GetItemSize(itemSizeCount, 2);
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
                    60,
                    this.gameObject.transform.position.z);
        //rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        boxCol.isTrigger = true;
    }

    public void OutGroup()
    {
        InGroup = false;
        gameObject.transform.parent = null;
        boxCol.isTrigger = false;
        DoHanteiEnter();

        this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    56,
                    this.gameObject.transform.position.z);

        rb = this.gameObject.AddComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    public void DestroyMe()
    {
        playercontroller.ReleaseChild();
        DoHanteiEnter();
        isDestroy = true;        
        StartCoroutine(Transparent());
    }

    public void DoHanteiEnter()
    {
        for (int i = 0; i < myGrabPoint.Length; i++)
        {
            playerCarryDowns[i].HanteiEnter();
        }
    }

    IEnumerator Transparent()
    {
        while (true)
        {
            for (int i = 0; i < 100; i++)
            {
                mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 1);
            }
            yield return new WaitForSeconds(0.2f);

            for (int j = 0; j < 100; j++)
            {
                mesh.material.color = mesh.material.color + new Color32(0, 0, 0, 1);
            }
            yield return new WaitForSeconds(0.2f);
        }


    }

}
