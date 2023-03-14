﻿using UnityEngine;
/// <summary>
/// ビームの狙う場所
/// </summary>
public class TargetV3D : MonoBehaviour
{

    public Transform targetCursor;
    public float speed = 1f;

    const float CENTER_POS = 0.0f;
    const float RIGHT_POS  = 1.0f;
    const float LEFT_POS   = -0.1f;


    //private Vector3 mouseWorldPosition;

    [SerializeField]
    Transform bossPosition;

    [SerializeField]
    Transform targetCenter;
    [SerializeField]
    Transform targetRight;
    [SerializeField]
    Transform targetLeft;


    // Positioning cursor prefab
    void FixedUpdate()
    {
        if (bossPosition.position.x == CENTER_POS)
        {
            Quaternion toRotation = Quaternion.LookRotation(targetCenter.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
            targetCursor.position = targetCenter.position;
        }

        if (bossPosition.position.x >= RIGHT_POS)
        {
            Quaternion toRotation = Quaternion.LookRotation(targetRight.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
            targetCursor.position = targetRight.position;
        }

        if (bossPosition.position.x <= LEFT_POS)
        {
            Quaternion toRotation = Quaternion.LookRotation(targetLeft.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
            targetCursor.position = targetLeft.position;
        }


    }
}
