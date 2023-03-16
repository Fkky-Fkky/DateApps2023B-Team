//�S����:�g�c����
using System;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �G�l���M�[�����̉^���Ɋւ���N���X
    /// </summary>
    public class CarryEnergy : MonoBehaviour
    {
        [SerializeField]
        private float defaultPosY = 51;

        [SerializeField]
        private float carryPosY = 60;

        [SerializeField]
        ItemSize myItemSize = ItemSize.Small;

        private BoxCollider boxCol = null;
        private GroupManager groupManager = null;
        private GameObject[] myGrabPoint = null;
        private PlayerCarryDown[] playerCarryDowns = null;

        private int number = 0;
        private bool isGroup = false;

        private const int MAX_GROUP_NUMBER = 4;
        private const int FIRST_GROUP_NUMBER = 1;

        public int GroupNumber = 1;

        /// <summary>
        /// �A�C�e���̑傫��
        /// </summary>
        public enum ItemSize
        {
            Small,
            Medium,
            Large
        }

        public int MyItemSizeCount
        {
            get { return (int)myItemSize; }
            private set { MyItemSizeCount = (int)myItemSize; }
        }

        // Start is called before the first frame update
        void Start()
        {
            boxCol = GetComponent<BoxCollider>();
            groupManager = null;
            Array.Resize(ref myGrabPoint, 0);
            Array.Resize(ref playerCarryDowns, myGrabPoint.Length);

            number = 0;
            isGroup = false;

            GroupNumber = 1;
        }

        /// <summary>
        /// �v���C���[�����g�̉^�����J�n�����ۂɌĂяo��
        /// </summary>
        /// <param name="thisGrabPoint">�v���C���[�̒͂݃|�C���g�̃Q�[���I�u�W�F�N�g</param>
        public void GetGrabPoint(GameObject thisGrabPoint)
        {
            Array.Resize(ref myGrabPoint, myGrabPoint.Length + 1);
            Array.Resize(ref playerCarryDowns, myGrabPoint.Length);
            myGrabPoint[number] = thisGrabPoint;
            playerCarryDowns[number] = thisGrabPoint.GetComponent<PlayerCarryDown>();
            number++;

            boxCol.isTrigger = false;

            while (!isGroup)
            {
                GameObject group = GameObject.FindWithTag("Group" + GroupNumber);
                groupManager = group.GetComponent<GroupManager>();

                if (group.transform.childCount <= 0)
                {
                    this.gameObject.transform.position = new Vector3(
                        this.gameObject.transform.position.x,
                        carryPosY,
                        this.gameObject.transform.position.z
                        );
                    gameObject.transform.SetParent(group.gameObject.transform);
                    groupManager = group.GetComponent<GroupManager>();
                    groupManager.GetItemSize(MyItemSizeCount, this.gameObject);

                    isGroup = true;
                    break;
                }
                else
                {
                    GroupNumber += FIRST_GROUP_NUMBER;
                    if (GroupNumber > MAX_GROUP_NUMBER)
                    {
                        GroupNumber = FIRST_GROUP_NUMBER;
                    }
                    groupManager = null;
                }
            }
        }

        /// <summary>
        /// ���g�̉^�����r���ŏI�������ۂɌĂяo��
        /// </summary>
        public void OutGroup()
        {
            isGroup = false;
            gameObject.transform.parent = null;
            DoCarryCancel();

            this.gameObject.transform.position = new Vector3(
                this.gameObject.transform.position.x,
                defaultPosY,
                this.gameObject.transform.position.z
                );
            Array.Resize(ref myGrabPoint, 0);
            Array.Resize(ref playerCarryDowns, myGrabPoint.Length);
            number = 0;
        }

        /// <summary>
        /// �^�����̎��g����C�ƐڐG�����ۂɌĂяo��
        /// </summary>
        public void DestroyMe()
        {
            groupManager.ReleaseChild();

            DoCarryCancel();
            Destroy(gameObject);
        }

        /// <summary>
        /// ���g���^�����Ă����v���C���[�̊֐����Ăяo���������s��
        /// </summary>
        public void DoCarryCancel()
        {
            for (int i = 0; i < myGrabPoint.Length; i++)
            {
                playerCarryDowns[i].CarryCancel();
            }
        }
    }
}