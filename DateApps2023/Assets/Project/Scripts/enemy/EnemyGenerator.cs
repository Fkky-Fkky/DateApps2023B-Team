using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �G�l�~�[�𐶂ݏo���W�F�l���[�^�[�N���X
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��������͈�A")]
    private Transform rangeA = null;

    [SerializeField]
    [Tooltip("��������͈�B")]
    private Transform rangeB = null;

    [SerializeField]
    [Tooltip("��������͈�C")]
    private Transform rangeC = null;

    [SerializeField]
    [Tooltip("��������͈�D")]
    private Transform rangeD = null;

    [SerializeField]
    [Tooltip("�X�p�C�_�[�̐������o")]
    private int spiderSpoanTime = 10;

    public GameObject spider = null;

    private float x = 0;

    private float z = 0;

    private float spiderTime = 0;

    private int rnd = 0;

    private bool endFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        spiderTime += Time.deltaTime;

        if (spider.transform.position.y <= -15 && endFlag == false)
        {
            //Rigidbody rb = spider.GetComponent<Rigidbody>();
            //rb.useGravity = false;
            //rb.constraints = RigidbodyConstraints.FreezePosition;
            //endFlag = true;
        }
        SummonSpider();
    }
    /// <summary>
    /// �G�l�~�[�𐶂ݏo��
    /// </summary>
    private void SummonSpider()
    {
        if (spiderTime >= spiderSpoanTime && rnd == 1)
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
