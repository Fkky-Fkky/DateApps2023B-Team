using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BossCSV : MonoBehaviour
{
    [SerializeField]
    private int BossMaxCount = 11;

    private TextAsset csvFile = null;
    private List<string[]> bossDate = new List<string[]>();

    public string[] bossType          = null;
    public float[] appearanceTime     = null;
    public float[] attackIntervalTime = null;
    public int[] appearanceLane       = null;
    public float[] positionZ          = null;
    public int[] bossHp               = null;
    public float[] bossSpeed          = null;

    private int height = 0;
    private int i      = 1;

    private void Awake()
    {

        bossType           = new string[BossMaxCount];
        appearanceTime     = new float[BossMaxCount];
        attackIntervalTime = new float[BossMaxCount];
        appearanceLane     = new int[BossMaxCount];
        positionZ          = new float[BossMaxCount];
        bossHp             = new int[BossMaxCount];
        bossSpeed          = new float[BossMaxCount];

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
            bossType[i]           = bossDate[i][0];
            appearanceTime[i]     = float.Parse(bossDate[i][1]);
            attackIntervalTime[i] = float.Parse(bossDate[i][2]);
            appearanceLane[i]     = int.Parse(bossDate[i][3]);
            positionZ[i]          = float.Parse(bossDate[i][4]);
            bossHp[i]             = int.Parse(bossDate[i][5]);
            bossSpeed[i]          = float.Parse(bossDate[i][6]);

        }
    }

}