using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    #region
    private Rigidbody rb;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private BossDamage bossDamage;

    [SerializeField]
    [Tooltip("���̍U���܂ł̊Ԋu")]
    private float intervalTime = 15;

    [SerializeField]
    [Tooltip("�U�����s��(�{�X���~�܂��Ă��鎞��)")]
    private float attackTime = 5;

    [SerializeField]
    [Tooltip("�W�Q�A�C�e����������܂ł̎���")]
    private float sabotageTime = 30;

    private bool sabotageFlag = false;
    private bool alreadyInstantFlag = false;

    [SerializeField]
    [Tooltip("��������͈�A(=�A�C�e���Ɠ����͈�)")]
    private Transform rangeA;

    [SerializeField]
    [Tooltip("��������͈�B(=�A�C�e���Ɠ����͈�)")]
    private Transform rangeB;

    [SerializeField]
    [Tooltip("�W�Q�A�C�e���������鍂��")]
    private float instancePosY = 55;

    [SerializeField]
    [Tooltip("�W�Q�A�C�e��(���Ƃ����������������)")]
    private GameObject[] sabotageItem;

    [SerializeField]
    private LayerMask LayerMask;

    [SerializeField]
    [Tooltip("�W�Q�A�C�e���d�Ȃ���p�̉�����")]
    private Vector3 halfExtents = new Vector3(12.5f, 12.5f, 12.5f);

    float time = 0;
    float currentSabotageTime = 0;
    #endregion

    #region �\���A�C�e��(��)�p
    private bool alreadyPredictFlag = false;
    [SerializeField]
    private float PredictInstancePosY = 55;
    [SerializeField]
    private GameObject[] predictSabotageItem;
    public Vector3[] predictInstantPos;
    public Vector3[] instantPos;
    int number = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bossDamage = GetComponent<BossDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossDamage.knockBackFlag)
        {
            time = 0;
        }

        //if (!sabotageFlag)
        //{
        //    time += Time.deltaTime;

        //    if (time > intervalTime)
        //    {
        //        time = 0;
        //        sabotageFlag = true;
        //        alreadyInstantFlag = false;
        //    }
        //}
        //if (sabotageFlag)
        //{
        //    time += Time.deltaTime;
        //    currentSabotageTime += Time.deltaTime;

        //    if (time < attackTime)
        //    {
        //        boss.transform.position = new Vector3(
        //            0.0f,
        //            boss.transform.position.y,
        //            boss.transform.position.z);
        //        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        //    }
        //    else
        //    {
        //        if (!alreadyInstantFlag)
        //        {
        //            GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("CloneSabotageItem");
        //            if (cloneItem.Length >= sabotageItem.Length)
        //            {
        //                alreadyInstantFlag = true;
        //            }

        //            for (int i = 0; i < sabotageItem.Length - cloneItem.Length; i++)
        //            {
        //                float x = Random.Range(rangeA.position.x, rangeB.position.x);
        //                float z = Random.Range(rangeA.position.z, rangeB.position.z);
        //                Vector3 instantPos = new Vector3(x, instancePosY, z);

        //                int layerMask = 1 << LayerMask;
        //                layerMask = ~layerMask;

        //                if (!Physics.CheckBox(instantPos, halfExtents, Quaternion.identity, layerMask))
        //                {
        //                    Instantiate(sabotageItem[i], instantPos, Quaternion.identity);
        //                }
        //            }
        //        }
        //    }
        //    if (currentSabotageTime > sabotageTime)
        //    {
        //        AllDestroy();
        //        time = 0;
        //        currentSabotageTime = 0;
        //        sabotageFlag = false;
        //    }
        //}

        #region �\���A�C�e��(��)�p
        if (!sabotageFlag)
        {
            time += Time.deltaTime;
            number = 0;

            if (time > intervalTime)
            {
                time = 0;
                sabotageFlag = true;
                alreadyInstantFlag = false;
                alreadyPredictFlag = false;
            }
        }
        if (sabotageFlag)
        {
            time += Time.deltaTime;
            currentSabotageTime += Time.deltaTime;

            if (time < attackTime)
            {
                boss.transform.position = new Vector3(
                    0.0f,
                    boss.transform.position.y,
                    boss.transform.position.z);
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);

                if (!alreadyPredictFlag)
                {
                    GameObject[] clonePredictItem = GameObject.FindGameObjectsWithTag("ClonePredictItem");
                    if (clonePredictItem.Length >= sabotageItem.Length)
                    {
                        alreadyPredictFlag = true;
                        number = 0;
                    }

                    for (int i = 0; i < sabotageItem.Length - clonePredictItem.Length; i++)
                    {
                        float x = Random.Range(rangeA.position.x, rangeB.position.x);
                        float z = Random.Range(rangeA.position.z, rangeB.position.z);
                        predictInstantPos[number] = new Vector3(x, instancePosY, z);
                        Vector3 checkPos = predictInstantPos[number];
                        checkPos.y = PredictInstancePosY;

                        int layerMask = 1 << LayerMask;
                        layerMask = ~layerMask;

                        if (!Physics.CheckBox(checkPos, halfExtents, Quaternion.identity, layerMask))
                        {
                            instantPos[number] = predictInstantPos[number];
                            Instantiate(predictSabotageItem[i], checkPos, Quaternion.identity);
                            number++;
                        }
                    }
                }
            }
            else
            {
                PredictDestroy();

                if (!alreadyInstantFlag)
                {
                    GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("CloneSabotageItem");
                    if (cloneItem.Length >= sabotageItem.Length)
                    {
                        alreadyInstantFlag = true;
                    }

                    for (int i = 0; i < sabotageItem.Length - cloneItem.Length; i++)
                    {
                        Instantiate(sabotageItem[i], instantPos[number], Quaternion.identity);
                        number++;
                    }
                }
            }

            if (currentSabotageTime > sabotageTime)
            {
                AllDestroy();
                time = 0;
                currentSabotageTime = 0;
                sabotageFlag = false;
            }
        }
        #endregion
    }

    void AllDestroy()
    {
        GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("CloneSabotageItem");

        foreach (GameObject clone_sabotageItem in cloneItem)
        {
            Destroy(clone_sabotageItem);
        }
    }

    #region �\���A�C�e��(��)�p
    void PredictDestroy()
    {
        GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("ClonePredictItem");

        foreach (GameObject clone_predictItem in cloneItem)
        {
            Destroy(clone_predictItem);
        }
    }
    #endregion
}
