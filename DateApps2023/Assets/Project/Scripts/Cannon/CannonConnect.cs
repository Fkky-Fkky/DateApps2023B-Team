using UnityEngine;

/// <summary>
/// 大砲が発射台に設置された時の処理をするクラス
/// </summary>
public class CannonConnect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem connectEffect = null;

    [SerializeField]
    private SEManager seManager = null;

    /// <summary>
    /// 大砲の設置されている場所
    /// </summary>
    public int ConnectingPos { get; private set; }

    /// <summary>
    /// 大砲が発射台に設置されているか
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
    /// 発射台に設置された時の処理
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
    /// 大砲が発射台から離れた時の処理
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
