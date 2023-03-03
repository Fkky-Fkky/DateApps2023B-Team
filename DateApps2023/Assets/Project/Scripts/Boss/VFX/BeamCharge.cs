using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCharge : MonoBehaviour
{
    public BossAttack bossAttack = null;

    private float time = 0.0f;
    private float effectTime = 0.0f;

    void Start()
    {
        time = 0.0f;

        effectTime = bossAttack.BeamTimeMax();

    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > effectTime)
        {
            Destroy(gameObject);
            time = 0.0f;
        }
    }
}