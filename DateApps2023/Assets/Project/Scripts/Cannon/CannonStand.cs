using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStand : MonoBehaviour
{
    [SerializeField]
    GameObject connectEffect = null;

    [SerializeField]
    private Transform effectPos = null;


    public bool IsConnect { get; private set; }
    
    private Transform cannonTransform = null;
    
    private const float CANNON_POS_Y = -0.3f;
    
    private void Update()
    {
        if (!IsConnect)
        {
            return;
        }

        if(CANNON_POS_Y < cannonTransform.position.y)
        {
            CannonCut();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Cannon"))
        {
            return;
        }
        cannonTransform = other.transform;
        CannontConnect();
    }

    private void CannontConnect()
    {
        IsConnect = true;
        Vector3 connectPos = new Vector3(cannonTransform.position.x, CANNON_POS_Y, cannonTransform.position.z);
        cannonTransform.position = connectPos;
        cannonTransform.rotation = transform.rotation;
        Instantiate(connectEffect, effectPos.position, Quaternion.identity);
    }

    private void CannonCut()
    {
        IsConnect = false;
        cannonTransform.rotation = Quaternion.identity;
        cannonTransform = null;
    }
}
