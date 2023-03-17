//�S����:�g�c����
using UnityEngine;
using UnityEngine.InputSystem;

namespace Resistance
{
    /// <summary>
    /// �^�����̈ړ��������A�v���C���[�̈ړ��Ɋւ���N���X
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("�ړ��̑���")]
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
        /// �v���C���[�̔ԍ�
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
        /// �v���C���[�������E��C���������ۂɌĂяo��
        /// </summary>
        /// <param name="groupNo">�O���[�v�ԍ�</param>
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
        /// �v���C���[�ƕ����E��C�����ꂽ�ۂɌĂяo��
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
        /// �v���C���[�̔ԍ��ɉ������Q�[���p�b�h�̓��͂��擾����
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
        /// �Q�[���p�b�h�̓��͂ɂ���Ĉړ����s��
        /// </summary>
        /// <param name="StickValue">�R���g���[���[���X�e�B�b�N�̓���</param>
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
        /// �X�e�B�b�N���̗͂ʂɉ������ړ��ʂ��Z�o����
        /// �X�e�B�b�N�̌X���ɉ����ăv���C���[�̌��������߂�
        /// </summary>
        /// <param name="StickValue">�R���g���[���[���X�e�B�b�N�̓���</param>
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
        /// �v���C���[���_���[�W���󂯂��ۂɌĂяo��
        /// </summary>
        public void PlayerDamage()
        {
            isGroup = false;
            isAttack = false;
            isEnterItem = false;
            isDamage = true;
        }

        /// <summary>
        /// �v���C���[�̋C��E�S�����I�������ۂɌĂяo��
        /// </summary>
        public void NotPlayerDamage()
        {
            isGroup = false;
            isAttack = false;
            isEnterItem = false;
            isDamage = false;
        }

        /// <summary>
        /// �v���C���[���A�^�b�N���J�n�����ۂɌĂяo��
        /// </summary>
        public void StartAttack()
        {
            isAttack = true;
        }

        /// <summary>
        /// �v���C���[�̃A�^�b�N���I�������ۂɌĂяo��
        /// </summary>
        public void EndAttack()
        {
            isAttack = false;
        }

        /// <summary>
        /// CarryEmote�̊J�n���O������X�ɌĂяo�����߂̏���
        /// </summary>
        public void StartCarryEmote()
        {
            carryEmote.CallStartCarryEmote();
        }

        /// <summary>
        /// CarryEmote�̏I�����O������X�ɌĂяo�����߂̏���
        /// </summary>
        public void EndCarryEmote()
        {
            carryEmote.CallEndCarryEmote();
        }

    }
}