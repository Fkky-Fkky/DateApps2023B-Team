using UnityEngine;

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

    public float MoveSpeed { get { return moveSpeed; } }
    public float CarryOverSpeed { get { return carryOverSpeed; } }
    public float AnimationSpeed { get { return animationSpeed; } }
    public float[] SmallCarrySpeed { get { return smallCarrySpeed; } }
    public float[] MidiumCarrySpeed { get { return midiumCarrySpeed; } }
    public float[] LargeCarrySpeed { get { return largeCarrySpeed; } }
}