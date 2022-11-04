using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private bool knockBackFlag = false;

    [SerializeField]
    [Tooltip("mä∑éZÅH")]
    private float knockBackPower = 300.0f;

    [SerializeField]
    private float stopTime = 5.0f;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!knockBackFlag)
        {
            time += Time.deltaTime;
            if (time <= stopTime)
            {
                boss.transform.position = new Vector3(
                    0.0f,
                    boss.transform.position.y,
                    boss.transform.position.z);
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        else
        {
            time = 0;

            boss.transform.position = new Vector3(
                0.0f,
                boss.transform.position.y,
                boss.transform.position.z + knockBackPower * Time.deltaTime);

        }
    }


    public void KnockbackTrue()
    {
        knockBackFlag = true;
    }

    public void KnockbackFalse()
    {

        knockBackFlag = false;

    }
}
