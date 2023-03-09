using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resistance
{
    public class DangerZoon : MonoBehaviour
    {
        public BossAttack bossAttack;

        private float time = 0.0f;
        private float effectTime;

        [SerializeField]
        private Renderer dangerRenderer;

        private float flashTime = 0.0f;
        private float flashTimeMax = 0.3f;

        void Start()
        {
            time = 0.0f;

            effectTime = bossAttack.BeamTimeMax();

        }

        void Update()
        {
            time += Time.deltaTime;
            if (time > effectTime)
            {
                Destroy(gameObject);
                time = 0.0f;
            }

            if (time > effectTime - 4.0f)
            {
                flashTime += Time.deltaTime;

                var repeatValue = Mathf.Repeat(flashTime, flashTimeMax);

                dangerRenderer.enabled = repeatValue >= flashTimeMax * 0.5f;
            }
        }
    }
}