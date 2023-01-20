using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    float time = 0.0f;

    [SerializeField]
    float attackIntervalTime = 20.0f;

    [SerializeField]
    GameObject beamObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time+= Time.deltaTime;
        if (time >= attackIntervalTime)
        {
            Vector3 pos= this.transform.position;
            pos.y = 0;
            Instantiate(beamObject, pos, Quaternion.identity);

            time= 0.0f;
        }
    }
}
