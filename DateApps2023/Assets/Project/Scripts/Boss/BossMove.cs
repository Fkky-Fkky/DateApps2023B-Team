using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed  =  2.0f;
    [SerializeField]
    private float Multiplier = 50.0f;

    [SerializeField]
    private GameObject cenetrTarget = null;
    [SerializeField]
    private GameObject leftTarget   = null;
    [SerializeField]
    private GameObject rightTarget  = null;

    [SerializeField]
    private GameObject hpGauge                = null;
    [SerializeField]
    private GameObject warningDisplay         = null;
    [SerializeField]
    private GameObject gameOverWarningDisplay = null;

    [SerializeField]
    private GameObject shockWaveEffect = null;

    [SerializeField]
    private Animator AnimationImage = null;

    [SerializeField]
    private Canvas canvas            = null;
    [SerializeField]
    private Renderer warningRenderer = null;
    [SerializeField]
    private Renderer gameOverDisplay = null;

    [SerializeField]
    private AudioSource audioSource     = null;
    [SerializeField]
    private AudioSource lastAttackAudio = null;
    [SerializeField]
    private AudioClip lastAttackSE      = null;

    private bool isDamageFlag = false;
    private bool isAppearance = true;
    private bool isNotMove    = false;
    private bool isAttackOff  = false;
    private bool isLastAttack = false;

    private int seCount      = 0;
    private int messageCount = 0;

    private float warningFlashTime = 0.0f;
    private float dangerFlashTime  = 0.0f;
    private float moveTime         = 0.0f;

    private string songName = null;

    private Camera camera      = null;
    private AudioClip dangerSE = null;
    private Rigidbody rb       = null;

    private BossCSVGenerator bossCSVGenerator = null;
    private BossAttack bossAttack             = null;

    public int BossHp = 0;
    public bool IsGameOver { get; private set; }
    public bool IsLanding { get; private set; }
    public bool IsHazard { get; private set; }

    const int MIN_HP       = 1;
    const int SMALE_MAX_HP = 2;
    const int NOMAL_MAX_HP = 5;
    const int BIG_MIN_HP   = 7;
    const int MAX_HP       = 9;
    const float UNDER_POSITION         = -54.5f;
    const float DANGER_FLASH_TIME_MAX  =   0.5f;
    const float WARNING_FLASH_TIME_MAX =   1.0f;
    const float MOVE_TIME_MAX          =   3.0f;
    const float BOSS_SCALE_Y           =  18.0f;

    const float NOMAL_UI_POSITION_X =  4.0f;
    const float MINI_UI_POSITION_X  = 13.0f;
    const float BIG_UI_POSITION_X   =  4.2f;

    const float NOMAL_CENETR_UI_POSITION_Y =  0.1f;
    const float NOMAL_SIDE_UI_POSITION_Y   = 0.05f;
    const float MINI_UI_POSITION_Y         = -0.7f;

    const float WARNING_DISPLAY_POSITION = 100.0f;
    const float ATTACK_OFF_POSITION      =  50.0f;
    const float DANGER_DISPLAY_POSITION  =  40.0f;

    const float GAME_OVER_TIME = 0.6f;

    private void Awake()
    {
        bossCSVGenerator = GameObject.Find("BossGenerator").GetComponent<BossCSVGenerator>();
        BossHp =  bossCSVGenerator.BossHP();

        if (BossHp > MAX_HP)
        {
            BossHp = MAX_HP;
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
            BossUIPositionCneter();

        }

        if (transform.position.x >= 0.1f)
        {
            tag = "Right";
            BossUIPositionRight();
        }


        if (transform.position.x <= -0.1f)
        {
            tag = "Left";
            BossUIPositionLeft();
        }

        bossAttack = GetComponent<BossAttack>();
        rb = GetComponent<Rigidbody>();

        camera = Camera.main;
        canvas.worldCamera = camera;

        warningDisplay.SetActive(false);
        gameOverWarningDisplay.SetActive(false);


        IsLanding    = false;
        IsHazard     = false;
        IsGameOver   = false;
    }

    void Update()
    {
        if (isAppearance)
        {
            Vector3 pos = transform.position;

            if (transform.position.y <= UNDER_POSITION)
            {
                Instantiate(shockWaveEffect, gameObject.transform.position, Quaternion.identity);
                IsLanding = true;
                pos.y = UNDER_POSITION;
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
            if (moveTime >= MOVE_TIME_MAX)
            {
                AnimationImage.SetTrigger("Walk");
                isNotMove = false;
                moveTime = 0.0f;
            }
        }

        if (!isDamageFlag)
        {
            if (!bossAttack.IsAttackAll())
            {
                Move();
            }
        }

        if (isLastAttack)
        {
            gameOverWarningDisplay.SetActive(true);
            dangerFlashTime += Time.deltaTime;

            var repeatValue = Mathf.Repeat(dangerFlashTime, DANGER_FLASH_TIME_MAX);

            gameOverDisplay.enabled = repeatValue >= DANGER_FLASH_TIME_MAX * 0.5f;

            GameOverAnimasiton();
        }

        if (IsHazard)
        {
            songName = "FirstDanger";
            dangerSE = (AudioClip)Resources.Load("Sound/" + songName);
            audioSource.clip = dangerSE;
            audioSource.Play();
        }

        if (isDamageFlag)
        {
            audioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce((Multiplier - 1f) * Physics.gravity, ForceMode.Acceleration);
    }

    private void BossUIPositionCneter()
    {
        if (BossHp >= MIN_HP && BossHp <= NOMAL_MAX_HP)
        {
            if (gameObject.transform.localScale.y > BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, NOMAL_CENETR_UI_POSITION_Y, 0);
            }
        }
        if (BossHp >= MIN_HP && BossHp <= SMALE_MAX_HP)
        {
            if (gameObject.transform.localScale.y < BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, MINI_UI_POSITION_Y, 0);
            }
        }
        if (BossHp >= BIG_MIN_HP && BossHp <= MAX_HP)
        {
            hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }

    }

    private void BossUIPositionRight()
    {
        if (BossHp >= MIN_HP && BossHp <= NOMAL_MAX_HP)
        {
            if (gameObject.transform.localScale.y > BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(NOMAL_UI_POSITION_X, NOMAL_SIDE_UI_POSITION_Y, 0);
            }
        }
        if (BossHp >= MIN_HP && BossHp <= SMALE_MAX_HP)
        {
            if (gameObject.transform.localScale.y < BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(MINI_UI_POSITION_X, MINI_UI_POSITION_Y, 0);
            }
        }
        if (BossHp >= BIG_MIN_HP && BossHp <= MAX_HP)
        {
            hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(BIG_UI_POSITION_X, 0, 0);
        }
    }

    private void BossUIPositionLeft()
    {
        if (BossHp >= MIN_HP && BossHp <= NOMAL_MAX_HP)
        {
            if (gameObject.transform.localScale.y > BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-NOMAL_UI_POSITION_X, NOMAL_SIDE_UI_POSITION_Y, 0.0f);
            }
        }
        if (BossHp >= MIN_HP && BossHp <= SMALE_MAX_HP)
        {
            if (gameObject.transform.localScale.y < BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-MINI_UI_POSITION_X, MINI_UI_POSITION_Y, 0.0f);
            }
        }
        if (BossHp >= BIG_MIN_HP && BossHp <= MAX_HP)
        {
            hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-BIG_UI_POSITION_X, 0, 0);
        }

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
            var repeatValue = Mathf.Repeat(warningFlashTime, WARNING_FLASH_TIME_MAX);

            warningRenderer.enabled = repeatValue >= WARNING_FLASH_TIME_MAX * 0.5f;

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
        else if ((transform.position.z - target.transform.position.z) > WARNING_DISPLAY_POSITION)
        {
            warningDisplay.SetActive(false);
            IsHazard = false;
            messageCount = 0;
        }

        if ((transform.position.z - target.transform.position.z) <= ATTACK_OFF_POSITION)
        {
            isAttackOff = true;
        }

        if ((transform.position.z - target.transform.position.z) <= DANGER_DISPLAY_POSITION)
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

    private void GameOverAnimasiton()
    {
        if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("LastAttack") && AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0f)
        {
            if (seCount < 1)
            {
                lastAttackAudio.PlayOneShot(lastAttackSE);
                seCount++;
            }
        }

        if (AnimationImage.GetCurrentAnimatorStateInfo(0).IsName("LastAttack") && AnimationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= GAME_OVER_TIME)
        {
            //ゲームオーバーフラグ
            IsGameOver = true;
        }
        if (isLastAttack && isDamageFlag)
        {
            audioSource.Stop();
            seCount = 0;
            isLastAttack = false;
        }
    }

    public void DamageTrue()
    {
        isDamageFlag = true;
    }

    public void DamageFalse()
    {
        isDamageFlag = false;
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