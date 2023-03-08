using UnityEngine;

/// <summary>
/// ボスの移動
/// </summary>
public class BossMove : MonoBehaviour
{
    [SerializeField]
    private GameObject cenetrTarget = null;
    [SerializeField]
    private GameObject leftTarget   = null;
    [SerializeField]
    private GameObject rightTarget  = null;

    [SerializeField]
    private GameObject warningDisplay         = null;
    [SerializeField]
    private GameObject gameOverWarningDisplay = null;

    [SerializeField]
    private GameObject shockWaveEffect = null;

    [SerializeField]
    private Animator animationImage = null;

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

    //[SerializeField]
    private BossHPBarUI bossHPBarUI = null;

    private bool isDamageFlag = false;
    private bool isAppearance = true;
    private bool isNotMove    = false;
    private bool isAttackOff  = false;
    private bool isLastAttack = false;

    private int seCount      = 0;
    private int messageCount = 0;

    private float moveSpeed = 0.0f;
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
    public bool IsHazard { get; private set; }

    const int MAX_HP = 9;
    const float HALF_INDEX             =   0.5f;
    const float UNDER_POSITION         = -54.5f;
    const float DANGER_FLASH_TIME_MAX  =   0.5f;
    const float WARNING_FLASH_TIME_MAX =   1.0f;
    const float MOVE_TIME_MAX          =   3.0f;
    const float SIDE_POS               =   0.1f;
    const float MULTIPLIER             =  50.0f;

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
        bossAttack = GetComponent<BossAttack>();
        bossHPBarUI= GetComponent<BossHPBarUI>();
        rb = GetComponent<Rigidbody>();

        if (transform.position.x == 0.0f)
        {
            tag = "Center";
            bossHPBarUI.BossUIPositionCneter();
        }

        if (transform.position.x >= SIDE_POS)
        {
            tag = "Right";
            bossHPBarUI.BossUIPositionRight();
        }

        if (transform.position.x <= -SIDE_POS)
        {
            tag = "Left";
            bossHPBarUI.BossUIPositionLeft();
        }

        camera = Camera.main;
        canvas.worldCamera = camera;

        warningDisplay.SetActive(false);
        gameOverWarningDisplay.SetActive(false);

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
                pos.y = UNDER_POSITION;
                transform.position = pos;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                animationImage.SetTrigger("StandBy");
                isNotMove = true;
                isAppearance = false;
            }
        }

        if (isNotMove)
        {
            moveTime += Time.deltaTime;
            if (moveTime >= MOVE_TIME_MAX)
            {
                animationImage.SetTrigger("Walk");
                isNotMove = false;
                moveTime = 0.0f;
            }
        }
        Move();

        if (isLastAttack)
        {
            gameOverWarningDisplay.SetActive(true);
            dangerFlashTime += Time.deltaTime;

            var repeatValue = Mathf.Repeat(dangerFlashTime, DANGER_FLASH_TIME_MAX);

            gameOverDisplay.enabled = repeatValue >= DANGER_FLASH_TIME_MAX * HALF_INDEX;

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
        rb.AddForce((MULTIPLIER - 1f) * Physics.gravity, ForceMode.Acceleration);
    }

    /// <summary>
    /// ボスの移動
    /// </summary>
    private void Move()
    {
        if (isDamageFlag)
        {
            return;
        }
        if (bossAttack.IsAttackAll())
        {
            return;
        }
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
    /// <summary>
    /// 目的先に移動する
    /// </summary>
    /// <param name="target">目的先のオブジェクト</param>
    private void MoveTarget(GameObject target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);

        lookRotation.z = 0;
        lookRotation.x = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

        Vector3 pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);


        if ((transform.position.z - target.transform.position.z) <= WARNING_DISPLAY_POSITION)
        {
            warningDisplay.SetActive(true);

            warningFlashTime += Time.deltaTime;
            var repeatValue = Mathf.Repeat(warningFlashTime, WARNING_FLASH_TIME_MAX);

            warningRenderer.enabled = repeatValue >= WARNING_FLASH_TIME_MAX * HALF_INDEX;

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
            animationImage.SetTrigger("LastAttack");
        }
        else
        {
            isLastAttack = false;
            gameOverWarningDisplay.SetActive(false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private void GameOverAnimasiton()
    {
        if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("LastAttack") && animationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0f)
        {
            if (seCount < 1)
            {
                lastAttackAudio.PlayOneShot(lastAttackSE);
                seCount++;
            }
        }

        if (animationImage.GetCurrentAnimatorStateInfo(0).IsName("LastAttack") && animationImage.GetCurrentAnimatorStateInfo(0).normalizedTime >= GAME_OVER_TIME)
        {
            IsGameOver = true;//ゲームオーバーフラグ
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
    /// <summary>
    /// 登場するフラグを返す
    /// </summary>
    /// <returns>登場するフラグ</returns>
    public bool IsAppearance()
    {
        return isAppearance;
    }
    /// <summary>
    /// 攻撃しないフラグを返す
    /// </summary>
    /// <returns>攻撃しないフラグ</returns>
    public bool IsAttackOff()
    {
        return isAttackOff;
    }
}