using UnityEngine;

/// <summary>
/// �{�X���U�����ꂽ�Ƃ��̃_���[�W�X�v���N�g
/// </summary>
public class BossDamage : MonoBehaviour
{
    [SerializeField]
    private float invincibleTimeEnd = 4.0f;

    [SerializeField]
    private GameObject explosionEffect = null;
    [SerializeField]
    private GameObject fellDownEffect  = null;

    [SerializeField]
    private Animator damageAnimation = null;

    [SerializeField]
    private Transform damagePoint = null;

    private bool isKnockback    = false;

    private int maxHp              =  0;
    private int bulletType         = -1;
    private int smallEnergyDamage  =  0;
    private int mediumEnergyDamage =  0;
    private int largeEnergyDamage  =  0;

    private float invincibleTime   = 0.0f;
    private float bossDamgeOffTime = 0.0f;
    private float knockbackTime    = 0.0f;
    private float bossDestroyTime  = 0.0f;

    private BossMove bossMove                   = null;
    private DamageCSV damageCSV                 = null;
    private BossDamageHPBarUI bossDamageHPBarUI = null;

    /// <summary>
    /// ���G�t���O
    /// </summary>
    public bool IsInvincible { get; private set; }
    /// <summary>
    /// �{�X���_���[�W���󂯂Ă���t���O
    /// </summary>
    public bool IsBossDamage { get; private set; }
    /// <summary>
    /// �{�X���_���[�W���󂯂��t���O
    /// </summary>
    public bool IsDamage { get; private set; }
    /// <summary>
    /// �{�X���|�ꂽ�t���O
    /// </summary>
    public bool IsFellDown { get; private set; }


    const float EFFECT_POS_Y             = -40.0f;
    const float BOSS_DAMGE_OFF_TIME_MAX  =   0.6f;
    const float KNOCK_BACK_TIME_MAX      =   1.5f;
    const float KNOCK_BACK_MOVE          =   3.0f;
    const float BOSS_DESTROY_TIME_MAX    =   2.5f;

    void Start()
    {
        bossMove = GetComponent<BossMove>();
        bossDamageHPBarUI = GetComponent<BossDamageHPBarUI>();

        damageCSV = GameObject.Find("BossManager").GetComponent<DamageCSV>();

        smallEnergyDamage  = damageCSV.Small;
        mediumEnergyDamage = damageCSV.Medium;
        largeEnergyDamage  = damageCSV.Large;

        IsBossDamage = false;
        IsInvincible = false;
        IsDamage     = false;
        IsFellDown = false;

        maxHp = bossMove.BossHp;

        bossDamageHPBarUI.HpMemoriPosition(maxHp);
    }

    void Update()
    {
        if (isKnockback)
        {
            knockbackTime += Time.deltaTime;
            if (knockbackTime <= KNOCK_BACK_TIME_MAX)
            {
                Vector3 bossPos = transform.position;
                bossPos.z += KNOCK_BACK_MOVE * Time.deltaTime;
                transform.position = bossPos;
            }
            else
            {
                knockbackTime = 0.0f;
                isKnockback = false;
            }
        }

        if (IsDamage)
        {
            if (!bossMove.IsAppearance)
            {
                Instantiate(explosionEffect, damagePoint.position, Quaternion.identity);
                BulletTypeDamage();
                DamageAnimation();

                IsInvincible = true;
                bulletType = -1;
                IsDamage = false;
            }
        }

        if (IsInvincible)
        {
            invincibleTime += Time.deltaTime;
            if (invincibleTime >= invincibleTimeEnd)
            {
                IsInvincible = false;
                damageAnimation.SetTrigger("Walk");
                bossMove.DamageFalse();
                invincibleTime = 0.0f;
            }
        }

        if (bossMove.BossHp <= 0)
        {
            bossDestroyTime += Time.deltaTime;
            if (bossDestroyTime >= BOSS_DESTROY_TIME_MAX)
            {
                IsFellDown = true;
                Vector3 pos = new Vector3(transform.position.x, EFFECT_POS_Y, transform.position.z);
                Instantiate(fellDownEffect, pos, Quaternion.identity);
                Destroy(gameObject);
                bossDestroyTime = 0.0f;
            }
        }

        if (IsBossDamage)
        {
            bossDamgeOffTime += Time.deltaTime;
            if (bossDamgeOffTime >= BOSS_DAMGE_OFF_TIME_MAX)
            {
                IsBossDamage = false;
                bossDamgeOffTime = 0.0f;
            }
        }
    }

    /// <summary>
    /// �G�l���M�[���Ƃ̃_���[�W��
    /// </summary>
    private void BulletTypeDamage()
    {
        if (bulletType == 0)
        {
            Damage(smallEnergyDamage);
            bossDamageHPBarUI.HpBarSmallActive(bossMove.BossHp);
        }
        else if (bulletType == 1)
        {
            Damage(mediumEnergyDamage);
            bossDamageHPBarUI.HpBarMediumActive(maxHp, bossMove.BossHp, mediumEnergyDamage);
        }
        else if (bulletType == 2)
        {
            Damage(largeEnergyDamage);
            bossDamageHPBarUI.HpBarLargeActive(maxHp, bossMove.BossHp);
        }
    }

    /// <summary>
    /// �̗͂̌�����
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W�̒l</param>
    private void Damage(int damage)
    {
        bossMove.BossHp -= damage;
        if (bossMove.BossHp < 0)
        {
            bossMove.BossHp = 0;
        }
    }
    /// <summary>
    /// �U�����ꂽ���̃A�j���[�V����
    /// </summary>
    private void DamageAnimation()
    {
        if (bossMove.BossHp > 0)
        {
            damageAnimation.SetTrigger("Damage");
        }
        else
        {
            damageAnimation.SetTrigger("Die");
        }
    }
    /// <summary>
    /// �G�l���M�[���̏ꍇ
    /// </summary>
    public void KnockbackTrueSmall()
    {
        DamageKnockBack(0);
    }
    /// <summary>
    /// �G�l���M�[���̏ꍇ
    /// </summary>
    public void KnockbackTrueMedium()
    {
        DamageKnockBack(1);
    }
    /// <summary>
    /// �G�l���M�[��̏ꍇ
    /// </summary>
    public void KnockbackTrueLarge()
    {
        DamageKnockBack(2);
    }
    /// <summary>
    /// �_���[�W
    /// </summary>
    /// <param name="Bullet">�G�l���M�[�̎�ނ̒l</param>
    void DamageKnockBack(int Bullet)
    {
        if (isKnockback)
        {
            return;
        }
        if (!bossMove.IsAppearance)
        {
            if (!IsInvincible)
            {
                isKnockback = true;
                IsDamage = true;
                IsBossDamage = true;
                bulletType = Bullet;
                bossMove.DamageTrue();
            }
        }
    }
}