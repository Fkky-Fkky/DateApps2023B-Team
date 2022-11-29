using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreateRandomPosition : MonoBehaviour
{
    //[SerializeField]
    //[Tooltip("生成する1オブジェクト")]
    //private GameObject createPrefab1;

    public GameObject[] CubePrefabs;

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

    int a = 0;
    int i = 0;
    int u = 0;

    public GameObject boss;

    /// <ここから>
    [SerializeField]
    private BossCamera bosscamera;
    ///
    ///steat
    ///
    ///
    ///Update
    //
    /// <ここまで>
    void Start()
    {
        boss = GameObject.Find("Boss");        
    }

    // Update is called once per frame
    void Update()
    {

        if (tower_flag == 1)
        {
            for (int i = 0; i < 4; i++)
            { 
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
       
            // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

           
            Instantiate(item[number], new Vector3(x, 55, z), CubePrefabs[number].transform.rotation);
                number += 1;
                // GameObjectを上記で決まったランダムな場所に生成
                //Instantiate(createPrefab, new Vector3(x, 1, z), createPrefab.transform.rotation);
            }
            number = 0;
            tower_flag = 0;
        }

        for (int i = 0; i < 4; i++)
        {
            if (tower_bild_flag == 0)
                CubePrefabs[i].SetActive(false);
        }


        if (tower_bild_flag == 1)
            CubePrefabs[0].SetActive(true);
        if (tower_bild_flag == 2 && CubePrefabs[0].activeSelf == true || a == 1&&CubePrefabs[0].activeSelf==true )
            CubePrefabs[1].SetActive(true);
        else if (tower_bild_flag == 2 && CubePrefabs[0].activeSelf == false)
            a = 1;

        if (tower_bild_flag == 3 && CubePrefabs[0].activeSelf == true && CubePrefabs[1].activeSelf == true 
            || i == 1 && CubePrefabs[0].activeSelf==true && CubePrefabs[1].activeSelf == true)
            CubePrefabs[2].SetActive(true);

        else if (tower_bild_flag == 3 && CubePrefabs[0].activeSelf == false && CubePrefabs[1].activeSelf == false
            || tower_bild_flag == 3 && CubePrefabs[0].activeSelf == true && CubePrefabs[1].activeSelf == false)
            i = 1;

        if (tower_bild_flag >= 4 && CubePrefabs[0].activeSelf == true && CubePrefabs[1].activeSelf == true && CubePrefabs[2].activeSelf == true
            ||u==1 && CubePrefabs[0].activeSelf == true && CubePrefabs[1].activeSelf == true && CubePrefabs[2].activeSelf == true)
        {
            CubePrefabs[3].SetActive(true);

            time += Time.deltaTime;
            a = 0;
            i = 0;
            u = 0;

        }
        else if (tower_bild_flag >= 4 && CubePrefabs[0].activeSelf == true && CubePrefabs[1].activeSelf == true && CubePrefabs[2].activeSelf == false|| tower_bild_flag >= 4 && CubePrefabs[0].activeSelf == true && CubePrefabs[1].activeSelf == false && CubePrefabs[2].activeSelf == false|| tower_bild_flag >= 4 && CubePrefabs[0].activeSelf == false && CubePrefabs[1].activeSelf == false && CubePrefabs[2].activeSelf == false)
            u = 1;
        if(time>=0.01)
        {
            //bosscamera.StartCoroutine(Camerachenge());
            bosscamera.swith();
        }
        if(time >= 0.5f)
        {
            boss.GetComponent<BossDamage>().KnockbackTrue();
        }

        if (time > 2.0f)
        {
            tower_bild_flag = 0;
            tower_flag = 1;
            time = 0;
            boss.GetComponent<BossDamage>().KnockbackFalse();

            CubePrefabs[0].SetActive(false);
            CubePrefabs[1].SetActive(false);
            CubePrefabs[2].SetActive(false);
            CubePrefabs[3].SetActive(false);

        }

        if (CubePrefabs[0].activeSelf == true && CubePrefabs[1].activeSelf == true && CubePrefabs[2].activeSelf == true && CubePrefabs[3].activeSelf == true)
            tower_bild_flag = 4;

        //デバック用
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            tower_bild_flag += 1;
        }
    }
    public void Settower_bild_flag()
    {
        tower_bild_flag = 1;
    }

    public void Settower_bild_flag2()
    {
        tower_bild_flag = 2;
    }

    public void Settower_bild_flag3()
    {
        tower_bild_flag = 3;
    }
    public void Settower_bild_flag4()
    {
        tower_bild_flag = 4;
    }
}
