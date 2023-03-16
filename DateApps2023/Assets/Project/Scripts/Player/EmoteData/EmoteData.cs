//担当者:吉田理紗
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// エモート関連のスクリプトにおいて、一部の変数を設定するクラス
    /// 作成したScriptableObjectを、対象のInspecterにアタッチして使う
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
        /// 開始時の時間
        /// </summary>
        public float StartTime { get { return startTime; } }

        /// <summary>
        /// 終了時までの時間
        /// </summary>
        public float EndTime { get { return endTime; } }

        /// <summary>
        /// 開始時と終了時に上昇する移動量
        /// </summary>
        public float MoveY { get { return moveY; } }

        /// <summary>
        /// 縮小するまでの時間
        /// </summary>
        public float SmallTime { get { return smallTime; } }

        /// <summary>
        /// 拡大するまでの時間
        /// </summary>
        public float BigTime { get { return bigTime; } }

        /// <summary>
        /// 拡大・縮小する際の変化量
        /// </summary>
        public float SizeChange { get { return sizeChange; } }

        /// <summary>
        /// 開始時に行うサイズ変化の変化量
        /// </summary>
        public float StartSizeChange { get { return startSizeChange; } }
    }
}