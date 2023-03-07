using UnityEngine;

/// <summary>
/// �G���[�g�֘A�̃X�N���v�g�ɂ����āA�ꕔ�̕ϐ���ݒ肷��N���X
/// �쐬����ScriptableObject���A�Ώۂ�Inspecter�ɃA�^�b�`���Ďg��
/// </summary>
[CreateAssetMenu(menuName = "CreateData/EmoteData", fileName = "EmoteData" )]
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

    public float StartTime { get { return startTime; } }
    public float EndTime { get { return endTime; } }
    public float MoveY { get { return moveY; } }
    public float SmallTime { get { return smallTime; } }
    public float BigTime { get { return bigTime; } }
    public float SizeChange { get { return sizeChange; } }
    public float StartSizeChange { get { return startSizeChange; } }
}
