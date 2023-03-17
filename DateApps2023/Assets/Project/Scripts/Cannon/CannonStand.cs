// �S���ҁF���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// ��C�̔��ˑ�N���X
    /// </summary>
    public class CannonStand : MonoBehaviour
    {
        [SerializeField]
        private STAND_POSITION standPosition = STAND_POSITION.NONE;

        /// <summary>
        /// ���ˑ�̏ꏊ��Ԃ�
        /// </summary>
        public int ConnectingPos
        {
            get { return (int)standPosition; }
        }

        /// <summary>
        /// ���ˑ�̈ʒu
        /// </summary>
        public enum STAND_POSITION
        {
            NONE = -1,
            LEFT,
            CENTRE,
            RIGHT
        }
    }
}