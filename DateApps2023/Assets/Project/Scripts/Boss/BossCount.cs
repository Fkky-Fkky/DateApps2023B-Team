//�S����:���c��
using UnityEngine;

/// <summary>
/// �{�X���|�ꂽ���J�E���g����
/// </summary>
public class BossCount : MonoBehaviour
{
    public int BossKillCount = 0;
    public static int KillCount;

    void Start()
    {
        KillCount = BossKillCount;
    }

    /// <summary>
    /// �{�X���|�ꂽ��J�E���g�𑝂₷
    /// </summary>
    public void SetBossKillCount()
    {
        KillCount++;
    }
    /// <summary>
    /// �{�X���|�ꂽ�J�E���g��Ԃ�
    /// </summary>
    /// <returns>�{�X��|�����J�E���g�̒l</returns>
    public static int GetKillCount()
    {
        return KillCount;
    }
}
