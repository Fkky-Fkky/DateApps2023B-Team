// ’S“–ÒFãƒ•½
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// ‘å–C‚ª”­Ë‘ä‚Éİ’u‚³‚ê‚½‚Ìˆ—‚ğ‚·‚éƒNƒ‰ƒX
    /// </summary>
    public class CannonConnect : MonoBehaviour
    {
        [SerializeField]
        private CannonEffectManager effectManager = null;

        [SerializeField]
        private SEManager seManager = null;

        /// <summary>
        /// ‘å–C‚Ìİ’u‚³‚ê‚Ä‚¢‚éêŠ
        /// </summary>
        public int ConnectingPos { get; private set; }

        /// <summary>
        /// ‘å–C‚ª”­Ë‘ä‚Éİ’u‚³‚ê‚Ä‚¢‚é‚©
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
            ConnectingPos = other.GetComponent<CannonStand>().ConnectingPos;
            standTransform = other.transform;
            CannontConnect();
        }

        /// <summary>
        /// ”­Ë‘ä‚Éİ’u‚³‚ê‚½‚Ìˆ—
        /// </summary>
        private void CannontConnect()
        {
            IsConnect = true;
            standCollision.enabled = false;
            transform.rotation = standTransform.rotation;
            if (!effectManager.ConnectEffect.gameObject.activeSelf)
            {
                effectManager.ConnectEffect.gameObject.SetActive(true);
                audioSource.PlayOneShot(seManager.CannonConnectSe);
            }
        }

        /// <summary>
        /// ‘å–C‚ª”­Ë‘ä‚©‚ç—£‚ê‚½‚Ìˆ—
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
}