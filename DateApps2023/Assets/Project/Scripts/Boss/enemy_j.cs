using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy_j : MonoBehaviour
{
    [SerializeField]
    [Tooltip("¶¬‚·‚é”ÍˆÍA")]
    private Transform rangeA;
    [SerializeField]
    [Tooltip("¶¬‚·‚é”ÍˆÍB")]
    private Transform rangeB;

    public GameObject spider;

    float x;

    float z;

    float spider_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spider_time += Time.deltaTime;
        if(spider_time >=60) 
        {
            spider_time = 0;
           
            x = Random.Range(rangeA.position.x, rangeB.position.x);

            z = Random.Range(rangeA.position.z, rangeB.position.z);
            Instantiate(spider, new Vector3(x, 16, z), spider.transform.rotation);
        }

    }
}
