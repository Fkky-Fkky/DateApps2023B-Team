using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;


public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    [Tooltip("ˆÚ“®‚Ì‘¬‚³")]
    private float moveSpeed = 3.0f;

    [SerializeField]
    int playerNo;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        GameObject child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void FixedUpdate()
    {
        var leftStickValue = Gamepad.all[playerNo-1].leftStick.ReadValue();
        Debug.Log(leftStickValue);

        Vector3 vec = new Vector3(0, 0, 0);
        if (leftStickValue.x != 0.0f)
        {
            vec.x = moveSpeed * Time.deltaTime * leftStickValue.x;
        }
        if (leftStickValue.y != 0.0f)
        {
            vec.z = moveSpeed * Time.deltaTime * leftStickValue.y;
        }
        rb.velocity = vec;
    }
}
