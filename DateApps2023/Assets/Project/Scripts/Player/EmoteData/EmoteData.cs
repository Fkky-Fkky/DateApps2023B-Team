//�S����:�g�c����
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �G���[�g�֘A�̃X�N���v�g�ɂ����āA�ꕔ�̕ϐ���ݒ肷��N���X
    /// �쐬����ScriptableObject���A�Ώۂ�Inspecter�ɃA�^�b�`���Ďg��
    /// </summary>
    [CreateAssetMenu(menuName = "CreateData/EmoteData", fileName = "EmoteData")]
    public class EmoteData : ScriptableObject
    {
        [SerializeField]
        private float startTime = 0.4f;
        [SerializeField]
        private float endTime = 0.4f;
        [SerializeField]
        private float moveY = 0.2f;
        [SerializeField]
        private float smallTime = 0.4f;
        [SerializeField]
        private float bigTime = 0.4f;
        [SerializeField]
        private float sizeChange = 0.2f;
        [SerializeField]
        private float startSizeChange = 0.2f;

        /// <summary>
        /// �J�n���̎���
        /// </summary>
        public float StartTime { get { return startTime; } }

        /// <summary>
        /// �I�����܂ł̎���
        /// </summary>
        public float EndTime { get { return endTime; } }

        /// <summary>
        /// �J�n���ƏI�����ɏ㏸����ړ���
        /// </summary>
        public float MoveY { get { return moveY; } }

        /// <summary>
        /// �k������܂ł̎���
        /// </summary>
        public float SmallTime { get { return smallTime; } }

        /// <summary>
        /// �g�傷��܂ł̎���
        /// </summary>
        public float BigTime { get { return bigTime; } }

        /// <summary>
        /// �g��E�k������ۂ̕ω���
        /// </summary>
        public float SizeChange { get { return sizeChange; } }

        /// <summary>
        /// �J�n���ɍs���T�C�Y�ω��̕ω���
        /// </summary>
        public float StartSizeChange { get { return startSizeChange; } }
    }
}