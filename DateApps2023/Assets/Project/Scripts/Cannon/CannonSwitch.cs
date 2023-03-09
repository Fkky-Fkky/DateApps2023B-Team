// �S���ҁF���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// ��C�̃X�C�b�`�N���X
    /// </summary>
    public class CannonSwitch : MonoBehaviour
    {
        [SerializeField]
        private EnergyCharge energyCharge = null;

        [SerializeField]
        private CannonShot cannonShot = null;

        [SerializeField]
        private CannonConnect cannonConnect = null;

        [SerializeField]
        private GameManager gameManager = null;

        private Vector3 defaultScale = Vector3.zero;
        private Vector3 switchOnScale = Vector3.zero;
        private GameObject button = null;
        private BoxCollider boxCollider = null;

        // Start is called before the first frame update
        void Start()
        {
            button = transform.GetChild(1).gameObject;
            defaultScale = button.transform.localScale;
            switchOnScale = new Vector3(0.0f, defaultScale.y, defaultScale.z);
            boxCollider = GetComponent<BoxCollider>();
            SwitchOn();
        }

        // Update is called once per frame
        void Update()
        {
            if (gameManager.IsGameOver)
            {
                SwitchOn();
            }

            if (!CanCannonShot())
            {
                SwitchOn();
            }
            else
            {
                SwitchOff();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("PlayerAttackPoint"))
            {
                return;
            }

            if (CanCannonShot())
            {
                cannonShot.Shot();
                SwitchOn();
            }
        }

        /// <summary>
        /// ��C�𔭎˂ł��邩��Ԃ�
        /// </summary>
        /// <returns>��C�𔭎˂ł��邩</returns>
        private bool CanCannonShot()
        {
            if (cannonShot.IsShotting)
            {
                return false;
            }

            if (!energyCharge.IsEnergyCharge)
            {
                return false;
            }

            if (!cannonConnect.IsConnect)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��C�̃X�C�b�`���I���ɂ���
        /// </summary>
        private void SwitchOn()
        {
            if (!boxCollider.enabled)
            {
                return;
            }
            boxCollider.enabled = false;
            button.transform.localScale = switchOnScale;
        }

        /// <summary>
        /// ��C�̃X�C�b�`���I�t�ɂ���
        /// </summary>
        private void SwitchOff()
        {
            if (boxCollider.enabled)
            {
                return;
            }
            boxCollider.enabled = true;
            button.transform.localScale = defaultScale;
        }
    }
}