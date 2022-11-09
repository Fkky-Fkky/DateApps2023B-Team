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

        if (!sabotageFlag)
        {
            time += Time.deltaTime;

            if (time > intervalTime)
            {
                time = 0;
                sabotageFlag = true;
                alreadyInstantFlag = false;
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
            }
            else
            {
                if (!alreadyInstantFlag)
                {
                    GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("CloneSabotageItem");
                    if (cloneItem.Length >= sabotageItem.Length)
                    {
                        alreadyInstantFlag = true;
                    }

                    for (int i = 0; i < sabotageItem.Length - cloneItem.Length; i++)
                    {
                        float x = Random.Range(rangeA.position.x, rangeB.position.x);
                        float z = Random.Range(rangeA.position.z, rangeB.position.z);
                        Vector3 instantPos = new Vector3(x, instancePosY, z);

                        int layerMask = 1 << LayerMask;
                        layerMask = ~layerMask;

                        if (!Physics.CheckBox(instantPos, halfExtents, Quaternion.identity, layerMask))
                        {
                            Instantiate(sabotageItem[i], instantPos, Quaternion.identity);
                        }
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

        void AllDestroy()
        {
            GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("CloneSabotageItem");

            foreach (GameObject clone_sabotageItem in cloneItem)
            {
                Destroy(clone_sabotageItem);
            }
        }

    }
}
