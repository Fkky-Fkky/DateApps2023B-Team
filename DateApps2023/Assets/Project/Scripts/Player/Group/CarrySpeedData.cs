//担当者:吉田理紗
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// 運搬関連のスクリプトにおいて、一部の変数を設定するクラス
    /// 作成したScriptableObjectを、対象のInspecterにアタッチして使う
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
        /// 運搬中の移動速度基準
        /// </summary>
        public float MoveSpeed { get { return moveSpeed; } }

        /// <summary>
        /// 人数が基準よりも下のときに移動速度に掛ける倍数
        /// </summary>
        public float CarryOverSpeed { get { return carryOverSpeed; } }

        /// <summary>
        /// 運搬中のアニメーションスピード
        /// </summary>
        public float AnimationSpeed { get { return animationSpeed; } }

        /// <summary>
        /// 物資(小)を運んでいるときの運搬速度
        /// </summary>
        public float[] SmallCarrySpeed { get { return smallCarrySpeed; } }

        /// <summary>
        /// 物資(中)を運んでいるときの運搬速度
        /// </summary>
        public float[] MidiumCarrySpeed { get { return midiumCarrySpeed; } }

        /// <summary>
        /// 物資(大)を運んでいるときの運搬速度
        /// </summary>
        public float[] LargeCarrySpeed { get { return largeCarrySpeed; } }
    }
}