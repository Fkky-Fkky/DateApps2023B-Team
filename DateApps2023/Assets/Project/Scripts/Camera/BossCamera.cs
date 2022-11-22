using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossCamera : MonoBehaviour
{
   

    [SerializeField]
    [Tooltip("���C���J����")]
    private GameObject Maincamera;
    [SerializeField]
    [Tooltip("�{�X�J����")]
    private GameObject Bosscamera;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Maincamera = GameObject.Find("Main Camera");
        Bosscamera = GameObject.Find("BossCamera");

        //�T�u�J�������A�N�e�B�u�ɂ���
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
