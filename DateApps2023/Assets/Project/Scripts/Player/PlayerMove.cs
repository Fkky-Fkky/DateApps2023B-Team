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
    private float tempMoveSpeed = 0;
    [SerializeField]
    private float slowMoveSpeed = 150.0f;

    private int playerNo;

    Rigidbody rb;

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

    private bool playerMoveDamage = false;
    private float defaultPosY = 54.0f;

    private Animator AnimationImage;

    //[SerializeField]
    //private Vector2 rimitPosX = Vector2.zero;
    //[SerializeField]
    //private Vector2 rimitPosZ = Vector2.zero;
    //[SerializeField]
    //private Vector2 respownPos = Vector2.zero;

    //float time = 0;
    //[SerializeField]
    //private float respawnTime = 1.0f;
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

        tempMoveSpeed = moveSpeed;

        defaultPosY = this.gameObject.transform.position.y;
    }

    //void Update()
    //{
    //    if(this.gameObject.transform.position.x < rimitPosX.x || this.gameObject.transform.position.x > rimitPosX.y)
    //    {
    //        time += Time.deltaTime;
    //        if(time > respawnTime)
    //        {
    //            this.gameObject.transform.position = new Vector3(respownPos.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    //            time = 0;
    //        }
    //    }
    //    if (this.gameObject.transform.position.z < rimitPosZ.x || this.gameObject.transform.position.z > rimitPosZ.y)
    //    {
    //        time += Time.deltaTime;
    //        if (time > respawnTime)
    //        {
    //            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, respownPos.y);
    //            time = 0;
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        GamepadMove();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("item")
           || collision.gameObject.CompareTag("item2")
           || collision.gameObject.CompareTag("item3")
           || collision.gameObject.CompareTag("item4")
           || collision.gameObject.CompareTag("Group")
           )
        {
            EnterItem = true;
            moveSpeed = slowMoveSpeed;
        }
        if (collision.gameObject.CompareTag("CloneSabotageItem"))
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
           || collision.gameObject.CompareTag("CloneSabotageItem")
           || collision.gameObject.CompareTag("Group")
           )
        {
            EnterItem = false;
            moveSpeed = tempMoveSpeed;
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

        rb = this.gameObject.AddComponent<Rigidbody>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        this.gameObject.transform.position = new Vector3(
                   this.gameObject.transform.position.x,
                   defaultPosY,
                   this.gameObject.transform.position.z);

        InGroup = false;
        AnimationImage.SetBool("Carry", false);
        AnimationImage.SetBool("CarryMove", false);


    }

    void GamepadMove()
    {
        if (!playerMoveDamage)
        {
            if (!InGroup)
            {
                var leftStickValue = Gamepad.all[playerNo].leftStick.ReadValue();
                Vector3 vec = new Vector3(0, 0, 0);

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

                if(leftStickValue.x == 0.0f && leftStickValue.y == 0.0f)
                {
                    AnimationImage.SetBool("Move", false);
                }

                rb.velocity = vec;

                if (!EnterItem)
                {
                    moveSpeed = tempMoveSpeed;
                    if (leftStickValue.x != 0 || leftStickValue.y != 0)
                    {
                        var direction = new Vector3(leftStickValue.x, 0, leftStickValue.y);
                        transform.localRotation = Quaternion.LookRotation(direction);
                    }
                }
                
            }
        }
    }

    public void PlayerDamage()
    {
        playerMoveDamage = true;
        InGroup = false;
        EnterItem = false;
    }

    public void NotPlayerDamage()
    {
        playerMoveDamage = false;
        InGroup = false;
        EnterItem = false;
    }

}
