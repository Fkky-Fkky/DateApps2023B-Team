using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubehantei : MonoBehaviour
{
    public CreateRandomPosition createrandomposition;

    private GameObject destroyItem = null;

    int a=0;
    int i = 0;
    int u = 0;
    int e = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("tower"); //Playerっていうオブジェクトを探す
        createrandomposition = obj.GetComponent<CreateRandomPosition>();　//付いているスクリプトを取得
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
            Destroy(destroyItem);
            createrandomposition.Settower_bild_flag();
            Debug.Log("a");
        }
        if (other.gameObject.CompareTag("item2"))
        {
            destroyItem = other.gameObject;
            Destroy(destroyItem);
            createrandomposition.Settower_bild_flag2();
            Debug.Log("i");
        }
        if (other.gameObject.CompareTag("item3"))
        {
            destroyItem = other.gameObject;
            Destroy(destroyItem);
            createrandomposition.Settower_bild_flag3();
            Debug.Log("u");
        }
        if (other.gameObject.CompareTag("item4"))
        {
            destroyItem = other.gameObject;
            Destroy(destroyItem);
            createrandomposition.Settower_bild_flag4();
            Debug.Log("e");
        }
    }
}
