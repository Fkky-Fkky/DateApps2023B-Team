using UnityEngine;

/// <summary>
/// �X�e�[�W�z�u�Ɋւ��鏈�����s���N���X
/// </summary>
public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stagePattern = null;

    [SerializeField]
    private Vector3 generatePos = Vector3.zero;

    private int number = 0;

    // Start is called before the first frame update
    void Start()
    {
        OnGenerate();
    }

    /// <summary>
    /// �ݒ肳�ꂽ�z��̒����烉���_���ŃX�e�[�W�𐶐�����
    /// </summary>
    void OnGenerate()
    {
        number = Random.Range(0, stagePattern.Length);
        Instantiate(stagePattern[number], generatePos, Quaternion.identity);
    }
}
