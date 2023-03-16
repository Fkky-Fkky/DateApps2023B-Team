//�S����:�g�c����
using TMPro;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �Q�[���{�҂̎��ԕ\���Ɋւ���N���X
    /// </summary>
    public class TimeCount : MonoBehaviour
    {
        [SerializeField]
        private string sceneName = "New Scene";

        private TextMeshProUGUI timeCdTMP = null;
        private bool isMain = true;

        public static float SecondsCount = 0;
        private const int ONE_MINUTES_SECONDS = 60;

        // Start is called before the first frame update
        void Start()
        {
            timeCdTMP = GetComponent<TextMeshProUGUI>();
            SecondsCount = 0.0f;
            isMain = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isMain)
            {
                return;
            }
            SecondsCount += Time.deltaTime;
            timeCdTMP.text = ((int)(SecondsCount / ONE_MINUTES_SECONDS)).ToString("00") + ":" + ((int)SecondsCount % ONE_MINUTES_SECONDS).ToString("00");
        }

        /// <summary>
        /// �֐����Ăяo�����O���̃X�N���v�g�Ɍo�ߎ��Ԃ̏��𑗂�
        /// </summary>
        /// <returns></returns>
        public static float GetTime()
        {
            return SecondsCount;
        }
    }
}