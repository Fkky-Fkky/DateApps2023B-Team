//�S����:���c��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �U��������G���A���f�X�g���C
    /// </summary>
    public class DangerZoon : MonoBehaviour
    {
        [SerializeField]
        private Renderer dangerRenderer = null;

        public BossAttack BossAttack = null;

        private float time = 0.0f;
        private float effectTime = 0.0f;

        private float flashTime    = 0.0f;
        private float maxFlashTime = 0.3f;

        const float REMAINING_SECONDS = 4.0f;
        const float HALF = 0.5f;

        void Start()
        {
            effectTime = BossAttack.BeamTimeMax();
        }

        void Update()
        {
            time += Time.deltaTime;
            if (time > effectTime)
            {
                Destroy(gameObject);
                time = 0.0f;
            }

            if (time > effectTime - REMAINING_SECONDS)
            {
                flashTime += Time.deltaTime;
                maxFlashTime -= 0.015f * Time.deltaTime;


                var repeatValue = Mathf.Repeat(flashTime, maxFlashTime);

                dangerRenderer.enabled = repeatValue >= maxFlashTime * HALF;
            }
        }
    }
}