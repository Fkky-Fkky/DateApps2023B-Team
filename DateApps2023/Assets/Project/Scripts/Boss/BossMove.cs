using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;

    [SerializeField]
    private GameObject cenetrTarget = null;
    [SerializeField]
    private GameObject leftTarget   = null;
    [SerializeField]
    private GameObject rightTarget  = null;

    [SerializeField]
    private Animator AnimationImage = null;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private GameObject hpGauge = null;

    [SerializeField]
    private GameObject warningDisplay = null;

    [SerializeField]
    private Renderer warningRenderer = null;

    [SerializeField]
    private GameObject gameOverWarningDisplay = null;

    [SerializeField]
    private Renderer gameOverDisplay = null;

    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private GameObject shockWaveEffect = null;

    [SerializeField]
    private float Multiplier = 50.0f;

    [SerializeField]
    private bool isLastAttack = false;

    [SerializeField]
    private AudioSource lastAttackAudio = null;
    [SerializeField]
    private AudioClip lastAttackSE      = null;


    public int BossHp = 0;

    private Rigidbody rb = null;

    private bool damageFlag = false;

    private float warningFlashTime          = 0.0f;
    private const float warningFlashTimeMax = 1.0f;

    private float flashTime          = 0.0f;
    private const float flashTimeMax = 0.5f;

    private AudioClip dangerSE = null;
    private string songName    = null;

    private const float underPos = -54.5f;

    private bool isAppearance = false;
    private bool isNotMove    = false;

    private float moveTime          = 0.0f;
    private const float moveTimeMax = 3.0f;

    public bool IsGameOver { get; private set; }

    private bool isAttackOff = false;
    public bool IsLanding { get; private set; }

    public bool IsHazard { get; private set; }

    private int messageCount = 0;

    private int seCount = 0;

    private BossAttack bossAttack = null;
    private BossCSVGenerator bossCSVGenerator = null;
    private Camera camera = null;

    private void Awake()
    {
        bossCSVGenerator = GameObject.Find("BossGenerator").GetComponent<BossCSVGenerator>();
        BossHp =  bossCSVGenerator.BossHP();

        if (BossHp > 9)
        {
            BossHp = 9;
        }

        moveSpeed = bossCSVGenerator.BossMoveSpeed();

        songName = "FirstDanger";
        dangerSE = (AudioClip)Resources.Load("Sound/" + songName);
        audioSource.clip = dangerSE;
        audioSource.Stop();
    }


    void Start()
    {

        if (transform.position.x == 0.0f)
        {
            tag = "Center";

            if (BossHp >= 1 && BossHp <= 5)
            {
                if (gameObject.transform.localScale.y > 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0.1f, 0);
                }
            }
            if (BossHp >= 1 && BossHp <= 2)
            {
                if (gameObject.transform.localScale.y < 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -0.7f, 0);
                }
            }
            if (BossHp >= 7 && BossHp <= 9)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }

        }

        if (transform.position.x >= 0.1f)
        {
            tag = "Right";
            if (BossHp >= 1 && BossHp <= 5)
            {
                if (gameObject.transform.localScale.y > 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(4.0f, 0.05f, 0);
                }
            }
            if (BossHp >= 1 && BossHp <= 2)
            {
                if (gameObject.transform.localScale.y < 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(13.0f, -0.7f, 0);
                }
            }
            if (BossHp >= 7 && BossHp <= 9)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(4.2f, 0, 0);
            }
        }


        if (transform.position.x <= -0.1f)
        {
            tag = "Left";
            if (BossHp >= 1 && BossHp <= 5)
            {
                if (gameObject.transform.localScale.y > 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-4.0f, 0.05f, 0.0f);
                }
            }
            if (BossHp >= 1 && BossHp <= 2)
            {
                if (gameObject.transform.localScale.y < 18.0f)
                {
                    hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-13.0f, -0.7f, 0.0f);
                }
            }
            if (BossHp >= 7 && BossHp <= 9)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-4.2f, 0, 0);
            }
        }

        bossAttack = GetComponent<BossAttack>();
        rb = GetComponent<Rigidbody>();

        camera = Camera.main;
        canvas.worldCamera = camera;

        warningDisplay.SetActive(false);
        gameOverWarningDisplay.SetActive(false);


        isLastAttack = false;
        isAttackOff = false;

        IsLanding = false;
        IsHazard = false;

        IsGameOver = false;
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

        if (!damageFlag)
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

            if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("LastAttack") && AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0f)
            {
                if (seCount < 1)
                {
                    lastAttackAudio.PlayOneShot(lastAttackSE);
                    seCount++;
                }
            }

            if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("LastAttack") && AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
            {
                //ゲームオーバーフラグ
                IsGameOver = true;
            }
            if (isLastAttack && damageFlag)
            {
                audioSource.Stop();
                seCount = 0;
                isLastAttack = false;
            }
        }

        if (IsHazard)
        {
            songName = "FirstDanger";
            dangerSE = (AudioClip)Resources.Load("Sound/" + songName);
            audioSource.clip = dangerSE;
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
            if (gameObject.tag == "Center")
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

            warningFlashTime += Time.deltaTime;
            var repeatValue = Mathf.Repeat(warningFlashTime, warningFlashTimeMax);

            warningRenderer.enabled = repeatValue >= warningFlashTimeMax * 0.5f;

            if (messageCount == 0)
            {
                IsHazard = true;
                messageCount++;
            }
            else
            {
                IsHazard = false;
            }
        }
        else if ((transform.position.z - target.transform.position.z) > 100.0f)
        {
            warningDisplay.SetActive(false);
            IsHazard = false;
            messageCount = 0;
        }

        if ((transform.position.z - target.transform.position.z) <= 50.0f)
        {
            isAttackOff = true;
        }

        if ((transform.position.z - target.transform.position.z) <= 40.0f)
        {
            warningDisplay.SetActive(false);

            songName = "SecondDanger";
            dangerSE = (AudioClip)Resources.Load("Sound/" + songName);
            audioSource.clip = dangerSE;
            audioSource.Play();

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