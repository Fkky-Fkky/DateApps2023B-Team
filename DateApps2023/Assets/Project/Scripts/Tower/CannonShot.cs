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

    public bool IsShotting { get; private set; }
    private float shotTime = 0.0f;

    private const float MAX_SHOT_TIME = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShotting)
        {
            shotTime += Time.deltaTime;
        }

        if(shotTime > MAX_SHOT_TIME)
        {
            shotTime = 0.0f;
            IsShotting = false;
        }
    }

    public void Shot()
    {
        IsShotting = true;
        energyCharge.DisChargeEnergy();
        Instantiate(smokeEffect, smokePosition);
    }
}
