using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CreateRandomPosition : MonoBehaviour
{

    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Transform rangeA;
    [SerializeField]
    [Tooltip("生成する範囲B")]
    private Transform rangeB;

    public GameObject[] item;


    public int tower_bild_flag = 0;


    int tower_flag = 0;
    float time;

    private int number=0;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private BossCamera bosscamera;

    [SerializeField]
    private Transform smokePoint = null;

    [SerializeField]
    private GameObject smokeEffect = null;

    private bool BlastFlag = false;
    private float RotateNumber;
    private Quaternion RotateY;


    // Update is called once per frame
    void Update()
    {

        if (tower_flag == 1)
        {
            for (int i = 0; i < item.Length; i++)
            {
                // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
                float x = Random.Range(rangeA.position.x, rangeB.position.x);

                // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
                float z = Random.Range(rangeA.position.z, rangeB.position.z);

                RotateNumber = UnityEngine.Random.Range(-180f, 180f);
                RotateY = Quaternion.Euler(0, RotateNumber, 0);

                Instantiate(item[number], new Vector3(x, 51, z), RotateY);
                
                number += 1;
            }
            number = 0;
            tower_flag = 0;
        }


        if (tower_bild_flag == 0)
        {
            if (BlastFlag)
            {
                GameObject[] cloneItem = GameObject.FindGameObjectsWithTag("SmokeEffect");
                foreach (GameObject clone_smokeEffect in cloneItem)
                {
                    Destroy(clone_smokeEffect);
                }
                BlastFlag = false;
            }
        }

        if (time >= 0.5f)
        {
            if (!BlastFlag)
            {
                Instantiate(smokeEffect, smokePoint.position, Quaternion.identity);
                BlastFlag = true;
            }
            bosscamera.cameraSwitch();
            boss.GetComponent<BossDamage>().KnockbackTrue();
        }

        if (time > 2.0f)
        {
            tower_bild_flag = 0;
            tower_flag = 1;
            time = 0;
        }

        if(tower_bild_flag >= 4)
        {
            time += Time.deltaTime;
        }

        ////デバック用
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            tower_bild_flag += 1;
        }
    }
    public void Settower_bild_flag()
    {
        tower_bild_flag += 1;
    }
}
