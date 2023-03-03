using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    private int killCount;
    TextMeshProUGUI countTMP;

    [SerializeField]
    private int AllBossCount = 15;

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
