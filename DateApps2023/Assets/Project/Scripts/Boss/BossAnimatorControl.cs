using UnityEngine;
/// <summary>
/// ボスのアニメーション
/// </summary>
public class BossAnimatorControl : MonoBehaviour
{
    [SerializeField]
    private new Animator animation = null;

    /// <summary>
    /// ゲームオーバーにする
    /// </summary>
    public bool IsGameOver { get; private set; }

    private void Start()
    {
        IsGameOver = false;
    }

    /// <summary>
    /// アニメーションの遷移(Trigger)
    /// </summary>
    /// <param name="animationName">アニメーションの名前</param>
    public void SetTrigger(string animationName)
    {
        animation.SetTrigger(animationName);
    }
    /// <summary>
    /// アニメーションの遷移(bool,true)
    /// </summary>
    /// <param name="animationName">アニメーションの名前</param>
    public void SetBoolTrue(string animationName)
    {
        animation.SetBool(animationName, true);
    }
    /// <summary>
    /// アニメーションの遷移(bool,false)
    /// </summary>
    /// <param name="animationName">アニメーションの名前</param>
    public void SetBoolFalse(string animationName)
    {
        animation.SetBool(animationName, false);
    }

    /// <summary>
    /// 攻撃された時のアニメーション
    /// </summary>
    public void DamageAnimation(int bossHp)
    {
        if (bossHp > 0)
        {
            animation.SetTrigger("Damage");
        }
        else
        {
            animation.SetTrigger("Die");
        }
    }
}