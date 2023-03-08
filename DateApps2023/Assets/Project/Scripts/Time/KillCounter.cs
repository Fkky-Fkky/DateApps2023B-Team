using TMPro;
using UnityEngine;

/// <summary>
/// 怪獣を倒した数と全怪獣の数を表示するクラス
/// </summary>
public class KillCounter : MonoBehaviour
{
    [SerializeField]
    private int allBossCount = 15;

    private TextMeshProUGUI countTMP = null;
    private int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        killCount = BossCount.GetKillCount();
        countTMP = GetComponent<TextMeshProUGUI>();
        countTMP.text = ((int)killCount).ToString("0") +"/"+((int)allBossCount).ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        killCount = BossCount.GetKillCount();
        countTMP.text = ((int)killCount).ToString("0") + "/" + ((int)allBossCount).ToString("0");
    }
}
