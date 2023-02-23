using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [SerializeField]
    private GameObject[] smokeEffects = new GameObject[3];

    [SerializeField]
    private GameObject[] shotChargeEffects = new GameObject[3];

    [SerializeField]
    private Transform smokePosition = null;

    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private AudioClip shotChargeSe = null;

    [SerializeField]
    private AudioClip shotSe = null;

    [SerializeField]
    private GameObject coolDownEffect = null;

    [SerializeField]
    private Transform coolTimePos = null;

    public bool IsShotting { get; private set; }
    public bool IsNowShot { get; private set; }

    private int energyType = 0;
    private float coolTime = 0.0f;
    private float[] laserEndTime = new float[3];
    private bool isCoolTime = false;
    private AudioSource audioSource = null;
    private GameObject shotCharge = null;

    private const float MAX_COOL_TIME = 3.0f;
    private const float INVOKE_TIME = 2.0f;

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

    public void Shot()
    {
        IsShotting = true;
        energyType = energyCharge.ChrgeEnergyType;
        CreateChageEffect();
        Invoke(nameof(CreateSmoke), INVOKE_TIME);
    }

    private void CreateChageEffect()
    {
        shotCharge = Instantiate(shotChargeEffects[energyType], smokePosition);
        audioSource.PlayOneShot(shotChargeSe);
    }

    private void CreateSmoke()
    {
        Instantiate(smokeEffects[energyType], smokePosition);
        audioSource.Stop();
        audioSource.PlayOneShot(shotSe);
        coolTime   = MAX_COOL_TIME;
        IsNowShot  = true;
        isCoolTime = true;
        Invoke(nameof(LaserEnd), laserEndTime[energyType]);
        energyCharge.DisChargeEnergy();
    }

    private void LaserEnd()
    {
        IsNowShot = false;
        Instantiate(coolDownEffect, coolTimePos.position, Quaternion.identity);
    }

    private void ShotCancel()
    {
        coolTime   = 0.0f;
        IsShotting = false;
        isCoolTime = false;
        IsNowShot  = false;
        Destroy(shotCharge);
        CancelInvoke();
        audioSource.Stop();
    }
}
