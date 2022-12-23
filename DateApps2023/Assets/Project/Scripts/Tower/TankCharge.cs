using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCharge : MonoBehaviour
{
    private GameObject inner = null;
    // Start is called before the first frame update
    void Start()
    {
        inner = transform.GetChild(0).gameObject;
        inner.SetActive(false);
    }

    public void Charge()
    {
        inner.SetActive(true);
    }

    public void DisCharge()
    {
        inner.SetActive(false);
    }
}
