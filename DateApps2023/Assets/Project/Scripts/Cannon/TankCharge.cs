// �S���ҁF���㏃��
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// ��C�̃G�l���M�[�^���N�Ɋւ���N���X
    /// </summary>
    public class TankCharge : MonoBehaviour
    {
        [SerializeField]
        private Material[] materials = new Material[3];

        private GameObject inner = null;
        private Material material = null;
        // Start is called before the first frame update
        void Start()
        {
            inner = transform.GetChild(0).gameObject;
            inner.SetActive(false);
        }

        /// <summary>
        /// �`���[�W���ꂽ�G�l���M�[�ɑΉ������}�e���A����ݒ肷��
        /// </summary>
        /// <param name="energyType">�`���[�W���ꂽ�G�l���M�[�̎��</param>
        public void Charge(int energyType)
        {
            material = materials[energyType];
            inner.GetComponent<MeshRenderer>().material = material;
            inner.SetActive(true);
        }

        /// <summary>
        /// �G�l���M�[�^���N���\���ɂ���
        /// </summary>
        public void DisCharge()
        {
            inner.SetActive(false);
        }
    }
}