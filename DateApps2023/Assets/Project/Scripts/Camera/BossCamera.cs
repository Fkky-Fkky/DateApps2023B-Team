using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossCamera : MonoBehaviour
{

    //private Camera cam;

    //cam = Camera.main;
    float a = 0.05f;
    float o = 0.05f;

    int camera_flag=0;

    //[SerializeField]
    //[Tooltip("メインカメラ")]
    //private GameObject Maincamera;
    [SerializeField]
    [Tooltip("ボスカメラ")]
    private Camera Bosscamera;

    float time = 0;



    // Start is called before the first frame update
    void Start()
    {
        //Maincamera = GameObject.Find("Main Camera");
        //Bosscamera = GameObject.Find("BossCamera");

        //サブカメラを非アクティブにする
       // Bosscamera.GetComponent<Camera>().enabled = false;
        Bosscamera.rect = new Rect(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //Bosscamera.SetActive(false);
        if (time >= 6)
        {
           Bosscamera.rect = new Rect(1.25f, 0.25f, 0.5f, 0.5f);


            //StartCoroutine(Camerachenge());
            //Bosscamera.rect = new Rect(0.25f, 0.25f, 0.5f, 0.5f);
        }
       
    }

    public void swith()
    {
        StartCoroutine(Camerachenge());
        camera_flag = 0;
        time = 0;
    }

    IEnumerator Camerachenge()
    {
       
        yield return new WaitForSeconds(0.2f);
        
        if (Bosscamera.rect.width < 0.5f)
        {
            Bosscamera.rect = new Rect(0.25f, 0.25f, 0 + a, 0.05f);
            if(Bosscamera.rect.width<0.5)
            a += 0.05f;
        }
        if(Bosscamera.rect.width >= 0.5f&&camera_flag==0)
        {
            Bosscamera.rect = new Rect(0.25f, 0.25f, 0.5f, 0.05f+o);
          
            if (Bosscamera.rect.height < 0.5)
                o += 0.05f;

            if (Bosscamera.rect.height==0.5f&&camera_flag==0)
            {
                camera_flag = 1;
            }
        }
            yield return null;
    }
}
