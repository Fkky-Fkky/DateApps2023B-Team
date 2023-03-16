//�S����:�g�c����
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �v���C���[�̉^���Ɋւ���N���X
/// </summary>
public class PlayerCarryDown : MonoBehaviour
{
    private PlayerMove playermove = null;
    private CarryEnergy energyItem = null;
    private CarryCannon cannonItem = null;

    private GameObject carryItem = null;
    private Rigidbody rb = null;
    private BoxCollider myCol = null;

    private int myPlayerNo = 5;
    private int myGroupNo = 1;

    private bool isCarry = false;
    private bool isCanUsed = false;
    private bool isCarryDamage = false;

    void Start()
    {
        playermove = GetComponentInParent<PlayerMove>();
        energyItem = null;
        cannonItem = null;

        carryItem = null;
        rb = GetComponentInParent<Rigidbody>();
        myCol = GetComponent<BoxCollider>();

        myGroupNo = 1;
        isCarry = false;
        isCanUsed = false;
        isCarryDamage = false;
    }

    void Update()
    {
        if (!isCarryDamage)
        {
            if (Gamepad.all[myPlayerNo].bButton.wasPressedThisFrame)
            {
                OnPressCarryButton();
            }
            if (Gamepad.all[myPlayerNo].bButton.wasReleasedThisFrame)
            {
                OnReleaseCarryButton();
            }
        }

        if (isCarry)
        {
            myCol.enabled = false;
        }
        if (carryItem == null)
        {
            isCarry = false;
            isCanUsed = false;
            myCol.enabled = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (isCarry)
        {
            return;
        }
        if (collision.gameObject.CompareTag("item")
                || collision.gameObject.CompareTag("Cannon"))
        {
            isCanUsed = true;
            carryItem = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (isCarry)
        {
            return;
        }
        if (collision.gameObject.CompareTag("item")
                || collision.gameObject.CompareTag("Cannon"))
        {
            isCanUsed = false;
            carryItem = null;
        }
    }

    /// <summary>
    /// �v���C���[���^���{�^����������ۂ̏�����s��
    /// </summary>
    void OnPressCarryButton()
    {
        if (isCarry)
        {
            return;
        }
        if (isCanUsed)
        {
            CheckItemTag();
        }
    }

    /// <summary>
    /// �v���C���[���^���{�^���𗣂����ۂ̏�����s��
    /// </summary>
    void OnReleaseCarryButton()
    {
        if (isCarry)
        {
            CarryCancel();
        }
    }

    /// <summary>
    /// ���Ƃ��Ƃ��Ă���I�u�W�F�N�g�̎�ނ𔻒肷��
    /// </summary>
    void CheckItemTag()
    {
        if (carryItem.CompareTag("item"))
        {
            energyItem = carryItem.GetComponent<CarryEnergy>();
            energyItem.GetGrabPoint(this.gameObject);
            myGroupNo = energyItem.GroupNumber;
            isCarry = true;
            isCanUsed = false;
            playermove.GetItem(myGroupNo);
        }
        if (carryItem.CompareTag("Cannon"))
        {
            if (carryItem.GetComponent<CannonShot>().IsShotting)
            {
                return;
            }
            cannonItem = carryItem.GetComponent<CarryCannon>();
            cannonItem.GetGrabPoint(this.gameObject);
            myGroupNo = cannonItem.GroupNumber;
            isCarry = true;
            isCanUsed = false;
            playermove.GetItem(myGroupNo);
        }
    }

    /// <summary>
    /// �v���C���[���^����I������ۂ̏�����s��
    /// </summary>
    public void CarryCancel()
    {
        playermove.RemoveItem();
        rb = GetComponentInParent<Rigidbody>();

        isCarry = false;
        isCanUsed = false;
        carryItem = null;
        energyItem = null;
        cannonItem = null;
        myCol.enabled = true;
    }

    /// <summary>
    /// ���g�̃v���C���[�ԍ���擾����
    /// </summary>
    /// <param name="parentNumber">�v���C���[�ԍ�</param>
    public void GetPlayerNo(int parentNumber)
    {
        myPlayerNo = parentNumber;
    }

    /// <summary>
    /// �v���C���[���_���[�W��󂯂��ۂɌĂяo��
    /// </summary>
    public void OnCarryDamage()
    {
        isCarryDamage = true;
    }

    /// <summary>
    /// �v���C���[���_���[�W����񕜂����ۂɌĂяo��
    /// </summary>
    public void OffCarryDamage()
    {
        isCarryDamage = false;
    }
}
