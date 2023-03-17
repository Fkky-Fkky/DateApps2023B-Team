// 担当者：吹上純平
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// 大砲のスイッチクラス
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
        /// 大砲を発射できるかを返す
        /// </summary>
        /// <returns>大砲を発射できるか</returns>
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
        /// 大砲のスイッチをオンにする
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
        /// 大砲のスイッチをオフにする
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