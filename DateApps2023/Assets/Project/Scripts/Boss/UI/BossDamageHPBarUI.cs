//�S����:���c��
using UnityEngine;

/// <summary>
/// �{�X���_���[�W�󂯂�����HPUI�̌���
/// </summary>
public class BossDamageHPBarUI : MonoBehaviour
{
    [SerializeField]
    private GameObject hpCores = null;
    [SerializeField]
    private GameObject[] hpBar = new GameObject[9];
    [SerializeField]
    private GameObject[] hpMemori = new GameObject[9];

    /// <summary>
    /// �{�X��HP
    /// </summary>
    private enum BOSS_HP
    {
        ONE   = 1,
        TWO   = 2,
        THREE = 3,
        FOUR  = 4,
        SEVEN = 7,
        EIGHT = 8
    }

    const float BOSS_COMPARE_SCALE_INDEX = 18.0f;

    const float SMALL_BOSS_HP_POS_X = 0.6f;
    const float NOMAL_BOSS_MIN_HP_POS_X = 0.8f;
    const float BOSS_MIN_HP_POS_X = 0.4f;
    const float BOSS_MAX_HP_POS_X = 0.2f;
    const float BOSS_HP_BAR_POS_Y = 0.7f;

    /// <summary>
    /// �{�X�̗̑͂ɂ���āAHP�������̕\���ƈʒu����
    /// </summary>
    /// <param name="maxHp">�ő�HP�̒l</param>
    public void HpMemoriPosition(int maxHp)
    {

        hpBar = new GameObject[maxHp];

        for (int i = 0; i < hpMemori.Length; i++)
        {
            hpMemori[i].SetActive(false);
        }

        for (int i = 0; i < maxHp; i++)
        {
            hpBar[i] = hpMemori[i];
            hpMemori[i].SetActive(true);
        }

        switch (maxHp)
        {
            case (int)BOSS_HP.ONE:
                if (gameObject.transform.localScale.y < BOSS_COMPARE_SCALE_INDEX)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(SMALL_BOSS_HP_POS_X, BOSS_HP_BAR_POS_Y, 0);
                }
                if (gameObject.transform.localScale.y > BOSS_COMPARE_SCALE_INDEX)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(NOMAL_BOSS_MIN_HP_POS_X, -BOSS_HP_BAR_POS_Y, 0);
                }
                break;
            case (int)BOSS_HP.TWO:
                if (gameObject.transform.localScale.y > BOSS_COMPARE_SCALE_INDEX)
                {
                    hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(SMALL_BOSS_HP_POS_X, -BOSS_HP_BAR_POS_Y, 0);
                }
                break;
            case (int)BOSS_HP.THREE:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(BOSS_MIN_HP_POS_X, -BOSS_HP_BAR_POS_Y, 0);
                break;
            case (int)BOSS_HP.FOUR:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(BOSS_MAX_HP_POS_X, -BOSS_HP_BAR_POS_Y, 0);
                break;
            case (int)BOSS_HP.SEVEN:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(BOSS_MIN_HP_POS_X, 0, 0);
                break;
            case (int)BOSS_HP.EIGHT:
                hpCores.GetComponent<RectTransform>().anchoredPosition = new Vector3(BOSS_MAX_HP_POS_X, 0, 0);
                break;
        }

    }
    /// <summary>
    /// ��ԏ������T�C�Y�̃G�l���M�[�Ŏ󂯂�����HPUI�̌�����
    /// </summary>
    /// <param name="bossHp">���݂�HP�̒l</param>
    public void HpBarSmallActive(int bossHp)
    {
        hpBar[bossHp + 0].SetActive(false);
    }

    /// <summary>
    /// �����炢�̃T�C�Y�̃G�l���M�[�Ŏ󂯂�����HPUI�̌�����
    /// </summary>
    /// <param name="maxHp">�ő�HP�̒l</param>
    /// <param name="bossHp">���݂�HP�̒l</param>
    /// <param name="mediumEnergyDamage">�G�l���M�[�̃_���[�W�̒l</param>
    public void HpBarMediumActive(int maxHp, int bossHp, int mediumEnergyDamage)
    {
        for (int i = 0; i < mediumEnergyDamage; i++)
        {
            if (bossHp + i < maxHp)
            {
                hpBar[bossHp + i].SetActive(false);
            }
        }
    }
    /// <summary>
    /// ��ԑ傫�����T�C�Y�̃G�l���M�[�Ŏ󂯂�����HPUI�̌�����
    /// </summary>
    /// <param name="maxHp">�ő�HP�̒l</param>
    /// <param name="bossHp">���݂�HP�̒l</param>
    public void HpBarLargeActive(int maxHp, int bossHp)
    {
        for (int i = 0; i < maxHp; i++)
        {
            hpBar[bossHp + i].SetActive(false);
        }
    }


}
