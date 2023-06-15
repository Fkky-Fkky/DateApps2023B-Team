// �S���ҁF���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �G�l���M�[�����̗��������N���X
    /// </summary>
    public class FallEnergy : MonoBehaviour
    {
        private bool isEnergyResourceLanded = false;
        private Transform transformCache = null;

        private const float FALL_SPEED = 15.0f;

        private void Awake()
        {
            transformCache = transform;
        }

        private void OnEnable()
        {
            const float SRART_POSITION_Y = 20.0f;
            isEnergyResourceLanded = false;
            Vector3 position = transformCache.position;
            position.y = SRART_POSITION_Y;
            transformCache.position = position;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnergyResourceLanded)
            {
                return;
            }
            Fall();
        }

        /// <summary>
        /// �G�l���M�[�����𗎉�������
        /// </summary>
        private void Fall()
        {
            Vector3 position = transformCache.position;
            position.y = Mathf.Max(position.y - FALL_SPEED * Time.deltaTime, 0.0f);
            transformCache.position = position;
            if (position.y <= 0.0f)
            {
                isEnergyResourceLanded = true;
            }
        }
    }
}