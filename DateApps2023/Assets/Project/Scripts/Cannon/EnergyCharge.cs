// 担当者：吹上純平
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// エネルギーのチャージ処理をするクラス
    /// </summary>
    public class EnergyCharge : MonoBehaviour
    {
        [SerializeField]
        private CannonEffectManager effectManager = null;

        [SerializeField]
        private SEManager seManager = null;

        [SerializeField]
        private EnergyGenerator generateEnergy = null;

        [SerializeField]
        private GameManager gameManager = null;

        [SerializeField]
        private GameObject cannonLaser = null;

        private float coolTime = 0.0f;
        private bool isSetCoolTime = false;
        private Vector3[] laserScale = new Vector3[3];
        private BoxCollider boxCol = null;
        private AudioSource audioSource = null;

        /// <summary>
        /// エネルギーがチャージされているかを返す
        /// </summary>
        public bool IsEnergyCharge { get; private set; }

        /// <summary>
        /// チャージされたエネルギーの種類を返す
        /// </summary>
        public int ChargeEnergyType { get; private set; }

        /// <summary>
        /// エネルギーの種類
        /// </summary>
        public enum ENERGY_TYPE
        {
            SMALL,
            MEDIUM,
            LARGE,
        }

        private void Start()
        {
            const float SMALL_LASER_SCALE = 0.3f;
            const float MEDIUM_LASER_SCALE = 1.0f;
            const float LARGE_LASER_SCALE = 1.5f;
            boxCol = GetComponent<BoxCollider>();
            audioSource = transform.parent.GetComponent<AudioSource>();
            IsEnergyCharge = false;
            laserScale[(int)ENERGY_TYPE.SMALL] = new Vector3(SMALL_LASER_SCALE, SMALL_LASER_SCALE, SMALL_LASER_SCALE);
            laserScale[(int)ENERGY_TYPE.MEDIUM] = new Vector3(MEDIUM_LASER_SCALE, MEDIUM_LASER_SCALE, MEDIUM_LASER_SCALE);
            laserScale[(int)ENERGY_TYPE.LARGE] = new Vector3(LARGE_LASER_SCALE, LARGE_LASER_SCALE, LARGE_LASER_SCALE);
        }

        private void Update()
        {
            if (!isSetCoolTime)
            {
                return;
            }

            coolTime = Mathf.Max(coolTime - Time.deltaTime, 0.0f);
            if (coolTime <= 0.0f)
            {
                isSetCoolTime = false;
                boxCol.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("item"))
            {
                return;
            }

            if (other.transform.parent == null)
            {
                return;
            }

            int itemSize = other.GetComponent<CarryEnergy>().MyItemSizeCount;
            switch (itemSize)
            {
                case (int)CarryEnergy.ItemSize.Small:
                    ChargeEnergyType = (int)ENERGY_TYPE.SMALL;
                    break;

                case (int)CarryEnergy.ItemSize.Medium:
                    ChargeEnergyType = (int)ENERGY_TYPE.MEDIUM;
                    break;

                case (int)CarryEnergy.ItemSize.Large:
                    ChargeEnergyType = (int)ENERGY_TYPE.LARGE;
                    break;
            }
            other.GetComponent<CarryEnergy>().DestroyMe();
            ChargeEnergy();
        }

        /// <summary>
        /// エネルギーをチャージする
        /// </summary>
        private void ChargeEnergy()
        {
            IsEnergyCharge = true;
            effectManager.GetEnergyChargeEffect(ChargeEnergyType).gameObject.SetActive(true);
            audioSource.PlayOneShot(seManager.EnergyChargeSe);
            if (gameManager.IsGameStart)
            {
                generateEnergy.GenerateEnergy();
            }
            cannonLaser.transform.localScale = laserScale[ChargeEnergyType];
            boxCol.enabled = false;
        }

        /// <summary>
        /// エネルギーを減らす
        /// </summary>
        /// <param name="isDamage">ダメージを受けているか</param>
        public void DisChargeEnergy(bool isDamage)
        {
            const float COOL_TIME_MAX = 3.0f;
            const float DAMAGE_COOL_TIME_MAX = 5.0f;
            float addCoolTime = COOL_TIME_MAX;
            if (isDamage)
            {
                addCoolTime = DAMAGE_COOL_TIME_MAX;
                effectManager.CannonDamageEffect.gameObject.SetActive(true);
                effectManager.CannonDamageSmokeEffect.gameObject.SetActive(true);
            }
            IsEnergyCharge = false;
            if (isSetCoolTime)
            {
                return;
            }
            isSetCoolTime = true;
            coolTime = addCoolTime;
        }
    }
}