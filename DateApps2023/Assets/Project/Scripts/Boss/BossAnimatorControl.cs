using UnityEngine;
/// <summary>
/// ボスのアニメーション
/// </summary>
public class BossAnimatorControl : MonoBehaviour
{
    [SerializeField]
    private new Animator animation = null;

    private int seCount = 0;

    const float GAME_OVER_TIME = 0.6f;

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