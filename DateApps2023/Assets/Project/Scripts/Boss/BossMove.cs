using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    [Tooltip("ƒ{ƒXˆÚ“®‘¬“x")]
    private float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity -= new Vector3(0.0f, 0.0f, moveSpeed * Time.deltaTime);
    }
}
