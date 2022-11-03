using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class player1 : MonoBehaviour
{
    Rigidbody rb;

    private CharacterController characterController;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.rightArrowKey.isPressed)  
            characterController.Move(gameObject.transform.right * 40.0f * Time.deltaTime);
        //transform.Translate(40f * Time.deltaTime, 0, 0);
        if (Keyboard.current.leftArrowKey.isPressed)
            characterController.Move(gameObject.transform.right * -40.0f * Time.deltaTime);
        //transform.Translate(-40f * Time.deltaTime, 0, 0);
        if (Keyboard.current.upArrowKey.isPressed)
            characterController.Move(gameObject.transform.forward * 40.0f* Time.deltaTime); 
            //transform.Translate(0, 0, 40f * Time.deltaTime);
        if (Keyboard.current.downArrowKey.isPressed)
            characterController.Move(gameObject.transform.forward * -40.0f* Time.deltaTime);
        //transform.Translate(0, 0, -40f * Time.deltaTime);
        ////rzi
        //if (Keyboard.current.wKey.isPressed)
        //    transform.Rotate(0.0f, -5000 * Time.deltaTime, 0.0f);
        //if (Keyboard.current.sKey.isPressed)
        //    transform.Rotate(50 * Time.deltaTime, 0.0f, 0.0f);
        //if (Keyboard.current.aKey.isPressed)
        //    transform.Rotate(0.0f, -50 * Time.deltaTime, 0.0f);
        //if (Keyboard.current.dKey.isPressed)
        //    transform.Rotate(0.0f, 50 * Time.deltaTime, 0.0f);
    }


}
