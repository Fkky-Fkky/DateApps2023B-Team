using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_j : MonoBehaviour
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
    float SpiderTime = 0;

    int rnd;

    bool EndFlag = false;

    [SerializeField] int SpiderSpoanTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(spider.transform.position.y <= -15&&EndFlag==false)
        {
            Rigidbody rb = spider.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            EndFlag = true;
        }

        SpiderTime += Time.deltaTime;
        if(SpiderTime >= SpiderSpoanTime && rnd==1) 
        {
            SpiderTime = 0;

            x = Random.Range(rangeA.position.x, rangeB.position.x);

            z = Random.Range(rangeA.position.z, rangeB.position.z);
            Instantiate(spider, new Vector3(x, -8, z), spider.transform.rotation);
            rnd = Random.Range(1, 3);
        }

        if (SpiderTime >= SpiderSpoanTime && rnd >= 2)
        {
            SpiderTime = 0;

            x = Random.Range(rangeC.position.x, rangeD.position.x);

            z = Random.Range(rangeC.position.z, rangeD.position.z);
            Instantiate(spider, new Vector3(x, -8, z), spider.transform.rotation);
            rnd = Random.Range(1, 3);
        }

    }
}
