using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossMove : MonoBehaviour
{

    public int bossHp;

    private Rigidbody rb;

    private BossAttack bossAttack;
    private BossDamage bossDamage;
    private BossCSVGenerator bossCSVGenerator;
    private GameOver gameOver;

    [SerializeField]
    private float moveSpeed = 2.0f;

    [SerializeField]
    private GameObject cenetrTarget;
    [SerializeField]
    private GameObject leftTarget;
    [SerializeField]
    private GameObject rightTarget;



    [SerializeField] Animator AnimationImage = null;


    private bool damageFlag = false;

    private Camera camera;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject hpGauge;

    [SerializeField]
    private GameObject warningDisplay;

    private float underPos = -54.5f;

    private bool isAppearance = false;
    private bool isNotMove = false;

    private float moveTime    = 0.0f;
    private float moveTimeMax = 3.0f;

    [SerializeField]
    private GameObject shockWaveEffect;

    [SerializeField]
    private float Multiplier = 50.0f;

    [SerializeField]
    private bool isLastAttack = false;

    private float gameOverTime = 0.0f;
    private float gameOverTimeCenterMax = 7.0f;
    private float gameOverTimeSideMax = 6.5f;

    private bool isAttackOff = false;

    // Start is called before the first frame update

    private void Awake()
    {
        bossCSVGenerator = GameObject.Find("BossGenerator").GetComponent<BossCSVGenerator>();
        bossHp =  bossCSVGenerator.BossHP();

        if (bossHp > 9)
        {
            bossHp = 9;
        }

        moveSpeed = bossCSVGenerator.BossMoveSpeed();

        gameOver = GameObject.Find("TargetLine").GetComponent<GameOver>();
    }


    void Start()
    {

        if (transform.position.x == 0.0f)
        {
            tag = "Center";

            if (bossHp >= 3 && bossHp <= 8)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0.15f, 0);
            }
            if (bossHp == 1)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -0.15f, 0);
            }
            if (bossHp >= 9)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }

        }

        if (transform.position.x >= 0.1f)
        {
            tag = "Right";
            if (bossHp >= 3 && bossHp <= 8)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(2.8f, 0.15f, 0);
            }
            if (bossHp == 1)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(9, -0.15f, 0);
            }
            if (bossHp >= 9)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(4, 0, 0);
            }
        }


        if (transform.position.x <= -0.1f)
        {
            tag = "Left";
            if (bossHp >= 3 && bossHp <= 8)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-2.8f, 0.15f, 0.0f);
            }
            if (bossHp == 1)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-9, -0.15f, 0.0f);
            }
            if (bossHp >= 9)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-4, 0, 0);
            }


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
            if (!bossAttack.IsAttackAll())
            {
                Move();
            }
        }

        if (isLastAttack) {
            gameOverTime += Time.deltaTime;
            if ((gameObject.tag == "Right" || gameObject.tag == "Left") && gameOverTime >= gameOverTimeSideMax)
            {
                gameOver.GameOverTransition();
            }
            if(gameObject.tag== "Center" && gameOverTime >= gameOverTimeCenterMax)
            {
                gameOver.GameOverTransition();
            }

            if ((gameOverTime < gameOverTimeCenterMax || gameOverTime < gameOverTimeSideMax) && damageFlag)
            {
                gameOverTime = 0.0f;
                isLastAttack = false;
            }
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
            if (gameObject.tag=="Center")
            {
                MoveTarget(cenetrTarget);
            }

            if (gameObject.tag == "Left")
            {
                MoveTarget(leftTarget);
            }

            if (gameObject.tag == "Right")
            {
                MoveTarget(rightTarget);
            }

        }
    }

    private void MoveTarget(GameObject target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);

        lookRotation.z = 0;
        lookRotation.x = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

        Vector3 pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);


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
        else
        {
            isLastAttack = false;
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