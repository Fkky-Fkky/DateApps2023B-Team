using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubehantei : MonoBehaviour
{
    public CreateRandomPosition createrandomposition;
    public hantei itemHantei;

    private GameObject destroyItem = null;


    // Start is called before the first frame update
    void Start()
    {
        GameObject obj_tower = GameObject.Find("tower");
        createrandomposition = obj_tower.GetComponent<CreateRandomPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            destroyItem = other.gameObject;
            destroyItem.GetComponent<hantei>().DestroyMe();
            createrandomposition.Settower_bild_flag();
            Debug.Log("a");
        }
        if (other.gameObject.CompareTag("item2"))
        {
            destroyItem = other.gameObject;
            destroyItem.GetComponent<hantei>().DestroyMe();
            createrandomposition.Settower_bild_flag2();
            Debug.Log("i");
        }
        if (other.gameObject.CompareTag("item3"))
        {
            destroyItem = other.gameObject;
            destroyItem.GetComponent<hantei>().DestroyMe();
            createrandomposition.Settower_bild_flag3();
            Debug.Log("u");
        }
        if (other.gameObject.CompareTag("item4"))
        {
            destroyItem = other.gameObject;
            destroyItem.GetComponent<hantei>().DestroyMe();
            createrandomposition.Settower_bild_flag4();
            Debug.Log("e");
        }
    }
}
