// �S���ҁF���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �Q�[���I�[�o�[���̔�������
    /// </summary>
    public class GameOverDirection : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] explosionEffects = new ParticleSystem[5];

        private int effectIndex = 0;
        private float generateTime = 0.0f;
        private GameManager gameManager = null;

        const float MAX_GENERATE_TIME = 0.5f;

        // Start is called before the first frame update
        void Start()
        {
            generateTime = 0.0f;
            gameManager  = GetComponentInParent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!gameManager.IsGameOver)
            {
                return;
            }

            generateTime -= Time.deltaTime;
            if (generateTime <= 0.0f)
            {
                PlayExplosionEffect();
                generateTime = MAX_GENERATE_TIME;
            }
        }

        /// <summary>
        /// �����G�t�F�N�g���Đ�����
        /// </summary>
        private void PlayExplosionEffect()
        {
            if (explosionEffects[effectIndex].gameObject.activeSelf)
            {
                return;
            }
            explosionEffects[effectIndex].gameObject.SetActive(true);
            effectIndex++;
            if(effectIndex >= explosionEffects.Length)
            {
                effectIndex = 0;
            }
        }
    }
}