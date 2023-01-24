using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossMove : MonoBehaviour
{
    private Rigidbody rb;


    [SerializeField]
    int bossHp;

    public BossAttack bossAttack;
    public BossCount bossCount;

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
        if(!damageFlag)
        {
            Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);

            lookRotation.z = 0;
            lookRotation.x = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

            transform.position = Vector3.MoveTowards(
                transform.position,
                target.transform.position,
                moveSpeed * Time.deltaTime
                );
        }
        else
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }

        if(Input.GetMouseButtonDown(1)) {
            bossHp -= 1;
        }

        if (bossHp < 0)
        {
            bossCount.bossKillCount++;
            Destroy(gameObject.gameObject);
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
