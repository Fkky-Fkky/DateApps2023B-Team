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

    private int playerNo;

    Rigidbody rb;

    private bool isInGroup = false;
    private bool isEnterItem = false;
    private bool isAttack = false;

    private GameObject enterItem = null;

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

    private bool isPlayerMoveDamage = false;
    private float defaultPosY = 54.0f;

    private Animator animationImage;

    private PlayerAttack attack;
    private PlayerEmote emote;
    private CarryEmote carryEmote;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animationImage = GetComponent<Animator>();

        switch (playerNumber)
        {
            case PlayerNumber.None:
                Debug.Log("None : " + gameObject.name);
                break;

            default:
                playerNo = (int)playerNumber;
                break;
        }

        carryDown = GetComponentInChildren<PlayerCarryDown>();
        carryDown.GetPlayerNo(playerNo);

        attack = GetComponentInChildren<PlayerAttack>();
        attack.GetPlayerNo(playerNo);

        emote = GetComponentInChildren<PlayerEmote>();
        emote.GetPlayerNo(playerNo);

        carryEmote = GetComponentInChildren<CarryEmote>();
        carryEmote.GetPlayerNo(playerNo);

        GetComponent<PlayerDamage>().GetPlayerNo(playerNo);


        defaultPosY = this.gameObject.transform.position.y;
    }

    private void Update()
    {
        if (isEnterItem)
        {
            if(enterItem == null)
            {
                isEnterItem = false;
            }
        }
    }

    private void FixedUpdate()
    {
        GamepadMove();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            isEnterItem = true;
            enterItem = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Group1")
           || collision.gameObject.CompareTag("Group2")
           || collision.gameObject.CompareTag("Group3")
           || collision.gameObject.CompareTag("Group4"))
        {
            isEnterItem = true;
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
            isEnterItem = false;
            enterItem = null;
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

        isInGroup = true;
        attack.OnIsCarry();

        animationImage.SetBool("Move", false);
        animationImage.SetBool("Carry", true);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Destroy(rigidbody);
        rb = GetComponentInParent<Rigidbody>();

    }

    public void RemoveItem()
    {
        if (isInGroup)
        {
            carryEmote.CallEndCarryEmote();
            gameObject.transform.parent.GetComponent<PlayerController>().PlayerOutGroup(playerNo);
            gameObject.transform.parent = null;
            isEnterItem = false;
            isInGroup = false;
        }
        
        attack.OffIsCarry();

        rb = this.gameObject.AddComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        this.gameObject.transform.position = new Vector3(
                   this.gameObject.transform.position.x,
                   defaultPosY,
                   this.gameObject.transform.position.z);

        animationImage.SetBool("Carry", false);
        animationImage.SetBool("CarryMove", false);
    }

    void GamepadMove()
    {
        if (!isPlayerMoveDamage && !isInGroup)
        {
            OnStickValue();
        }
    }

    void OnStickValue()
    {
        var leftStickValue = Gamepad.all[playerNo].leftStick.ReadValue();
        Vector3 vec = new Vector3(0, 0, 0);

        if (!isAttack)
        {
            if (leftStickValue.x != 0.0f)
            {
                animationImage.SetBool("Move", true);
                vec.x = moveSpeed * Time.deltaTime * leftStickValue.x;
            }
            if (leftStickValue.y != 0.0f)
            {
                animationImage.SetBool("Move", true);
                vec.z = moveSpeed * Time.deltaTime * leftStickValue.y;
            }

            if (!isEnterItem)
            {
                if (leftStickValue.x != 0 || leftStickValue.y != 0)
                {
                    var direction = new Vector3(leftStickValue.x, 0, leftStickValue.y);
                    transform.localRotation = Quaternion.LookRotation(direction);
                }
            }
        }

        if (leftStickValue.x == 0.0f && leftStickValue.y == 0.0f)
        {
            animationImage.SetBool("Move", false);
        }

        rb.velocity = vec;
    }

    public void PlayerDamage()
    {
        isPlayerMoveDamage = true;
        isInGroup = false;
        isAttack = false;
        isEnterItem = false;
    }

    public void NotPlayerDamage()
    {
        isPlayerMoveDamage = false;
        isInGroup = false;
        isAttack = false;
        isEnterItem = false;
    }

    public void StartAttack()
    {
        isAttack = true;
    }

    public void EndAttack()
    {
        isAttack = false;
    }

    public void StartCarryEmote()
    {
        carryEmote.CallStartCarryEmote();
    }

    public void EndCarryEmote()
    {
        carryEmote.CallEndCarryEmote();
    }

}
