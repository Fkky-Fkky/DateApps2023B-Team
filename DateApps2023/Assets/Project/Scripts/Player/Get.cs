using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Get : MonoBehaviour
{
    Rigidbody rb;
    public bool item_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("item")
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4")
            )
        {
            item_flag = true;
            collision.gameObject.transform.SetParent(transform);
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("item")
            || collision.gameObject.CompareTag("item2")
            || collision.gameObject.CompareTag("item3")
            || collision.gameObject.CompareTag("item4")
            )
        {
            item_flag = false;
            collision.gameObject.transform.parent = null;
        }
    }
}
