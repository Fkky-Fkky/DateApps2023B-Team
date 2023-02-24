using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BossCSV : MonoBehaviour
{
    private TextAsset csvFile;
    private List<string[]> bossDate = new List<string[]>();

    public string[] bossType;
    public float[] appearanceTime;
    public float[] attackIntervalTime;
    public int[] appearanceLane;
    public float[] positionZ;
    public int[] bossHp;
    public float[] bossSpeed;

    private int height = 0;
    private int i = 1;

    [SerializeField]
    private int BossMaxCount;

    private void Awake()
    {

        bossType = new string[BossMaxCount];
        appearanceTime = new float[BossMaxCount];
        attackIntervalTime = new float[BossMaxCount];
        appearanceLane = new int[BossMaxCount];
        positionZ = new float[BossMaxCount];
        bossHp = new int[BossMaxCount];
        bossSpeed = new float[BossMaxCount];

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
            bossType[i] = bossDate[i][0];
            appearanceTime[i] = float.Parse(bossDate[i][1]);
            attackIntervalTime[i] = float.Parse(bossDate[i][2]);
            appearanceLane[i] = int.Parse(bossDate[i][3]);
            positionZ[i] = float.Parse(bossDate[i][4]);
            bossHp[i] = int.Parse(bossDate[i][5]);
            bossSpeed[i] = float.Parse(bossDate[i][6]);

        }
    }

}