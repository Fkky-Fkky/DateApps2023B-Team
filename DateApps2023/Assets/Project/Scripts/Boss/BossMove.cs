using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossMove : MonoBehaviour
{
    private Rigidbody rb;


    [SerializeField]
    public float bossHp;

    private BossAttack bossAttack;
    private BossDamage bossDamage;

    [SerializeField]
    [Tooltip("ƒ{ƒXˆÚ“®‘¬“x")]
    private float moveSpeed = 2.0f;

    [SerializeField]
    private GameObject target;

    private bool damageFlag = false;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (transform.position.x == 0.0f)
        {
            tag = "Center";
        }

        if (transform.position.x >= 0.1f)
        {
            tag = "Right";
        }

        if (transform.position.x <= -0.1f)
        {
            tag = "Left";
        }

        bossAttack = GetComponent<BossAttack>();
        bossDamage = GetComponent<BossDamage>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!damageFlag && !bossDamage.isTrance)
        {
            if (!bossAttack.isAttack)
            {
                Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);

                lookRotation.z = 0;
                lookRotation.x = 0;

                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

                Vector3 pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    pos,
                    moveSpeed * Time.deltaTime
                    );
            }
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
