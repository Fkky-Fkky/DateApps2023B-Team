//�S����:�g�c����
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// �^���֘A�̃X�N���v�g�ɂ����āA�ꕔ�̕ϐ���ݒ肷��N���X
    /// �쐬����ScriptableObject���A�Ώۂ�Inspecter�ɃA�^�b�`���Ďg��
    /// </summary>
    [CreateAssetMenu(menuName = "CreateData/CarrySpeedData", fileName = "CarrySpeedData")]
    public class CarrySpeedData : ScriptableObject
    {
        [SerializeField]
        private float moveSpeed = 50;
        [SerializeField]
        private float carryOverSpeed = 0.1f;
        [SerializeField]
        private float animationSpeed = 0.01f;
        [SerializeField]
        private float[] smallCarrySpeed = null;
        [SerializeField]
        private float[] midiumCarrySpeed = null;
        [SerializeField]
        private float[] largeCarrySpeed = null;

        /// <summary>
        /// �^�����̈ړ����x�
        /// </summary>
        public float MoveSpeed { get { return moveSpeed; } }

        /// <summary>
        /// �l������������̂Ƃ��Ɉړ����x�Ɋ|����{��
        /// </summary>
        public float CarryOverSpeed { get { return carryOverSpeed; } }

        /// <summary>
        /// �^�����̃A�j���[�V�����X�s�[�h
        /// </summary>
        public float AnimationSpeed { get { return animationSpeed; } }

        /// <summary>
        /// ����(��)���^��ł���Ƃ��̉^�����x
        /// </summary>
        public float[] SmallCarrySpeed { get { return smallCarrySpeed; } }

        /// <summary>
        /// ����(��)���^��ł���Ƃ��̉^�����x
        /// </summary>
        public float[] MidiumCarrySpeed { get { return midiumCarrySpeed; } }

        /// <summary>
        /// ����(��)���^��ł���Ƃ��̉^�����x
        /// </summary>
        public float[] LargeCarrySpeed { get { return largeCarrySpeed; } }
    }
}