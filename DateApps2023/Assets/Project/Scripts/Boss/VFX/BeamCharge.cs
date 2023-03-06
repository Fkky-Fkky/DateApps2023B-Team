using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCharge : MonoBehaviour
{
    private float time       = 0.0f;
    private float effectTime = 0.0f;

    public BossAttack BossAttack = null;

    void Start()
    {
        time = 0.0f;

        effectTime = BossAttack.BeamTimeMax();

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