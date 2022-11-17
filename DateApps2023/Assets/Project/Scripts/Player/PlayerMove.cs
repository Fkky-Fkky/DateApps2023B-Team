using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;



public class PlayerMove : MonoBehaviour
{
    #region
    [SerializeField]
    [Tooltip("ˆÚ“®‚Ì‘¬‚³")]
    private float moveSpeed = 2000.0f;

    private int playerNo;

    Rigidbody rb;
    private BoxCollider myBoxCol;

    [SerializeField] private float maxAngularSpeed = Mathf.Infinity;
    [SerializeField] private float smoothTime = 0.01f;
    [SerializeField] private Vector3 _forward = Vector3.forward;
    [SerializeField] private Vector3 _up = Vector3.up;
    [SerializeField] private Vector3 _axis = Vector3.up;

    private Transform myTransform;
    private Vector3 prevPosition;
    private float currentAngularVelocity;

    private bool InGroup = false;
    private bool EnterItem = false;

    public enum PlayerNumber
    {
        PL_1P,
        PL_2P,
        PL_3P,
        PL_4P,
        None
    }

    [SerializeField]
    PlayerNumber playerNumber = PlayerNumber.None;

    PlayerCarryDown carryDown;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myBoxCol = GetComponent<BoxCollider>();

        myTransform = transform;
        prevPosition = myTransform.position;

        switch (playerNumber)
        {
            case PlayerNumber.None:
                Debug.Log("None : " + gameObject.name);
                break;

            case PlayerNumber.PL_1P:
                playerNo = (int)PlayerNumber.PL_1P;
                break;
            case PlayerNumber.PL_2P:
                playerNo = (int)PlayerNumber.PL_2P;
                break;
            case PlayerNumber.PL_3P:
                playerNo = (int)PlayerNumber.PL_3P;
                break;
            case PlayerNumber.PL_4P:
                playerNo = (int)PlayerNumber.PL_4P;
                break;
        }

        carryDown = GetComponentInChildren<PlayerCarryDown>();
        carryDown.GetPlayerNo(playerNo);

    }

    // Update is called once per frame
    void Update()
    {
        if (!InGroup)
        {
            if (!EnterItem)
            {
                var position = myTransform.position;
                var delta = position - prevPosition;
                prevPosition = position;

                if (delta == Vector3.zero)
                {
                    return;
                }

                var offsetRot = Quaternion.Inverse(Quaternion.LookRotation(_forward, _up));
                var forward = myTransform.TransformDirection(_forward);
                var projectFrom = Vector3.ProjectOnPlane(forward, _axis);
                var projectTo = Vector3.ProjectOnPlane(delta, _axis);
                var diffAngle = Vector3.Angle(projectFrom, projectTo);
                var rotAngle = Mathf.SmoothDampAngle(
                    0,
                    diffAngle,
                    ref currentAngularVelocity,
                    smoothTime,
                    maxAngularSpeed
                );
                var lookFrom = Quaternion.LookRotation(projectFrom);
                var lookTo = Quaternion.LookRotation(projectTo);
                var nextRot = Quaternion.RotateTowards(lookFrom, lookTo, rotAngle) * offsetRot;

                myTransform.rotation = nextRot;
            }
        }
        
    }

    private void FixedUpdate()
    {
        GamepadMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("item")
           || collision.gameObject.CompareTag("item2")
           || collision.gameObject.CompareTag("item3")
           || collision.gameObject.CompareTag("item4")
           )
        {
            EnterItem = true;
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
            EnterItem = false;
        }
    }

    public void GetItem(int groupNo)
    {
        GameObject group = GameObject.Find("Group" + groupNo);
        gameObject.transform.SetParent(group.gameObject.transform);
        group.GetComponent<PlayerController>().GetMyNo(playerNo);

        InGroup = true;
        //rb.isKinematic = true;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Destroy(rigidbody);
        rb = GetComponentInParent<Rigidbody>();
        
        //myBoxCol.enabled = false;
    }

    public void RemoveItem(int groupNo)
    {
        GameObject group = GameObject.Find("Group" + groupNo);
        gameObject.transform.parent = null;
        PlayerController playerController = group.GetComponent<PlayerController>();
        int sentNumber = playerNo;
        playerController.OutGroup(sentNumber);

        //myBoxCol.enabled = true;
        EnterItem = false;
        //rb.isKinematic = false;
        rb = this.gameObject.AddComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        InGroup = false;
    }

    void GamepadMove()
    {
        if (!InGroup)
        {
            var leftStickValue = Gamepad.all[playerNo].leftStick.ReadValue();

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
}
