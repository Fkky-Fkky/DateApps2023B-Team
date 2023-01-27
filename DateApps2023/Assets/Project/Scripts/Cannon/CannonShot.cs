using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [SerializeField]
    private GameObject smokeEffect = null;

    [SerializeField]
    private GameObject chargeShotEffect = null;

    [SerializeField]
    private Transform smokePosition = null;

    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private GenerateEnergy generateEnergy = null;

    [SerializeField]
    private AudioClip shotChargeSe = null;

    [SerializeField]
    private AudioClip shotSe = null;

    public bool IsShotting { get; private set; }
    public bool IsNowShot { get; private set; }

    private int shotNum = 0;
    private float coolTime = 0.0f;
    private bool isCoolTime = false;
    private AudioSource audioSource = null;

    private const int MAX_SHOT_NUM = 5;
    private const float MAX_COOL_TIME = 3.0f;
    private const float INVOKE_TIME = 2.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        IsNowShot = false;
        if (shotNum >= MAX_SHOT_NUM)
        {
            generateEnergy.Generate();
            shotNum = 0;
        }
    }

    public void Shot()
    {
        IsShotting = true;
        energyCharge.DisChargeEnergy();
        CreateChageEffect();
        Invoke(nameof(CreateSmoke), INVOKE_TIME);
        shotNum++;
    }

    private void CreateChageEffect()
    {
        Instantiate(chargeShotEffect, smokePosition);
        audioSource.PlayOneShot(shotChargeSe);
    }

    private void CreateSmoke()
    {
        Instantiate(smokeEffect, smokePosition);
        audioSource.Stop();
        audioSource.PlayOneShot(shotSe);
        coolTime = MAX_COOL_TIME;
        IsNowShot = true;
        isCoolTime = true;
    }
}
