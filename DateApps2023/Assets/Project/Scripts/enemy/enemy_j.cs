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

    [SerializeField]
    [Tooltip("¶¬‚·‚é”ÍˆÍC")]
    private Transform rangeC;
    [SerializeField]
    [Tooltip("¶¬‚·‚é”ÍˆÍD")]
    private Transform rangeD;

    public GameObject spider;

    float x;

    float z;
    float spider_time = 0;

    int rnd;

    bool end_flag = false;

    [SerializeField] int spider_spoan_time = 3;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(spider.transform.position.y <= -15&&end_flag==false)
        {
            Rigidbody rb = spider.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            //rb.AddForce(force, ForceMode.Impulse);

            end_flag = true;
        }

        spider_time += Time.deltaTime;
        if(spider_time >= spider_spoan_time && rnd==1) 
        {
            spider_time = 0;

            x = Random.Range(rangeA.position.x, rangeB.position.x);

            z = Random.Range(rangeA.position.z, rangeB.position.z);
            Instantiate(spider, new Vector3(x, -8, z), spider.transform.rotation);
            rnd = Random.Range(1, 3);
        }

        if (spider_time >= spider_spoan_time && rnd >= 2)
        {
            spider_time = 0;

            x = Random.Range(rangeC.position.x, rangeD.position.x);

            z = Random.Range(rangeC.position.z, rangeD.position.z);
            Instantiate(spider, new Vector3(x, -8, z), spider.transform.rotation);
            rnd = Random.Range(1, 3);
        }

    }
}
