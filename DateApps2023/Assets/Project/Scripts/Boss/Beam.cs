using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    [SerializeField]
    float beamSpeed;


    [SerializeField]
    GameObject tagetC;

    [SerializeField]
    GameObject tagetR;

    [SerializeField]
    GameObject targetL;

    float centerTarget =  0.0f;
    float rightTarget  =  0.1f;
    float leftTarget   = -0.1f;



    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x == centerTarget)
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                tagetC.transform.position,
                beamSpeed * Time.deltaTime
                );
        }

        if (gameObject.transform.position.x >= rightTarget)
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                tagetR.transform.position,
                beamSpeed * Time.deltaTime
                );
        }

        if (gameObject.transform.position.x <= leftTarget)
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                targetL.transform.position,
                beamSpeed * Time.deltaTime
                );
        }

    }
}
