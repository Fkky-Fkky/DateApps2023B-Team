using UnityEngine;

/// <summary>
/// ��C�����ˑ�ɐݒu���ꂽ���̏���������N���X
/// </summary>
public class CannonConnect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem connectEffect = null;

    [SerializeField]
    private SEManager seManager = null;

    /// <summary>
    /// ��C�̐ݒu����Ă���ꏊ
    /// </summary>
    public int ConnectingPos { get; private set; }

    /// <summary>
    /// ��C�����ˑ�ɐݒu����Ă��邩
    /// </summary>
    public bool IsConnect { get; private set; }

    private Transform standTransform = null;
    private AudioSource audioSource = null;
    private BoxCollider standCollision = null;

    private const float CANNON_POS_Y = -0.3f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!IsConnect)
        {
            return;
        }

        if (CANNON_POS_Y < transform.position.y)
        {
            CannonCut();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("CannonStand"))
        {
            return;
        }
        standCollision = other.gameObject.GetComponent<BoxCollider>();
        ConnectingPos  = other.GetComponent<CannonStand>().ConnectingPos;
        standTransform = other.transform;
        CannontConnect();
    }

    /// <summary>
    /// ���ˑ�ɐݒu���ꂽ���̏���
    /// </summary>
    private void CannontConnect()
    {
        IsConnect = true;
        standCollision.enabled = false;
        transform.rotation = standTransform.rotation;
        if (!connectEffect.gameObject.activeSelf)
        {
            connectEffect.gameObject.SetActive(true);
            audioSource.PlayOneShot(seManager.CannonConnectSe);
        }
    }

    /// <summary>
    /// ��C�����ˑ䂩�痣�ꂽ���̏���
    /// </summary>
    private void CannonCut()
    {
        IsConnect = false;
        standCollision.enabled = true;
        transform.rotation = Quaternion.identity;
        standTransform = null;
        ConnectingPos = (int)CannonStand.STAND_POSITION.NONE;
    }
}
