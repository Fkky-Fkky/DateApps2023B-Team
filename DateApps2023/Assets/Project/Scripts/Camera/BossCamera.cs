using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossCamera : MonoBehaviour
{
   

    [SerializeField]
    [Tooltip("メインカメラ")]
    private GameObject Maincamera;
    [SerializeField]
    [Tooltip("ボスカメラ")]
    private GameObject Bosscamera;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Maincamera = GameObject.Find("Main Camera");
        Bosscamera = GameObject.Find("BossCamera");

        //サブカメラを非アクティブにする
        Bosscamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 3)
            Bosscamera.SetActive(false);
        
    }

    public void Camerachenge()
    {
        Bosscamera.SetActive(true);

        time = 0;
    }
}
