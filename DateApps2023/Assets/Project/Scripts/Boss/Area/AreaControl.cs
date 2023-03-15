using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����蔻��G���A�Ɗ댯�G���A�̐���
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

    private Vector3 dangerCenter = new Vector3(0.0f, -1.2f, 0.0f);
    private Vector3 dangerLeft   = new Vector3(-10.0f, -1.2f, 0.0f);
    private Vector3 dangerRigth  = new Vector3(10.0f, -1.2f, 0.0f);

    private List<GameObject> dangerAreaList = new List<GameObject>();

    const float DANGER_OBJECT_ANGLE_Y = 180.0f;

    /// <summary>
    /// �r�[����������G���A�̕\��
    /// </summary>
    public void GenerateDangerZone(GameObject boss)
    {
        switch (boss.tag)
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
    
    /// <summary>
    /// �U�����L�����Z�������Ƃ��ɃG�t�F�N�g�ƃG���A�j��
    /// </summary>
    public void DestroyDamageAreaList()
    {
        for (int i = 0; i < dangerAreaList.Count; i++)
        {
            Destroy(dangerAreaList[i]);
            dangerAreaList.RemoveAt(i);
        }
    }
}
