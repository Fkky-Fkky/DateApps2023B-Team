using UnityEngine;
/// <summary>
/// �{�X�̃A�j���[�V����
/// </summary>
public class BossAnimatorControl : MonoBehaviour
{
    [SerializeField]
    private new Animator animation = null;

    private int seCount = 0;

    const float GAME_OVER_TIME = 0.6f;

    /// <summary>
    /// �A�j���[�V�����̑J��(Trigger)
    /// </summary>
    /// <param name="animationName">�A�j���[�V�����̖��O</param>
    public void SetTrigger(string animationName)
    {
        animation.SetTrigger(animationName);
    }
    /// <summary>
    /// �A�j���[�V�����̑J��(bool,true)
    /// </summary>
    /// <param name="animationName">�A�j���[�V�����̖��O</param>
    public void SetBoolTrue(string animationName)
    {
        animation.SetBool(animationName, true);
    }
    /// <summary>
    /// �A�j���[�V�����̑J��(bool,false)
    /// </summary>
    /// <param name="animationName">�A�j���[�V�����̖��O</param>
    public void SetBoolFalse(string animationName)
    {
        animation.SetBool(animationName, false);
    }

    /// <summary>
    /// �U�����ꂽ���̃A�j���[�V����
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