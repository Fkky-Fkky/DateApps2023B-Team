using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public BossAttack bossAttack;

    private float ereaTime = 0.0f;
    private float ereaTimeMax;

    void Start()
    {
        ereaTime = 0.0f;
        ereaTimeMax = bossAttack.BeamTimeMax();
    }

    // Update is called once per frame
    void Update()
    {
        ereaTime += Time.deltaTime;
        if (ereaTime >= ereaTimeMax)
        {
            Destroy(gameObject);
            ereaTime = 0.0f;
        }
    }
}
