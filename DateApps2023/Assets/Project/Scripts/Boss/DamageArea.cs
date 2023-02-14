using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public BossAttack bossAttack;
    float time = 0.0f;
    float destroyTime;

    private void Start()
    {
        destroyTime = bossAttack.BeamOffTimeMax();
    }
    void Update()
    {
        time += Time.deltaTime;
        if(time>=destroyTime)
        {
            Destroy(gameObject);
            time= 0.0f;
        }
    }
}
