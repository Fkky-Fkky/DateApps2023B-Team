// 担当者：吹上純平
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// 大砲の発射処理クラス
    /// </summary>
    public class CannonShot : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] shotSmokeEffects = new ParticleSystem[3];

        [SerializeField]
        private ParticleSystem[] shotChargeEffects = new ParticleSystem[3];

        [SerializeField]
        private ParticleSystem[] coolDownEffects = new ParticleSystem[2];

        [SerializeField]
        private EnergyCharge energyCharge = null;

        [SerializeField]
        private SEManager seManager = null;

        /// <summary>
        /// ビームが発射中か
        /// </summary>
        public bool IsShotting { get; private set; }

        /// <summary>
        /// ビームが発射されたか
        /// </summary>
        public bool IsNowShot { get; private set; }

        private int energyType = 0;
        private float coolTime = 0.0f;
        private float[] laserEndTime = new float[3];
        private bool isCoolTime = false;
        private AudioSource audioSource = null;

        private void Start()
        {
            const float SMALL_LASER_END_TIME  = 1.0f;
            const float MEDIUM_LASER_END_TIME = 1.5f;
            const float LARGE_LASER_END_TIME  = 2.0f;
            audioSource = GetComponent<AudioSource>();
            laserEndTime[(int)EnergyCharge.ENERGY_TYPE.SMALL] = SMALL_LASER_END_TIME;
            laserEndTime[(int)EnergyCharge.ENERGY_TYPE.MEDIUM] = MEDIUM_LASER_END_TIME;
            laserEndTime[(int)EnergyCharge.ENERGY_TYPE.LARGE] = LARGE_LASER_END_TIME;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isCoolTime)
            {
                return;
            }

            coolTime = Mathf.Max(coolTime - Time.deltaTime, 0.0f);

            if (coolTime > 0.0f)
            {
                return;
            }

            IsShotting = false;
            isCoolTime = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("BossAttack"))
            {
                return;
            }
            ShotCancel();
        }

        /// <summary>
        /// 発射処理
        /// </summary>
        public void Shot()
        {
            const float INVOKE_TIME = 2.0f;
            IsShotting = true;
            energyType = energyCharge.ChargeEnergyType;
            audioSource.PlayOneShot(seManager.GetCannonBeamSe(energyType));
            PlayShotChargeEffect();
            Invoke(nameof(PlayShotSmoke), INVOKE_TIME);
        }

        /// <summary>
        /// ビーム発射前のエフェクト再生
        /// </summary>
        private void PlayShotChargeEffect()
        {
            shotChargeEffects[energyType].gameObject.SetActive(true);
        }

        /// <summary>
        /// 発射時の煙エフェクト再生
        /// </summary>
        private void PlayShotSmoke()
        {
            const float MAX_COOL_TIME = 3.0f;
            shotSmokeEffects[energyType].gameObject.SetActive(true);
            coolTime = MAX_COOL_TIME;
            IsNowShot = true;
            isCoolTime = true;
            Invoke(nameof(LaserEnd), laserEndTime[energyType]);
            energyCharge.DisChargeEnergy(false);
        }

        /// <summary>
        /// ビームのエフェクト終了処理
        /// </summary>
        private void LaserEnd()
        {
            IsNowShot = false;
            PlayCoolDownEffects();
        }

        /// <summary>
        /// クールダウンエフェクト生成
        /// </summary>
        private void PlayCoolDownEffects()
        {
            for (int i = 0; i < coolDownEffects.Length; i++)
            {
                coolDownEffects[i].gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 発射キャンセル処理
        /// </summary>
        private void ShotCancel()
        {
            coolTime = 0.0f;
            IsShotting = false;
            isCoolTime = false;
            IsNowShot = false;
            shotChargeEffects[energyType].gameObject.SetActive(false);
            CancelInvoke();
            audioSource.Stop();
            PlayCoolDownEffects();
        }
    }
}