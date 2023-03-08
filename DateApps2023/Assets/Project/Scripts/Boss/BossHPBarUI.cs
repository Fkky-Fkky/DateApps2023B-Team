using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスのHPUIの位置調整
/// </summary>
public class BossHPBarUI : MonoBehaviour
{
    [SerializeField]
    private GameObject hpGauge = null;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private BossMove bossMove = null;

    const int MIN_HP = 1;
    const int SMALE_MAX_HP = 2;
    const int NOMAL_MAX_HP = 5;
    const int BIG_MIN_HP = 7;
    const int MAX_HP = 9;

    const float BOSS_SCALE_Y = 18.0f;
    const float NOMAL_UI_POSITION_X = 4.0f;
    const float MINI_UI_POSITION_X = 13.0f;
    const float BIG_UI_POSITION_X = 4.2f;

    const float NOMAL_CENETR_UI_POSITION_Y = 0.1f;
    const float NOMAL_SIDE_UI_POSITION_Y = 0.05f;
    const float MINI_UI_POSITION_Y = -0.7f;

    /// <summary>
    /// 中央に出現するボスのHPUIの位置
    /// </summary>
    public void BossUIPositionCneter()
    {
        if (bossMove.BossHp >= MIN_HP && bossMove.BossHp <= NOMAL_MAX_HP)
        {
            if (gameObject.transform.localScale.y > BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, NOMAL_CENETR_UI_POSITION_Y, 0);
            }
        }
        if (bossMove.BossHp >= MIN_HP && bossMove.BossHp <= SMALE_MAX_HP)
        {
            if (gameObject.transform.localScale.y < BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, MINI_UI_POSITION_Y, 0);
            }
        }
        if (bossMove.BossHp >= BIG_MIN_HP && bossMove.BossHp <= MAX_HP)
        {
            hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// 右側に出現するボスのHPUIの位置
    /// </summary>
    public void BossUIPositionRight()
    {
        if (bossMove.BossHp >= MIN_HP && bossMove.BossHp <= NOMAL_MAX_HP)
        {
            if (gameObject.transform.localScale.y > BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(NOMAL_UI_POSITION_X, NOMAL_SIDE_UI_POSITION_Y, 0);
            }
        }
        if (bossMove.BossHp >= MIN_HP && bossMove.BossHp <= SMALE_MAX_HP)
        {
            if (gameObject.transform.localScale.y < BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(MINI_UI_POSITION_X, MINI_UI_POSITION_Y, 0);
            }
        }
        if (bossMove.BossHp >= BIG_MIN_HP && bossMove.BossHp <= MAX_HP)
        {
            hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(BIG_UI_POSITION_X, 0, 0);
        }
    }

    /// <summary>
    /// 左側に出現するボスのHPUIの位置
    /// </summary>
    public void BossUIPositionLeft()
    {
        if (bossMove.BossHp >= MIN_HP && bossMove.BossHp <= NOMAL_MAX_HP)
        {
            if (gameObject.transform.localScale.y > BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-NOMAL_UI_POSITION_X, NOMAL_SIDE_UI_POSITION_Y, 0.0f);
            }
        }
        if (bossMove.BossHp >= MIN_HP && bossMove.BossHp <= SMALE_MAX_HP)
        {
            if (gameObject.transform.localScale.y < BOSS_SCALE_Y)
            {
                hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-MINI_UI_POSITION_X, MINI_UI_POSITION_Y, 0.0f);
            }
        }
        if (bossMove.BossHp >= BIG_MIN_HP && bossMove.BossHp <= MAX_HP)
        {
            hpGauge.GetComponent<RectTransform>().anchoredPosition = new Vector3(-BIG_UI_POSITION_X, 0, 0);
        }
    }
}