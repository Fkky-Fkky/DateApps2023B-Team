using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region
    [SerializeField]
    [Tooltip("移動の速さ")]
    private float moveSpeed = 3.0f;

    [SerializeField] 
    private CharacterController[] characterController;



    #endregion

    private void Awake()
    {
        //characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gamepad.all で接続されているすべてのゲームパッドを列挙できる
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var gamepad = Gamepad.all[i];
            var character = characterController[i].GetComponent<CharacterController>();
            var leftStickValue = gamepad.leftStick.ReadValue();
        
            if (gamepad == null)
            {
                Debug.Log("ゲームパッドがありません。");
                return;
            }

            //if (Input.GetKey(KeyCode.W))
            //{
            //    characterController[i].Move(gameObject.transform.forward * moveSpeed * Time.deltaTime);
            //}
            //if (Input.GetKey(KeyCode.S))
            //{
            //    characterController[i].Move(gameObject.transform.forward * -1.0f * moveSpeed * Time.deltaTime);
            //}
            //if (Input.GetKey(KeyCode.D))
            //{
            //    characterController[i].Move(gameObject.transform.right * moveSpeed * Time.deltaTime);
            //}
            //if (Input.GetKey(KeyCode.A))
            //{
            //    characterController[i].Move(gameObject.transform.right * -1.0f * moveSpeed * Time.deltaTime);
            //}

            if (leftStickValue.x != 0.0f)
            {
                character.Move(gameObject.transform.right * moveSpeed * Time.deltaTime * leftStickValue.x);
            }
            if (leftStickValue.y != 0.0f)
            {
                character.Move(gameObject.transform.forward * moveSpeed * Time.deltaTime * leftStickValue.y);
            }
        }



    }
}
