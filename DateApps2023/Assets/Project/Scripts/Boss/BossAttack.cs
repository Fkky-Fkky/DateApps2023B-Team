using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    #region
    enum SabotageType
    {
        None,
        Rubble, //瓦礫
        Flame   //火の粉
    }

    [SerializeField]
    SabotageType mySabotageType = SabotageType.None;


    private Rigidbody rb;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private BossDamage bossDamage;

    [SerializeField]
    [Tooltip("次の攻撃までの間隔")]
    private float intervalTime = 15;

    [SerializeField]
    [Tooltip("攻撃実行中(ボスが止まっている時間)")]
    private float attackTime = 5;

    [SerializeField]
    [Tooltip("妨害アイテムが消えるまでの時間")]
    private float sabotageTime = 30;

    private bool sabotageFlag = false;
    private bool alreadyInstantFlag = false;

    [SerializeField]
    [Tooltip("生成する範囲A(=アイテムと同じ範囲)")]
    private Transform rangeA;

    [SerializeField]
    [Tooltip("生成する範囲B(=アイテムと同じ範囲)")]
    private Transform rangeB;

    [SerializeField]
    [Tooltip("妨害アイテムが落ちる高さ")]
    private float instancePosY = 55;

    [SerializeField]
    [Tooltip("妨害アイテム(落としたい数だけ入れる)")]
    private GameObject[] sabotageItem;

    [SerializeField]
    private LayerMask LayerMask;

    [SerializeField]
    [Tooltip("妨害アイテム重なり回避用の仮判定")]
    private Vector3 halfExtents = new Vector3(12.5f, 12.5f, 12.5f);

    float time = 0;
    float currentSabotageTime = 0;

    private bool firstRubble = true;

    private float RotateNumber;
    private Quaternion RotateY;
    #endregion

    #region 予測アイテム(仮)用
    private bool alreadyPredictFlag = false;
    [SerializeField]
    private float PredictInstancePosY = 55;
    [SerializeField]
    private GameObject[] predictSabotageItem;
    public Vector3[] predictInstantPos;
    public Vector3[] instantPos;
    private int predictNumber = 0;
    private int instantNumber = 0;
    private int instantCloneValue = 0;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bossDamage = GetComponent<BossDamage>();

        instantCloneValue = 0;
        predictNumber = 0;
        time = 0;

        switch (mySabotageType)
        {
            case SabotageType.Rubble:
                firstRubble = true;
                break;
            case SabotageType.Flame:
                firstRubble = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bossDamage.knockBackFlag)
        {
            time = 0;
        }

        if (firstRubble)
        {
            FirstRubbleAttack();
        }

        if (!sabotageFlag)
        {
            time += Time.deltaTime;

            if (time >= intervalTime)
            {
                time = 0;
                predictNumber = 0;
                alreadyInstantFlag = false;
                alreadyPredictFlag = false;
                sabotageFlag = true;
            
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
                        predictNumber = 0;
                    }

                    for (int i = 0; i < sabotageItem.Length - clonePredictItem.Length; i++)
                    {
                        float x = UnityEngine.Random.Range(rangeA.position.x, rangeB.position.x);
                        float z = UnityEngine.Random.Range(rangeA.position.z, rangeB.position.z);
                        predictInstantPos[predictNumber] = new Vector3(x, instancePosY, z);
                        Vector3 CheckPos = predictInstantPos[predictNumber];
                        CheckPos.y = PredictInstancePosY;
                        Quaternion predictRot = Quaternion.identity;
                        predictRot.y = 180f;

                        int layerMask = 1 << LayerMask;
                        layerMask = ~layerMask;

                        if (!Physics.CheckBox(CheckPos, halfExtents, predictRot, layerMask))
                        {
                            instantPos[predictNumber] = predictInstantPos[predictNumber];
                            Instantiate(predictSabotageItem[predictNumber], CheckPos, predictRot);
                            predictNumber++;
                        }

                        if (predictNumber >= instantPos.Length)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                PredictDestroy();

                if (!alreadyInstantFlag)
                {
                    if(instantCloneValue <= 0)
                    {
                        instantCloneValue = 0;
                    }
                    
                    for (int i = 0; i < sabotageItem.Length; i++)
                    {
                        RotateNumber = UnityEngine.Random.Range(-180f, 180f);
                        RotateY = Quaternion.Euler(0, RotateNumber, 0);
                        Instantiate(sabotageItem[i], instantPos[i], RotateY);
                        instantNumber++;
                    }

                    if (instantNumber >= sabotageItem.Length)
                    {
                        alreadyInstantFlag = true;
                    }
                }
            }

            switch (mySabotageType)
            {
                case SabotageType.Rubble:
                    #region
                    if (currentSabotageTime > sabotageTime)
                    {
                        time = 0;
                        instantCloneValue += sabotageItem.Length;
                        currentSabotageTime = 0;
                        sabotageFlag = false;
                    }
                    #endregion
                    break;
                case SabotageType.Flame:
                    #region
                    if (currentSabotageTime > sabotageTime)
                    {
                        AllDestroy();
                        time = 0;
                        currentSabotageTime = 0;
                        sabotageFlag = false;
                    }
                    #endregion
                    break;
            }

            
        }

    }

    void FirstRubbleAttack()
    {
        GameObject[] cloneFirstSabotage = GameObject.FindGameObjectsWithTag("CloneSabotageItem");
        if (cloneFirstSabotage.Length >= sabotageItem.Length)
        {
            predictNumber = 0;
            firstRubble = false;
        }

        for (int i = 0; i < sabotageItem.Length - cloneFirstSabotage.Length; i++)
        {
            float x = UnityEngine.Random.Range(rangeA.position.x, rangeB.position.x);
            float z = UnityEngine.Random.Range(rangeA.position.z, rangeB.position.z);
            predictInstantPos[predictNumber] = new Vector3(x, instancePosY, z);
            Vector3 CheckPos = predictInstantPos[predictNumber];
            CheckPos.y = PredictInstancePosY;
            RotateNumber = UnityEngine.Random.Range(-180f, 180f);
            RotateY = Quaternion.Euler(0, RotateNumber, 0);

            int layerMask = 1 << LayerMask;
            layerMask = ~layerMask;

            if (!Physics.CheckBox(CheckPos, halfExtents, RotateY, layerMask))
            {
                instantPos[predictNumber] = predictInstantPos[predictNumber];
                Instantiate(sabotageItem[predictNumber], CheckPos, RotateY);
                predictNumber++;
            }
            if (predictNumber >= instantPos.Length)
            {
                break;
            }
        }
    }

    void AllDestroy()
    {
        GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("CloneSabotageItem");

        foreach (GameObject clone_sabotageItem in cloneItem)
        {
            Destroy(clone_sabotageItem);
        }
    }

    void PredictDestroy()
    {
        GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("ClonePredictItem");

        foreach (GameObject clone_predictItem in cloneItem)
        {
            Destroy(clone_predictItem);
        }
    }

}
