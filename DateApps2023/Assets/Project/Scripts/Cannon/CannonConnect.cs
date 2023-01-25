using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonConnect : MonoBehaviour
{
    [SerializeField]
    GameObject connectEffect = null;

    [SerializeField]
    private Transform effectPos = null;

    public int ConnectingPos { get; private set; }
    public bool IsConnect { get; private set; }

    private Transform standTransform = null;

    private const float CANNON_POS_Y = -0.3f;

    private void Update()
    {
        if (!IsConnect)
        {
            return;
        }

        if (CANNON_POS_Y < transform.position.y)
        {
            CannonCut();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("CannonStand"))
        {
            return;
        }
        ConnectingPos = other.GetComponent<CannonStand>().ConnectingPos;
        standTransform = other.transform;
        CannontConnect();
    }

    private void CannontConnect()
    {
        IsConnect = true;
        transform.rotation = standTransform.rotation;
        Instantiate(connectEffect, effectPos.position, Quaternion.identity);
    }

    private void CannonCut()
    {
        IsConnect = false;
        transform.rotation = Quaternion.identity;
        standTransform = null;
        ConnectingPos = -1;
    }
}
