using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BossCSV : MonoBehaviour
{
    [SerializeField]
    private int bossMaxCount = 11;

    private TextAsset csvFile       = null;

    private List<string[]> bossDate = new List<string[]>();

    private int height = 0;
    private int i      = 1;

    public int[] AppearanceLane       = null;
    public int[] BossHp               = null;
    public float[] AppearanceTime     = null;
    public float[] AttackIntervalTime = null;
    public float[] PositionZ          = null;
    public float[] BossSpeed          = null;
    public string[] BossType          = null;

    private void Awake()
    {

        BossType           = new string[bossMaxCount];
        AppearanceTime     = new float[bossMaxCount];
        AttackIntervalTime = new float[bossMaxCount];
        AppearanceLane     = new int[bossMaxCount];
        PositionZ          = new float[bossMaxCount];
        BossHp             = new int[bossMaxCount];
        BossSpeed          = new float[bossMaxCount];

        BossCSVLoad();
    }

    private void CsvReader()
    {
        csvFile = Resources.Load("CSV/BossSample") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            bossDate.Add(line.Split(','));
            height++;
        }
    }

    public void BossCSVLoad()
    {
        CsvReader();

        for (i = 1; i < height; i++)
        {
            BossType[i]           = bossDate[i][0];
            AppearanceTime[i]     = float.Parse(bossDate[i][1]);
            AttackIntervalTime[i] = float.Parse(bossDate[i][2]);
            AppearanceLane[i]     = int.Parse(bossDate[i][3]);
            PositionZ[i]          = float.Parse(bossDate[i][4]);
            BossHp[i]             = int.Parse(bossDate[i][5]);
            BossSpeed[i]          = float.Parse(bossDate[i][6]);

        }
    }

}