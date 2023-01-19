using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStand : MonoBehaviour
{
    public bool IsConnect { get; private set; }

    private Vector3 connectCannonPos;
    private Transform cannonTransform = null;
    
    private const float CANNON_POS_Y = -0.3f;
    
    private void Start()
    {
        connectCannonPos = new Vector3(transform.position.x, CANNON_POS_Y, transform.position.z);
    }

    private void Update()
    {
        if (!IsConnect)
        {
            return;
        }

        if(connectCannonPos.y < cannonTransform.position.y)
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
        cannonTransform.position = connectCannonPos;
        cannonTransform.rotation = transform.rotation;
    }

    private void CannonCut()
    {
        IsConnect = false;
        cannonTransform.rotation = Quaternion.identity;
        cannonTransform = null;
    }
}
