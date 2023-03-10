using System;
using UnityEngine;

/// <summary>
/// ��C�̉^���Ɋւ���N���X
/// </summary>
public class CarryCannon : MonoBehaviour
{
    [SerializeField]
    private float defaultPosY = 51;

    [SerializeField]
    private float carryPosY = 60;

    [SerializeField]
    ItemSize myItemSize = ItemSize.Small;

    private BoxCollider boxCol = null;
    //private PlayerController playercontroller;
    private GroupManager groupManager;
    private GameObject[] myGrabPoint = null;
    private PlayerCarryDown[] playerCarryDowns = null;

    private int myItemSizeCount = 0;
    private int number = 0;
    private bool isGroup = false;

    public int GroupNumber = 1;

    enum ItemSize
    {
        Small,
        Medium,
        Large
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        //playercontroller = null;
        groupManager = null;
        Array.Resize(ref myGrabPoint, 0);
        Array.Resize(ref playerCarryDowns, myGrabPoint.Length);

        switch (myItemSize)
        {
            default:
                myItemSizeCount = (int)myItemSize;
                break;
        }
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
            //playercontroller = group.GetComponent<PlayerController>();
            groupManager = group.GetComponent<GroupManager>();

            if (group.transform.childCount <= 0)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    carryPosY,
                    this.gameObject.transform.position.z
                    );
                gameObject.transform.SetParent(group.gameObject.transform);
                //playercontroller = group.GetComponent<PlayerController>();
                //playercontroller.GetItemSize(myItemSizeCount, 2, this.gameObject);
                groupManager = group.GetComponent<GroupManager>();
                groupManager.GetItemSize(myItemSizeCount, 2, this.gameObject);

                isGroup = true;
                break;
            }
            else
            {
                GroupNumber += 1;
                if (GroupNumber > 4)
                {
                    GroupNumber = 1;
                }
                //playercontroller = null;
                groupManager = null;
            }
        }
    }

    /// <summary>
    /// ���g�̉^�����I�������ۂɌĂяo��
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
