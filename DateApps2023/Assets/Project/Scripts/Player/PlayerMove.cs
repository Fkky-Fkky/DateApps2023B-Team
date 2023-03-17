//担当者:吉田理紗
using UnityEngine;
using UnityEngine.InputSystem;

namespace Resistance
{
    /// <summary>
    /// 運搬時の移動を除く、プレイヤーの移動に関するクラス
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("移動の速さ")]
        private float moveSpeed = 2000.0f;

        [SerializeField]
        PlayerNumber playerNumber = PlayerNumber.None;

        private PlayerCarryDown carryDown = null;
        private PlayerAttack attack = null;
        private PlayerEmote emote = null;
        private CarryEmote carryEmote = null;

        private Rigidbody rb = null;
        private Animator animationImage = null;
        private GameObject enterItem = null;

        private int playerNo = 5;
        private float defaultPosY = 54.0f;

        private bool isGroup = false;
        private bool isEnterItem = false;
        private bool isAttack = false;
        private bool isDamage = false;

        private Vector3 vec = Vector3.zero;

        /// <summary>
        /// プレイヤーの番号
        /// </summary>
        public enum PlayerNumber
        {
            PL_1P,
            PL_2P,
            PL_3P,
            PL_4P,
            None
        }

        // Start is called before the first frame update
        void Start()
        {
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
            GetComponent<PlayerDamage>().GetPlayerNo(playerNo);

            rb = GetComponent<Rigidbody>();
            animationImage = GetComponent<Animator>();
            enterItem = null;
            defaultPosY = this.gameObject.transform.position.y;

            isGroup = false;
            isEnterItem = false;
            isAttack = false;
            isDamage = false;

            vec = Vector3.zero;
        }

        private void Update()
        {
            if (!isEnterItem)
            {
                return;
            }
            if (enterItem == null)
            {
                isEnterItem = false;
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

        /// <summary>
        /// プレイヤーが物資・大砲を持った際に呼び出す
        /// </summary>
        /// <param name="groupNo">グループ番号</param>
        public void GetItem(int groupNo)
        {
            GameObject group = GameObject.Find("Group" + groupNo);
            gameObject.transform.SetParent(group.gameObject.transform);
            group.GetComponent<GroupMove>().GetMyNo(playerNo, this.gameObject);

            isGroup = true;
            attack.OnIsCarry();

            animationImage.SetBool("Move", false);
            animationImage.SetBool("Carry", true);

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            Destroy(rigidbody);
            rb = GetComponentInParent<Rigidbody>();

        }

        /// <summary>
        /// プレイヤーと物資・大砲が離れた際に呼び出す
        /// </summary>
        public void RemoveItem()
        {
            if (isGroup)
            {
                carryEmote.CallEndCarryEmote();
                gameObject.transform.parent.GetComponent<GroupMove>().PlayerOutGroup(playerNo);
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

        /// <summary>
        /// プレイヤーの番号に応じたゲームパッドの入力を取得する
        /// </summary>
        void GamepadMove()
        {
            var leftStickValue = Gamepad.all[playerNo].leftStick.ReadValue();

            if (!isGroup)
            {
                NotIsGroup(leftStickValue);
            }
        }

        /// <summary>
        /// ゲームパッドの入力によって移動を行う
        /// </summary>
        /// <param name="StickValue">コントローラー左スティックの入力</param>
        void NotIsGroup(Vector2 StickValue)
        {
            vec = Vector3.zero;

            if (!isAttack && !isDamage)
            {
                NotIsAttack(StickValue);
            }

            if (StickValue.x == 0.0f && StickValue.y == 0.0f)
            {
                animationImage.SetBool("Move", false);
            }

            rb.velocity = vec;
        }

        /// <summary>
        /// スティック入力の量に応じた移動量を算出する
        /// スティックの傾きに応じてプレイヤーの向きを決める
        /// </summary>
        /// <param name="StickValue">コントローラー左スティックの入力</param>
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

        /// <summary>
        /// プレイヤーがダメージを受けた際に呼び出す
        /// </summary>
        public void PlayerDamage()
        {
            isGroup = false;
            isAttack = false;
            isEnterItem = false;
            isDamage = true;
        }

        /// <summary>
        /// プレイヤーの気絶・拘束が終了した際に呼び出す
        /// </summary>
        public void NotPlayerDamage()
        {
            isGroup = false;
            isAttack = false;
            isEnterItem = false;
            isDamage = false;
        }

        /// <summary>
        /// プレイヤーがアタックを開始した際に呼び出す
        /// </summary>
        public void StartAttack()
        {
            isAttack = true;
        }

        /// <summary>
        /// プレイヤーのアタックが終了した際に呼び出す
        /// </summary>
        public void EndAttack()
        {
            isAttack = false;
        }

        /// <summary>
        /// CarryEmoteの開始を外部から更に呼び出すための処理
        /// </summary>
        public void StartCarryEmote()
        {
            carryEmote.CallStartCarryEmote();
        }

        /// <summary>
        /// CarryEmoteの終了を外部から更に呼び出すための処理
        /// </summary>
        public void EndCarryEmote()
        {
            carryEmote.CallEndCarryEmote();
        }

    }
}