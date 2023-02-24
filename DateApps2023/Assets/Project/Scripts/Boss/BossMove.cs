using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    [SerializeField]
    private GameObject gameOverWarningDisplay;

    [SerializeField]
    private Renderer gameOverDisplay;

    private float flashTime = 0.0f;
    [SerializeField]
    private float flashTimeMax = 0.5f;

    [SerializeField]
    private AudioSource audioSource;

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
    public bool IsLanding { get; private set; }

    public bool isHazard { get; private set; }

    private int messageCount = 0;

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


        audioSource.Stop();
    }


    void Start()
    {

        if (transform.position.x == 0.0f)
        {
            tag = "Center";

            if (bossHp >= 2 && bossHp <= 5)
            {
                if (gameObject.transform.localScale.y > 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0.055f, 0);
                }
            }
            if (bossHp >= 1 && bossHp <= 2)
            {
                if (gameObject.transform.localScale.y < 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -0.5f, 0);
                }
            }
            if (bossHp >= 7 && bossHp <= 9)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }

        }

        if (transform.position.x >= 0.1f)
        {
            tag = "Right";
            if (bossHp >= 2 && bossHp <= 5)
            {
                if (gameObject.transform.localScale.y > 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(2.9f, 0.055f, 0);
                }
            }
            if (bossHp >= 1 && bossHp <= 2)
            {
                if (gameObject.transform.localScale.y < 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(8.6f, -0.5f, 0);
                }
            }
            if (bossHp >= 7 && bossHp <= 9)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(4, 0, 0);
            }
        }


        if (transform.position.x <= -0.1f)
        {
            tag = "Left";
            if (bossHp >= 2 && bossHp <= 5)
            {
                if (gameObject.transform.localScale.y > 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-2.9f, 0.055f, 0.0f);
                }
            }
            if (bossHp >= 1 && bossHp <= 2)
            {
                if (gameObject.transform.localScale.y < 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-8.6f, -0.5f, 0.0f);
                }
            }
            if (bossHp >= 7 && bossHp <= 9)
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
        gameOverWarningDisplay.SetActive(false);

        isAppearance = true;
        isNotMove = true;


        isLastAttack = false;
        isAttackOff = false;

        IsLanding = false;
        isHazard = false;

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
                IsLanding = true;
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

        if (isLastAttack)
        {
            gameOverWarningDisplay.SetActive(true);
            flashTime += Time.deltaTime;

            var repeatValue = Mathf.Repeat(flashTime, flashTimeMax);

            gameOverDisplay.enabled = repeatValue >= flashTimeMax * 0.5f;

            if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("LastAttack") && AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                gameOver.GameOverTransition();
            }
            if ((gameOverTime < gameOverTimeCenterMax || gameOverTime < gameOverTimeSideMax) && damageFlag)
            {
                gameOverTime = 0.0f;
                isLastAttack = false;
            }
        }

        if (isHazard)
        {
            audioSource.Play();
        }

        if (damageFlag)
        {
            audioSource.Stop();
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
            if (messageCount == 0)
            {
                isHazard = true;
                messageCount++;
            }
            else
            {
                isHazard = false;
            }
        }
        else if ((transform.position.z - target.transform.position.z) > 100.0f)
        {
            warningDisplay.SetActive(false);
            isHazard = false;
            messageCount = 0;

        }


        if ((transform.position.z - target.transform.position.z) <= 50.0f)
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
            gameOverWarningDisplay.SetActive(false);
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