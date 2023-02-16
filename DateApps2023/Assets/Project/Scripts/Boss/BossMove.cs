using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossMove : MonoBehaviour
{

    [SerializeField]
    public float bossHp;

    private Rigidbody rb;

    private BossAttack bossAttack;
    private BossDamage bossDamage;

    [SerializeField]
    private float moveSpeed = 2.0f;

    [SerializeField]
    private GameObject target;

    [SerializeField] Animator AnimationImage = null;


    private bool damageFlag = false;

    private Camera camera;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject warningDisplay;

    private float underPos = -54.5f;

    private bool isAppearance = false;
    private bool isNotMove = false;

    private float moveTime    = 0.0f;
    private float moveTimeMax = 2.0f;

    [SerializeField]
    private GameObject shockWaveEffect;

    [SerializeField]
    private float Multiplier = 50.0f;

    private bool isLastAttack = false;

    private bool isAttackOff = false;

    // Start is called before the first frame update
    void Start()
    {

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
        rb = GetComponent<Rigidbody>();

        camera = Camera.main;
        canvas.worldCamera = camera;

        warningDisplay.SetActive(false);

        isAppearance = true;
        isNotMove = true;


        isLastAttack = false;
        isAttackOff = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isAppearance)
        {
            Vector3 pos = transform.position;

            if (transform.position.y <= underPos)
            {
                Instantiate(shockWaveEffect, gameObject.transform.position, Quaternion.identity);
                pos.y = underPos;
                transform.position = pos;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                AnimationImage.SetTrigger("StandBy");
                isNotMove = true;
                isAppearance = false;
            }
        }

        if (isNotMove)
        {
            moveTime += Time.deltaTime;
            if (moveTime >= moveTimeMax)
            {
                AnimationImage.SetTrigger("Walk");
                isNotMove = false;
                moveTime = 0.0f;
            }
        }


        if (!damageFlag && !bossDamage.isTrance)
        {
            if (!bossAttack.isAttack)
            {
                Move();
            }
        }

        if ((transform.position.z - target.transform.position.z) <= 100.0f)
        {
            warningDisplay.SetActive(true);
        }

        if ((transform.position.z - target.transform.position.z) <= 60.0f)
        {
            isAttackOff = true;
        }

        if ((transform.position.z - target.transform.position.z) <= 40.0f)
        {
            warningDisplay.SetActive(false);
            isLastAttack = true;
            AnimationImage.SetTrigger("LastAttack");
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce((Multiplier - 1f) * Physics.gravity, ForceMode.Acceleration);
    }

    private void Move()
    {
        if (!isAppearance && !isNotMove && !isLastAttack)
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

    public void DamageTrue()
    {
        damageFlag = true;
    }

    public void DamageFalse()
    {
        damageFlag = false;
    }

    public bool IsAppearance()
    {
        return isAppearance;
    }

    public bool IsAttackOff()
    {
        return isAttackOff;
    }
}