using UnityEngine;

/// <summary>
/// ステージ配置に関する処理を行うクラス
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
    /// 設定された配列の中からランダムでステージを生成する
    /// </summary>
    void OnGenerate()
    {
        number = Random.Range(0, stagePattern.Length);
        Instantiate(stagePattern[number], generatePos, Quaternion.identity);
    }
}
