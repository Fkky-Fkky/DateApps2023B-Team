using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    //ParticleSystem型を変数psで宣言します。
    public ParticleSystem ps;
    //GameObject型で変数objを宣言します。
    GameObject obj;

    [SerializeField]
    [Tooltip("生成する範囲A")]
    private GameObject[] item;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("BigExplosion");
        //GetComponentInChildrenで子要素も含めた
        //ParticleSystemにアクセスして変数psで参照します。
        ps = obj.GetComponentInChildren<ParticleSystem>();
        //変数objを非表示にしてパーティクルの再生を止めます。
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
