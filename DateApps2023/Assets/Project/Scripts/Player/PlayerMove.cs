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
    [Tooltip("ˆÚ“®‚Ì‘¬‚³")]
    private float moveSpeed = 2000.0f;

    private int playerNo;

    private Rigidbody rb;

    private bool isGroup = false;
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

    private PlayerCarryDown carryDown;

    private bool isPlayerMoveDamage = false;
    private float defaultPosY = 54.0f;

    private Animator animationImage;

    private PlayerAttack attack;
    private PlayerEmote emote;
    private CarryEmote carryEmote;

    private Vector3 vec = Vector3.zero;

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

        isGroup = true;
        attack.OnIsCarry();

        animationImage.SetBool("Move", false);
        animationImage.SetBool("Carry", true);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Destroy(rigidbody);
        rb = GetComponentInParent<Rigidbody>();

    }

    public void RemoveItem()
    {
        if (isGroup)
        {
            carryEmote.CallEndCarryEmote();
            gameObject.transform.parent.GetComponent<PlayerController>().PlayerOutGroup(playerNo);
            gameObject.transform.parent = null;
            isEnterItem = false;
            isGroup = false;
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
        var leftStickValue = Gamepad.all[playerNo].leftStick.ReadValue();

        if (!isGroup)
        {
            NotIsGroup(leftStickValue);
        }

    }

    void NotIsGroup(Vector2 StickValue)
    {
        vec = Vector3.zero;

        if (!isAttack)
        {
            NotIsAttack(StickValue);
        }

        if (StickValue.x == 0.0f && StickValue.y == 0.0f)
        {
            animationImage.SetBool("Move", false);
        }

        rb.velocity = vec;
    }

    void NotIsAttack(Vector2 StickValue)
    {

        if (StickValue.x != 0.0f)
        {
            animationImage.SetBool("Move", true);
            vec.x = moveSpeed * Time.deltaTime * StickValue.x;
        }
        if (StickValue.y != 0.0f)
        {
            animationImage.SetBool("Move", true);
            vec.z = moveSpeed * Time.deltaTime * StickValue.y;
        }

        if (!isEnterItem)
        {
            if (StickValue.x != 0 || StickValue.y != 0)
            {
                var direction = new Vector3(StickValue.x, 0, StickValue.y);
                transform.localRotation = Quaternion.LookRotation(direction);
            }
        }
    }

    public void PlayerDamage()
    {
        isPlayerMoveDamage = true;
        isGroup = false;
        isAttack = false;
        isEnterItem = false;
    }

    public void NotPlayerDamage()
    {
        isPlayerMoveDamage = false;
        isGroup = false;
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
