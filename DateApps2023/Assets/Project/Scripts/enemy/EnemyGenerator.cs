using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Transform rangeA = null;

    [SerializeField]
    [Tooltip("生成する範囲B")]
    private Transform rangeB = null;

    [SerializeField]
    [Tooltip("生成する範囲C")]
    private Transform rangeC = null;

    [SerializeField]
    [Tooltip("生成する範囲D")]
    private Transform rangeD = null;

    [SerializeField]
    [Tooltip("スパイダーの生成感覚")]
    private int spiderSpoanTime = 3;

    public GameObject spider = null;

    float x = 0;

    float z = 0;
    float spiderTime = 0;

    int rnd = 0;

    bool endFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        spiderTime += Time.deltaTime;

        if (spider.transform.position.y <= -15&&endFlag==false)
        {
            Rigidbody rb = spider.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            endFlag = true;
        }

        if(spiderTime >= spiderSpoanTime && rnd==1) 
        {
            spiderTime = 0;

            x = Random.Range(rangeA.position.x, rangeB.position.x);

            z = Random.Range(rangeA.position.z, rangeB.position.z);
            Instantiate(spider, new Vector3(x, -8, z), spider.transform.rotation);
            rnd = Random.Range(1, 3);
        }

        if (spiderTime >= spiderSpoanTime && rnd >= 2)
        {
            spiderTime = 0;

            x = Random.Range(rangeC.position.x, rangeD.position.x);

            z = Random.Range(rangeC.position.z, rangeD.position.z);
            Instantiate(spider, new Vector3(x, -8, z), spider.transform.rotation);
            rnd = Random.Range(1, 3);
        }

    }
}
