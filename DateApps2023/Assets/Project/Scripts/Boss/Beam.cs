using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    [SerializeField]
    float beamSpeed;

    float centerTarget = 0.0f;
    float rightTarget = 0.1f;
    float leftTarget = -0.1f;

    bool isDestroy;
    float time = 0.0f;
    float destroyTime = 0.5f;


    [SerializeField]
    GameObject targetC;

    [SerializeField]
    GameObject targetR;

    [SerializeField]
    GameObject targetL;

    


    private void Start()
    {
        targetC.SetActive(false);
        isDestroy = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x == centerTarget)
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                targetC.transform.position,
                beamSpeed * Time.deltaTime
                );
        }

        if (gameObject.transform.position.x >= rightTarget)
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                targetR.transform.position,
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



        BeamDestroy();
    }


    private void BeamDestroy()
    {
        if (gameObject.transform.position == targetC.transform.position ||
            gameObject.transform.position == targetR.transform.position ||
            gameObject.transform.position == targetL.transform.position)
        {
            isDestroy= true;
        }

        if(isDestroy )
        {
            time += Time.deltaTime;
            if (time > destroyTime)
            {

                Destroy(gameObject);
                isDestroy= false;
                time= 0;
            }
        }

    }
}
