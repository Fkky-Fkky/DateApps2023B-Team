using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCharge : MonoBehaviour
{
    private float time = 0.0f;
    private float effectTime = 2.0f;

    void Start()
    {
        time = 0.0f;
        effectTime = 2.0f;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > effectTime) {
            Destroy(gameObject);
            time = 0.0f;
        }
    }
}
