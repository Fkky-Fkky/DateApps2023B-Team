using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    #region
    private int killCount;
    private TextMeshProUGUI countTMP;

    [SerializeField]
    private int AllBossCount = 15;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        countTMP = GetComponent<TextMeshProUGUI>();
        killCount = BossCount.GetKillCount();
        countTMP.text = ((int)killCount).ToString("0") +"/"+((int)AllBossCount).ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        killCount = BossCount.GetKillCount();
        countTMP.text = ((int)killCount).ToString("0") + "/" + ((int)AllBossCount).ToString("0");
    }
}
