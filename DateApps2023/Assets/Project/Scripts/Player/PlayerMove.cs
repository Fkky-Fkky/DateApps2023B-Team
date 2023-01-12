using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;



public class PlayerMove : MonoBehaviour
{
    #region
    [SerializeField]
    [Tooltip("移動の速さ")]
    private float moveSpeed = 2000.0f;
    private float tempMoveSpeed = 0;
    [SerializeField]
    private float slowMoveSpeed = 150.0f;

    private int playerNo;

    Rigidbody rb;

    private bool InGroup = false;
    private bool EnterItem = false;
    private bool IsAttack = false;

    private GameObject ItemOfEnter = null;

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

    private bool playerMoveDamage = false;
    private float defaultPosY = 54.0f;

    private Animator AnimationImage;

    private PlayerAttack attack;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AnimationImage = GetComponent<Animator>();

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

        attack = GetComponentInChildren<PlayerAttack>();
        attack.GetPlayerNo(playerNo);

        tempMoveSpeed = moveSpeed;

        defaultPosY = this.gameObject.transform.position.y;
    }

    private void Update()
    {
        if (EnterItem)
        {
            if(ItemOfEnter == null)
            {
                EnterItem = false;
            }
        }
    }

    private void FixedUpdate()
    {
        GamepadMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Group"))
        {
            moveSpeed = slowMoveSpeed;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            EnterItem = true;
            ItemOfEnter = collision.gameObject;
        }
        if (collision.gameObject.CompareTag("Group1")
           || collision.gameObject.CompareTag("Group2")
           || collision.gameObject.CompareTag("Group3")
           || collision.gameObject.CompareTag("Group4"))
        {
            EnterItem = true;
            moveSpeed = slowMoveSpeed;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("item")
           || collision.gameObject.CompareTag("Group1")
           || collision.gameObject.CompareTag("Group2")
           || collision.gameObject.CompareTag("Group3")
           || collision.gameObject.CompareTag("Group4")
           )
        {
            EnterItem = false;
            ItemOfEnter = null;
            this.gameObject.transform.position = new Vector3(
                this.gameObject.transform.position.x,
                defaultPosY,
                this.gameObject.transform.position.z);
        }

        if (collision.gameObject.CompareTag("Group1")
           || collision.gameObject.CompareTag("Group2")
           || collision.gameObject.CompareTag("Group3")
           || collision.gameObject.CompareTag("Group4"))
        {
            EnterItem = false;
            this.gameObject.transform.position = new Vector3(
                  this.gameObject.transform.position.x,
                  defaultPosY,
                  this.gameObject.transform.position.z);
        }
    }

    public void GetItem(int groupNo)
    {
        GameObject group = GameObject.Find("Group" + groupNo);
        gameObject.transform.SetParent(group.gameObject.transform);
        group.GetComponent<PlayerController>().GetMyNo(playerNo, this.gameObject);

        InGroup = true;
        AnimationImage.SetBool("Move", false);
        AnimationImage.SetBool("Carry", true);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Destroy(rigidbody);
        rb = GetComponentInParent<Rigidbody>();

    }

    public void RemoveItem(int groupNo)
    {
        GameObject group = GameObject.Find("Group" + groupNo);
        gameObject.transform.parent = null;
        PlayerController playerController = group.GetComponent<PlayerController>();
        int sentNumber = playerNo;
        playerController.PlayerOutGroup(sentNumber);

        EnterItem = false;
        InGroup = false;

        rb = this.gameObject.AddComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        this.gameObject.transform.position = new Vector3(
                   this.gameObject.transform.position.x,
                   defaultPosY,
                   this.gameObject.transform.position.z);

        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);
    }

    void GamepadMove()
    {
        if (!playerMoveDamage)
        {
            var leftStickValue = Gamepad.all[playerNo].leftStick.ReadValue();

            if (!InGroup)
            {
                Vector3 vec = new Vector3(0, 0, 0);

                if (!IsAttack)
                {
                    if (leftStickValue.x != 0.0f)
                    {
                        AnimationImage.SetBool("Move", true);
                        vec.x = moveSpeed * Time.deltaTime * leftStickValue.x;
                    }
                    if (leftStickValue.y != 0.0f)
                    {
                        AnimationImage.SetBool("Move", true);
                        vec.z = moveSpeed * Time.deltaTime * leftStickValue.y;
                    }

                    if (!EnterItem)
                    {
                        moveSpeed = tempMoveSpeed;
                        if (leftStickValue.x != 0 || leftStickValue.y != 0)
                        {
                            var direction = new Vector3(leftStickValue.x, 0, leftStickValue.y);
                            transform.localRotation = Quaternion.LookRotation(direction);
                        }
                    }
                    else
                    {
                        moveSpeed = slowMoveSpeed;
                    }
                }

                if(leftStickValue.x == 0.0f && leftStickValue.y == 0.0f)
                {
                    AnimationImage.SetBool("Move", false);
                }

                rb.velocity = vec;
            }
        }
    }

    public void PlayerDamage()
    {
        playerMoveDamage = true;
        InGroup = false;
        IsAttack = false;
        EnterItem = false;
    }

    public void NotPlayerDamage()
    {
        playerMoveDamage = false;
        InGroup = false;
        IsAttack = false;
        EnterItem = false;
    }

    public void StartAttack()
    {
        IsAttack = true;
    }

    public void EndAttack()
    {
        IsAttack = false;
    }

    public void CallHanteiEnter()
    {
        carryDown.HanteiEnter();
    }

}
