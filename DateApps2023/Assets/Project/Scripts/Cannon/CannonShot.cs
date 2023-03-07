using UnityEngine;

/// <summary>
/// ��C�̔��ˏ����N���X
/// </summary>
public class CannonShot : MonoBehaviour
{
    [SerializeField]
    private GameObject[] smokeEffects = new GameObject[3];

    [SerializeField]
    private GameObject[] shotChargeEffects = new GameObject[3];

    [SerializeField]
    private GameObject coolDownEffect = null;

    [SerializeField]
    private Transform[] coolDownPos = new Transform[2];

    [SerializeField]
    private Transform smokePosition = null;

    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private SEManager seManager = null;

    public bool IsShotting { get; private set; }
    public bool IsNowShot { get; private set; }

    private int energyType = 0;
    private float coolTime = 0.0f;
    private float[] laserEndTime = new float[3];
    private bool isCoolTime = false;
    private AudioSource audioSource = null;
    private GameObject shotCharge = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        laserEndTime[0] = 1.0f;
        laserEndTime[1] = 1.5f;
        laserEndTime[2] = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCoolTime)
        {
            return;
        }

        coolTime = Mathf.Max(coolTime - Time.deltaTime, 0.0f);

        if (coolTime > 0.0f)
        {
            return;
        }

        IsShotting = false;
        isCoolTime = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("BossAttack"))
        {
            return;
        }
        ShotCancel();
    }

    /// <summary>
    /// ���ˏ���
    /// </summary>
    public void Shot()
    {
        const float INVOKE_TIME = 2.0f;
        IsShotting = true;
        energyType = energyCharge.ChrgeEnergyType;
        audioSource.PlayOneShot(seManager.GetCannonBeamSe(energyType));
        CreateChageEffect();
        Invoke(nameof(CreateSmoke), INVOKE_TIME);
    }

    /// <summary>
    /// �r�[�����ˑO�̃G�t�F�N�g����
    /// </summary>
    private void CreateChageEffect()
    {
        shotCharge = Instantiate(shotChargeEffects[energyType], smokePosition);
    }

    /// <summary>
    /// ���ˎ��̉�����
    /// </summary>
    private void CreateSmoke()
    {
        const float MAX_COOL_TIME = 3.0f;
        Instantiate(smokeEffects[energyType], smokePosition);
        coolTime   = MAX_COOL_TIME;
        IsNowShot  = true;
        isCoolTime = true;
        Invoke(nameof(LaserEnd), laserEndTime[energyType]);
        energyCharge.DisChargeEnergy();
    }

    /// <summary>
    /// �r�[���̃G�t�F�N�g�I������
    /// </summary>
    private void LaserEnd()
    {
        IsNowShot = false;
        CreateCoolDownEffects();
    }

    /// <summary>
    /// �N�[���_�E���G�t�F�N�g����
    /// </summary>
    private void CreateCoolDownEffects()
    {
        for (int i = 0; i < coolDownPos.Length; i++)
        {
            Instantiate(coolDownEffect, coolDownPos[i].position, Quaternion.identity);
        }
    }

    /// <summary>
    /// ���˃L�����Z������
    /// </summary>
    private void ShotCancel()
    {
        coolTime   = 0.0f;
        IsShotting = false;
        isCoolTime = false;
        IsNowShot  = false;
        Destroy(shotCharge);
        CancelInvoke();
        audioSource.Stop();
        CreateCoolDownEffects();
    }
}
