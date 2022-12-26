using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSwitch : MonoBehaviour
{
    [SerializeField]
    private EnergyCharge energyCharge = null;

    [SerializeField]
    private CannonShot cannonShot = null;

    private bool isShot = false;

    private Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = GetComponent<Transform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        isShot = !cannonShot.IsShotting && energyCharge.IsEnergyCharge();

        Vector3 scale_ = scale;
        if (!isShot)
        {
            scale_.y = 1.0f;

        }
        else
        {
            scale_.y = scale.y;
        }

        transform.localScale = scale_;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isShot)
            {
                cannonShot.Shot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerAttackPoint"))
        {
            return;
        }

        if (isShot)
        {
            cannonShot.Shot();
        }
    }
}
