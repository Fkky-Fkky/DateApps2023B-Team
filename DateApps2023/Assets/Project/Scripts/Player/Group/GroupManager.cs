using TMPro;
using UnityEngine;

/// <summary>
/// �^�����̃O���[�v�z���̃I�u�W�F�N�g�����Ɋւ���N���X
/// </summary>
public class GroupManager : MonoBehaviour
{
    [SerializeField]
    private int carryTextOrderInLayer = 0;

    private Rigidbody rb = null;
    private GroupMove groupMove = null;
    private TextMeshPro carryText = null;
    private Outline outline = null;

    private float defaultMass = 1.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        rb.useGravity = false;
        defaultMass = rb.mass;

        groupMove = GetComponent<GroupMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.CompareTag("Player"))
                {
                    transform.GetChild(i).gameObject.GetComponent<PlayerDamage>().JudgeCapture(other.gameObject);
                }
            }
        }

        if (other.gameObject.CompareTag("BossAttack"))
        {
            DamageChild();
        }
    }

    /// <summary>
    /// �O���[�v�z���̃I�u�W�F�N�g��������c���Ă��Ȃ����𔻒肷��
    /// �c���Ă����ꍇ�̓^�O�ŋ�ʂ��ăI�u�W�F�N�g�̊֐����Ăяo��
    /// </summary>
    public void CheckOnlyChild()
    {
        if (transform.childCount <= 1)
        {
            ItemOutGroup();
            AllFragFalse();
        }
    }

    /// <summary>
    /// �^������A�C�e���̏����擾����
    /// �A�C�e�����O���[�v�z���ɓ���ۂɌĂяo��
    /// </summary>
    /// <param name="itemSize">�A�C�e���̃T�C�Y(�d��)</param>
    /// <param name="itemType">�A�C�e���̃^�C�v�@1=�G�l���M�[����,2=��C</param>
    /// <param name="gameObject">�A�C�e���̃Q�[���I�u�W�F�N�g</param>
    public void GetItemSize(int itemSize, int itemType, GameObject gameObject)
    {
        groupMove.SetItenSizeCount(itemSize);

        carryText = gameObject.GetComponentInChildren<TextMeshPro>();
        carryText.gameObject.GetComponent<MeshRenderer>().sortingOrder = carryTextOrderInLayer;
        outline = gameObject.GetComponentInChildren<Outline>();
        outline.enabled = false;
        if (itemType == 2)
        {
            rb.mass *= 10;
        }
        groupMove.CheckPlayerCount();
    }

    /// <summary>
    /// �O���[�v���O���[�v�z���̃I�u�W�F�N�g�𗣂��ۂɌĂяo��
    /// </summary>
    public void ReleaseChild()
    {
        groupMove.SetNullPlayer();
        AllFragFalse();
    }

    /// <summary>
    /// �^�����̃O���[�v���_���[�W���󂯂��ۂɌĂяo��
    /// �O���[�v�z���̃I�u�W�F�N�g�𗣂�
    /// </summary>
    private void DamageChild()
    {
        groupMove.SetNullPlayer();
        ItemOutGroup();
        AllFragFalse();
    }

    /// <summary>
    /// �O���[�v�z���̃A�C�e���̃O���[�v����ʂ���֐����Ăяo��
    /// </summary>
    void ItemOutGroup()
    {
        if (transform.childCount >= 1)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.CompareTag("item"))
                {
                    transform.GetChild(i).gameObject.GetComponent<CarryEnergy>().OutGroup();
                }
                else if (transform.GetChild(i).gameObject.CompareTag("Cannon"))
                {
                    transform.GetChild(i).gameObject.GetComponent<CarryCannon>().OutGroup();
                }
            }
        }
    }

    /// <summary>
    /// �^�����̃O���[�v���^�����I������ۂɌĂяo��
    /// </summary>
    void AllFragFalse()
    {
        groupMove.FalseGamepad();
        outline.enabled = true;
        outline = null;
        groupMove.CheckPlayerCount();
        carryText.text = null;
        carryText = null;
        rb.mass = defaultMass;
        rb.velocity = Vector3.zero;
    }

    /// <summary>
    /// �^�����̃e�L�X�g�\���Ɋւ��鏈�����s��
    /// </summary>
    /// <param name="playerCount">�O���[�v�z���̃v���C���[�̐l��</param>
    /// <param name="needCarryCount">�^���ɕK�v�ȃv���C���[�̐l��</param>
    public void CheckCarryText(int playerCount,int needCarryCount)
    {
        if(carryText != null)
        {
            carryText.text = playerCount.ToString("0") + "/" + needCarryCount.ToString("0");
            CheckNeedCarryText(playerCount, needCarryCount);
        }
        
    }

    /// <summary>
    /// �^������UI�\���Ɋւ��鏈�����s��
    /// </summary>
    /// <param name="playerCount">�O���[�v�z���̃v���C���[�̐l��</param>
    /// <param name="needCarryCount">�^���ɕK�v�ȃv���C���[�̐l��</param>
    void CheckNeedCarryText(int playerCount, int needCarryCount)
    {
        if (playerCount >= needCarryCount)
        {
            carryText.color = Color.white;
            for (int i = 0; i < groupMove.ChildPlayer.Length; i++)
            {
                if (groupMove.ChildPlayer[i] != null)
                {
                    groupMove.ChildPlayer[i].GetComponent<PlayerMove>().EndCarryEmote();
                }
            }
        }
        else if (playerCount <= 0)
        {
            carryText.text = null;
        }
        else
        {
            carryText.color = Color.red;
            for (int i = 0; i < groupMove.ChildPlayer.Length; i++)
            {
                if (groupMove.ChildPlayer[i] != null)
                {
                    groupMove.ChildPlayer[i].GetComponent<PlayerMove>().StartCarryEmote();
                }
            }
        }
    }
}