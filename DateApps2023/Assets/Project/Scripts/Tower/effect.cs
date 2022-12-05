using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    //ParticleSystem�^��ϐ�ps�Ő錾���܂��B
    public ParticleSystem ps;
    //GameObject�^�ŕϐ�obj��錾���܂��B
    GameObject obj;

    [SerializeField]
    [Tooltip("��������͈�A")]
    private GameObject[] item;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("BigExplosion");
        //GetComponentInChildren�Ŏq�v�f���܂߂�
        //ParticleSystem�ɃA�N�Z�X���ĕϐ�ps�ŎQ�Ƃ��܂��B
        ps = obj.GetComponentInChildren<ParticleSystem>();
        //�ϐ�obj���\���ɂ��ăp�[�e�B�N���̍Đ����~�߂܂��B
        obj.SetActive(false);
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explosioneffect()
    {
        obj.SetActive(true);
        ps.Play();
    }
}
