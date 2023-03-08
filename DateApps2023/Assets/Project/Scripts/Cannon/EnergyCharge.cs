using UnityEngine;

/// <summary>
/// �G�l���M�[�̃`���[�W����������N���X
/// </summary>
public class EnergyCharge : MonoBehaviour
{
    [SerializeField]
    private GameObject[] energyChargeEffect = new GameObject[3];

    [SerializeField]
    private SEManager seManager = null;

    [SerializeField]
    private EnergyGenerator generateEnergy = null;

    [SerializeField]
    private GameObject cannonLaser = null;

    private float coolTime = 0.0f;
    private bool isCoolTime = false;
    private Vector3[] laserScale = new Vector3[3];

    private BoxCollider boxCol = null;
    private AudioSource audioSource = null;
    private const int ADD_ENERGY = 1;
    private const float SMALL_LASER_SCALE = 0.3f;
    private const float LARGE_LASER_SCALE = 1.0f;
    private const float MEDIUM_LASER_SCALE = 1.5f;

    public int Energy { get; private set; }
    public int ChrgeEnergyType { get; private set; }

    public enum ENERGY_TYPE
    {
        SMALL,
        MEDIUM,
        LARGE,
    }

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        audioSource = transform.parent.GetComponent<AudioSource>();
        Energy = 0;
        laserScale[0] = new Vector3(SMALL_LASER_SCALE, SMALL_LASER_SCALE, SMALL_LASER_SCALE);
        laserScale[1] = new Vector3(LARGE_LASER_SCALE, LARGE_LASER_SCALE, LARGE_LASER_SCALE);
        laserScale[2] = new Vector3(MEDIUM_LASER_SCALE, MEDIUM_LASER_SCALE, MEDIUM_LASER_SCALE);
    }

    private void Update()
    {
        if (!isCoolTime)
        {
            return;
        }

        coolTime = Mathf.Max(coolTime - Time.deltaTime, 0.0f);
        if (coolTime <= 0.0f)
        {
            isCoolTime = false;
            boxCol.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("item"))
        {
            return;
        }

        if (other.transform.parent == null)
        {
            return;
        }

        int itemSize = other.GetComponent<CarryEnergy>().MyItemSizeCount;
        switch (itemSize)
        {
            case (int)CarryEnergy.ItemSize.Small:
                ChrgeEnergyType = (int)ENERGY_TYPE.SMALL;
                break;

            case (int)CarryEnergy.ItemSize.Medium:
                ChrgeEnergyType = (int)ENERGY_TYPE.MEDIUM;
                break;

            case (int)CarryEnergy.ItemSize.Large:
                ChrgeEnergyType = (int)ENERGY_TYPE.LARGE;
                break;
        }
        other.GetComponent<CarryEnergy>().DestroyMe();
        ChargeEnergy();
    }

    /// <summary>
    /// �G�l���M�[���`���[�W����
    /// </summary>
    private void ChargeEnergy()
    {
        const int MAX_ENERGY = 1;
        Energy = Mathf.Min(Energy + ADD_ENERGY, MAX_ENERGY);
        Instantiate(energyChargeEffect[ChrgeEnergyType], transform.position, Quaternion.identity);
        audioSource.PlayOneShot(seManager.EnergyChargeSe);
        generateEnergy.GenerateEnergy();
        cannonLaser.transform.localScale = laserScale[ChrgeEnergyType];
        if (Energy >= MAX_ENERGY)
        {
            boxCol.enabled = false;
        }
    }

    /// <summary>
    /// �G�l���M�[�����炷
    /// </summary>
    public void DisChargeEnergy()
    {
        const float COOL_TIME_MAX = 3.0f;
        Energy = Mathf.Max(Energy - ADD_ENERGY, 0);
        if (isCoolTime)
        {
            return;
        }
        isCoolTime = true;
        coolTime = COOL_TIME_MAX;
    }

    /// <summary>
    /// �G�l���M�[���`���[�W����Ă��邩��Ԃ�
    /// </summary>
    /// <returns>�G�l���M�[���`���[�W����Ă��邩</returns>
    public bool IsEnergyCharged()
    {
        return Energy > 0;
    }
}
