//�S����:���c��
using UnityEngine;

/// <summary>
/// �����蔻��G���A�̃f�X�g���C���s��
/// </summary>
public class DamageArea : MonoBehaviour
{
    public BossAttack BossAttack = null;
    private float time           = 0.0f;
    private float destroyTime    = 0.0f;

    private void Start()
    {
        destroyTime = BossAttack.BeamOffTimeMax();
    }
    void Update()
    {
        time += Time.deltaTime;
        if(time>=destroyTime)
        {
            Destroy(gameObject);
            time= 0.0f;
        }
    }
}
