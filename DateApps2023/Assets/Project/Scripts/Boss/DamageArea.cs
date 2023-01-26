using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    float time = 0.0f;
    float destroyTime = 0.5f;
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
