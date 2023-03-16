//担当者:武田碧
using UnityEngine;

/// <summary>
/// 当たり判定エリアのデストロイを行う
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
