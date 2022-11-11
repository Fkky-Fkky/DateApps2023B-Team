using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class hantei : MonoBehaviour
{
    //GameObject ObjectB;
    //CreateRandomPosition CR;
    //public CreateRandomPosition createrandomposition;

   
    //public CreateRandomPosition CreateRandomPosition;

    // Start is called before the first frame update
    void Start()
    {
        //ObjectB = GameObject.Find("ObjectB");
        //CR = ObjectB.GetComponent();
        

       
        //GameObject obj = GameObject.Find("tower"); //Playerっていうオブジェクトを探す
        //createrandomposition = obj.GetComponent<CreateRandomPosition>();　//付いているスクリプトを取得


        //CreateRandomPosition createrandomposition=GetComponent<CreateRandomPosition>();
    }


    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hantei"))
        {
            Destroy(gameObject);
            //createrandomposition.Settower_bild_flag();
        }
    }

    
}
