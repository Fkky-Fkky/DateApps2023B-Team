// 担当者：吹上純平
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// 大砲のエネルギータンクに関するクラス
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
        /// チャージされたエネルギーに対応したマテリアルを設定する
        /// </summary>
        /// <param name="energyType">チャージされたエネルギーの種類</param>
        public void Charge(int energyType)
        {
            material = materials[energyType];
            inner.GetComponent<MeshRenderer>().material = material;
            inner.SetActive(true);
        }

        /// <summary>
        /// エネルギータンクを非表示にする
        /// </summary>
        public void DisCharge()
        {
            inner.SetActive(false);
        }
    }
}