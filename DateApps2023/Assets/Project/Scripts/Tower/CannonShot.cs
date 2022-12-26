using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [SerializeField]
    private GameObject smokeEffect = null;

    [SerializeField]
    private Transform smokePosition = null;

    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private BossDamage bossDamage = null;

    public bool IsShotting { get; private set; }
    private int smokeNum = 0;
    private GameObject[] smokeEffects = new GameObject[3];

    private float coolTime = 0.0f;

    private const float MAX_COOL_TIME = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsShotting)
        {
            return;
        }

        coolTime = Mathf.Max(coolTime - Time.deltaTime, 0.0f);
        if(coolTime <= 0.0f && smokeNum >= 3)
        {
            IsShotting = false;
            DestroySmoke();
        }
    }

    public void Shot()
    {
        IsShotting = true;
        energyCharge.DisChargeEnergy();
        InvokeRepeating("CreateSmoke", 0.0f, 2.0f);
    }

    private void CreateSmoke()
    {
        smokeEffects[smokeNum] = Instantiate(smokeEffect, smokePosition);
        smokeNum++;
        bossDamage.KnockbackTrue();
        if (smokeNum >= 3)
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
