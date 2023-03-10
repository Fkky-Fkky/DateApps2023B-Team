using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 当たり判定エリアと危険エリアの生成
/// </summary>
public class AreaControl : MonoBehaviour
{
    [SerializeField]
    private GameObject dmageAreaCenter = null;
    [SerializeField]
    private GameObject damageAreaRight = null;
    [SerializeField]
    private GameObject damageAreaLeft  = null;
    [SerializeField]
    private GameObject dangerZone      = null;

    private int areaCount = 0;

    private Vector3 dangerCenter = new Vector3(0.0f, -1.2f, 0.0f);
    private Vector3 dangerLeft = new Vector3(-10.0f, -1.2f, 0.0f);
    private Vector3 dangerRigth = new Vector3(10.0f, -1.2f, 0.0f);

    private List<GameObject> dangerAreaList = new List<GameObject>();

    const int AREA_COUNT_MAX = 1;

    const float CENTER_TARGET         =  0.0f;
    const float RIGHT_TARGET          =  0.1f;
    const float LEFT_TARGET           = -0.1f;
    const float DANGER_OBJECT_ANGLE_Y = 180.0f;

    /// <summary>
    /// ビームが当たるエリアの表示
    /// </summary>
    public void DangerZone()
    {
        switch (gameObject.tag)
        {
            case "Center":
                dangerAreaList.Add(Instantiate(dangerZone, dangerCenter, Quaternion.Euler(0.0f, DANGER_OBJECT_ANGLE_Y, 0.0f)));
                break;
            case "Left":
                dangerAreaList.Add(Instantiate(dangerZone, dangerLeft, Quaternion.Euler(0.0f, DANGER_OBJECT_ANGLE_Y, 0.0f)));
                break;
            case "Right":
                dangerAreaList.Add(Instantiate(dangerZone, dangerRigth, Quaternion.Euler(0.0f, DANGER_OBJECT_ANGLE_Y, 0.0f)));
                break;
        }
    }

    public void GenerateDamageArea()
    {
        if (gameObject.transform.position.x == CENTER_TARGET)
        {
            DamageObject(dmageAreaCenter);
        }
        if (gameObject.transform.position.x >= RIGHT_TARGET)
        {
            DamageObject(damageAreaRight);
        }
        if (gameObject.transform.position.x <= LEFT_TARGET)
        {
            DamageObject(damageAreaLeft);
        }
    }

    /// <summary>
    /// 当たり判定エリアを生成
    /// </summary>
    /// <param name="damageArea"></param>
    private void DamageObject(GameObject damageArea)
    {
        if (areaCount < AREA_COUNT_MAX)
        {
            Instantiate(damageArea);
            areaCount++;
        }
    }
}
