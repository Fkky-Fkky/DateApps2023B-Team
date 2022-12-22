using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossMove : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    [Tooltip("ƒ{ƒXˆÚ“®‘¬“x")]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private GameObject target;

    private bool damageFlag = false; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity -= new Vector3(0.0f, 0.0f, moveSpeed * Time.deltaTime);
        if(!damageFlag)
        {
            Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);

            lookRotation.z = 0;
            lookRotation.x = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

            Vector3 p = new Vector3(0f, 0f, moveSpeed * Time.deltaTime);

            transform.Translate(p);
        }
        else
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    public void DamageTrue()
    {
        damageFlag = true;
    }

    public void DamageFalse()
    {
        damageFlag = false;
    }
}
