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
    private BossDamage bossDamage = null;

    [SerializeField]
    private GenerateEnergy generateEnergy = null;

    public bool IsShotting { get; private set; }

    private int smokeNum = 0;
    private int shotNum = 0;
    private float coolTime = 0.0f;
    private GameObject[] smokeEffects = new GameObject[3];
    private GameObject cloneChrageEffect = null;

    private const int MAX_SMOKE_NUM = 3;
    private const int MAX_SHOT_NUM = 5;
    private const float MAX_COOL_TIME = 3.0f;
    private const float INVOKE_TIME = 2.0f;
    private const float REPEAT_INVOKE_TIME = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (!IsShotting)
        {
            return;
        }

        coolTime = Mathf.Max(coolTime - Time.deltaTime, 0.0f);
        if(coolTime <= 0.0f && smokeNum >= MAX_SMOKE_NUM)
        {
            IsShotting = false;
            DestroySmoke();
            DestroyChargeEffect();
            if(shotNum >= MAX_SHOT_NUM)
            {
                generateEnergy.Generate();
                shotNum = 0;
            }
        }
    }

    public void Shot()
    {
        IsShotting = true;
        energyCharge.DisChargeEnergy();
        CreateChageEffect();
        InvokeRepeating(nameof(CreateSmoke), INVOKE_TIME, REPEAT_INVOKE_TIME);
        shotNum++;
    }

    private void CreateChageEffect()
    {
        cloneChrageEffect = Instantiate(chargeShotEffect, smokePosition);
    }

    private void DestroyChargeEffect()
    {
        Destroy(cloneChrageEffect);
    }

    private void CreateSmoke()
    {
        smokeEffects[smokeNum] = Instantiate(smokeEffect, smokePosition);
        smokeNum++;
        bossDamage.KnockbackTrue();
        if (smokeNum >= MAX_SMOKE_NUM)
        {
            CancelInvoke();
            coolTime = MAX_COOL_TIME;
        }
    }

    private void DestroySmoke()
    {
        for (int i = 0; i < smokeEffects.Length; i++)
        {
            Destroy(smokeEffects[i]);
        }
        smokeNum = 0;
    }
}
