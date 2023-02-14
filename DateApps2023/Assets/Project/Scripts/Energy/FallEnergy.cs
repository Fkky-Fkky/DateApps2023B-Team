using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEnergy : MonoBehaviour
{
    private bool isLand = false;
    private Vector3 position = Vector3.zero;

    private const float FALL_SPEED = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
       position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLand)
        {
            return;
        }

        position.y = Mathf.Max(position.y - FALL_SPEED * Time.deltaTime, 0.0f);

        transform.position = position;
        if(transform.position.y <= 0.0f)
        {
            isLand = true;
        }
    }
}
