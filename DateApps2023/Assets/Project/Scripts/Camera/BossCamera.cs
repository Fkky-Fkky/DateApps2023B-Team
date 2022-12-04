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
    float y = 0.5f;

    int camera_flag=0;

    //[SerializeField]
    //[Tooltip("メインカメラ")]
    //private GameObject Maincamera;
    [SerializeField]
    [Tooltip("ボスカメラ")]
    private Camera Bosscamera;

    [SerializeField]
    [Tooltip("モニター")]
    private GameObject monita;

    float time = 0;



    // Start is called before the first frame update
    void Start()
    {
        //Maincamera = GameObject.Find("Main Camera");
        //Bosscamera = GameObject.Find("BossCamera");

        //サブカメラを非アクティブにする
       // Bosscamera.GetComponent<Camera>().enabled = false;
        Bosscamera.rect = new Rect(0, 0, 0, 0);
        monita.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;

        if (time >= 5)
        {
            Bosscamera.rect = new Rect(0, 0, 0, 0);
            monita.SetActive(false);
            a = 0.05f;
            o = 0.05f;
             y = 0.5f;

        }

        //Bosscamera.SetActive(false);
        //if (time >= 5 && camera_flag == 1)
        //{
        //    camera_flag = 2;
        //    StartCoroutine(Camerachengeout());
        //}

    }

    public void swith()
    {
        
        camera_flag = 0;
        time = 0;
        StartCoroutine(Camerachenge());
        monita.SetActive(true);
        
    }

    IEnumerator Camerachenge()
    {
       
        yield return new WaitForSeconds(0.5f);
        
        if (Bosscamera.rect.width < 0.5f)
        {
            Bosscamera.rect = new Rect(0.25f, 0.5f, 0 + a, 0.05f);
            
            a += 0.05f;
        }
        if(Bosscamera.rect.width >= 0.5f&&camera_flag==0)
        {
            Bosscamera.rect = new Rect(0.25f, y, 0.5f, 0.05f+o);

            if (Bosscamera.rect.height < 0.5)
            { 
                o += 0.05f;
                y -= 0.03f;
            }
               
            //if (Bosscamera.rect.height>=0.5f&&camera_flag==0)
            //{
            //    camera_flag = 1;
                
            //}
        }
        yield return null;
    }

    IEnumerator Camerachengeout()
    {
        Debug.Log("a");

        yield return new WaitForSeconds(0.5f);

        if (Bosscamera.rect.height <= 0.6f )
        {
            Bosscamera.rect = new Rect(0.25f, y, 0.5f, 0.05f + o);//
            Debug.Log("i");
            o -= 0.05f;
            y += 0.03f;
        }

        if (Bosscamera.rect.height <= 0.05f)
        {
            Debug.Log("u");
            Bosscamera.rect = new Rect(0.25f, 0.25f, 0 + a, 0.5f);

            if (Bosscamera.rect.height < 0.5)
                a -= 0.05f;


            if (camera_flag == 2)
            {
                Debug.Log("e");
                camera_flag = 0;
                a = 0.05f;
                o = 0.05f;
                y = 0.5f;
                monita.SetActive(false);
            }
        }
        yield return null;
    }


}
