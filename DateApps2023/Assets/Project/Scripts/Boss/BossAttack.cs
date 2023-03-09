using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�X�̍U���̃X�v���N�g
/// </summary>
public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private float chageTimeMax = 10.0f;

    [SerializeField]
    private GameObject dmageAreaCenter = null;
    [SerializeField]
    private GameObject damageAreaRight = null;
    [SerializeField]
    private GameObject damageAreaLeft  = null;

    [SerializeField]
    private GameObject chargeEffect = null;
    [SerializeField]
    private GameObject dangerZone   = null;

    [SerializeField]
    private Transform chargePos = null;

    [SerializeField]
    private AudioClip beamSE = null;

    [SerializeField]
    private BossMove bossMove     = null;
    [SerializeField]
    private BossDamage bossDamage = null;

    private int effectStop = 0;
    private int areaCount  = 0;
    private int seCount    = 0;

    private float time               = 0.0f;
    private float attackIntervalTime = 0.0f;
    private float chargeTime         = 0.0f;
    private float beamOffTime        = 0.0f;

    private Vector3 dangerCenter = new Vector3(0.0f, -1.2f, 0.0f);
    private Vector3 dangerLeft   = new Vector3(-10.0f, -1.2f, 0.0f);
    private Vector3 dangerRigth  = new Vector3(10.0f, -1.2f, 0.0f);

    private List<GameObject> effectList     = new List<GameObject>();
    private List<GameObject> dangerAreaList = new List<GameObject>();

    private AudioSource audioSource                 = null;
    private BossCSVGenerator bossCSVGenerator       = null;
    private BossAnimatorControl bossAnimatorControl = null;

    /// <summary>
    /// �r�[���𔭎˂���
    /// </summary>
    public bool IsAttack { get; private set; }
    /// <summary>
    /// �`���[�W�J�n
    /// </summary>
    public bool IsCharge { get; private set; }
    /// <summary>
    /// �`���[�W�`�r�[���𔭎ˌ�܂�
    /// </summary>
    public bool IsAttackAll { get; private set; }


    const int AREA_COUNT_MAX = 1;
    const int SE_COUNT_MAX   = 1;

    const float CENTER_TARGET         =   0.0f;
    const float RIGHT_TARGET          =   0.1f;
    const float LEFT_TARGET           =  -0.1f;
    const float BEAM_OFF_TIME_MAX     =   2.0f;
    const float DANGER_OBJECT_ANGLE_Y = 180.0f;

    private void Start()
    {
        bossCSVGenerator = GameObject.Find("BossGenerator").GetComponent<BossCSVGenerator>();
        audioSource = GetComponent<AudioSource>();
        bossAnimatorControl = GetComponent<BossAnimatorControl>();

        attackIntervalTime = bossCSVGenerator.AttackIntervalTime();
        IsAttackAll = false;
        IsAttack = false;
        IsCharge = false;
    }
    
    void Update()
    {
        if (!bossMove.IsAttackOff)
        {
            if (!bossDamage.IsInvincible)
            {
                Attack();
            }
        }

        if (bossMove.BossHp <= 0 || bossDamage.IsInvincible)
        {
            AttackOff();

            ListDestroy(effectList);
            ListDestroy(dangerAreaList);
        }
    }

    /// <summary>
    /// �{�X���U������
    /// </summary>
    private void Attack()
    {
        time += Time.deltaTime;
        if (bossMove.BossHp > 0 && !bossDamage.IsDamage)
        {
            if (time >= attackIntervalTime)
            {
                IsAttackAll = true;
                bossAnimatorControl.SetBoolTrue("Attack");
                Charge();
                ShootBeam();
            }
        }
    }

    /// <summary>
    /// �`���[�W
    /// </summary>
    private void Charge()
    {
        if (effectStop < 1)
        {
            effectList.Add(Instantiate(chargeEffect, chargePos.position, Quaternion.identity));
            DangerZone();
            IsCharge = true;
            effectStop++;
        }
        else
        {
            IsCharge = false;
        }
    }
    /// <summary>
    /// �r�[����������G���A�̕\��
    /// </summary>
    private void DangerZone()
    {
        switch (gameObject.tag)
        {
            case "Center":
                dangerAreaList.Add(Instantiate(dangerZone, dangerCenter, Quaternion.Euler(0.0f, DANGER_OBJECT_ANGLE_Y, 0.0f)));
                break;
            case "Left":
                dangerAreaList.Add(Instantiate(dangerZone, dangerLeft, Quaternion.Euler(0.0f, DANGER_OBJECT_ANGLE_Y, 0.0f)));
                break;
            case "Right":
                dangerAreaList.Add(Instantiate(dangerZone, dangerRigth, Quaternion.Euler(0.0f, DANGER_OBJECT_ANGLE_Y, 0.0f)));
                break;
        }
    }
    /// <summary>
    /// �r�[���𔭎�
    /// </summary>
    private void ShootBeam()
    {
         chargeTime += Time.deltaTime;
        if (chargeTime >= chageTimeMax)
        {
            IsAttack = true;
            DamageAreaControl();
        }
        else if (chargeTime < chageTimeMax && bossDamage.IsBossDamage)
        {
            AttackOff();
            ListDestroy(effectList);
            ListDestroy(dangerAreaList);
        }
    }
    /// <summary>
    /// �U�����L�����Z�������Ƃ��ɃG�t�F�N�g�ƃG���A�j��
    /// </summary>
    /// <param name="list">�f�X�g���C����I�u�W�F�N�g</param>
    private void ListDestroy(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
            list.RemoveAt(i);
        }
    }

    /// <summary>
    /// �����蔻����o��
    /// </summary>
    private void DamageAreaControl()
    {
        if (seCount < SE_COUNT_MAX)
        {
            audioSource.PlayOneShot(beamSE);
            seCount++;
        }

        if (gameObject.transform.position.x == CENTER_TARGET)
        {
            DamageObject(dmageAreaCenter);
        }
        if (gameObject.transform.position.x >= RIGHT_TARGET)
        {
            DamageObject(damageAreaRight);
        }
        if (gameObject.transform.position.x <= LEFT_TARGET)
        {
            DamageObject(damageAreaLeft);
        }

        beamOffTime += Time.deltaTime;
        if (beamOffTime >= BEAM_OFF_TIME_MAX)
        {
            AttackOff();
        }
    }

    /// <summary>
    /// �����蔻��G���A�𐶐�
    /// </summary>
    /// <param name="damageArea"></param>
    private void DamageObject(GameObject damageArea)
    {
        if (areaCount < AREA_COUNT_MAX)
        {
            Instantiate(damageArea);
            areaCount++;
        }
    }
    /// <summary>
    /// �U���I��
    /// </summary>
    private void AttackOff()
    {
        IsAttack    = false;
        IsAttackAll = false;
        seCount    = 0;
        effectStop = 0;
        areaCount  = 0;
        chargeTime  = 0.0f;
        beamOffTime = 0.0f;
        time        = 0.0f;
        bossAnimatorControl.SetBoolFalse("Attack");
    }
    /// <summary>
    /// �`���[�W���I�����鎞�Ԃ�Ԃ�
    /// </summary>
    /// <returns>�`���[�W���I�����鎞��</returns>
    public float BeamTimeMax()
    {
        return chageTimeMax;
    }
    /// <summary>
    /// �r�[�������ˏI���܂ł̎��Ԃ�Ԃ�
    /// </summary>
    /// <returns>�r�[�������ˏI���܂ł̎���</returns>
    public float BeamOffTimeMax()
    {
        return BEAM_OFF_TIME_MAX;
    }
}