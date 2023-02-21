using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BossCSV : MonoBehaviour
{
    private TextAsset csvFile;
    private List<string[]> bossDate = new List<string[]>();

    public string[] bossType = new string[11];
    public float[] appearanceTime = new float[11];
    public float[] attackIntervalTime = new float[11];
    public int[] appearanceLane = new int[11];
    public float[] positionZ = new float[11];
    public int[] bossHp = new int[11];
    public float[] bossSpeed = new float[11];

    private int height = 0;
    private int i = 1; 


    private void Awake()
    {
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


            Debug.Log("ボスを読み込みました : " + bossType[i]);
            Debug.Log("ボスの出現時間を読み込みました : " + appearanceTime[i]);
            Debug.Log("ボスの攻撃時間を読み込みました : " + attackIntervalTime[i]);
            Debug.Log("ボスの出現レーンを読み込みました : " + appearanceLane[i]);
            Debug.Log("ボスの移動スピードを読み込みました : " + bossSpeed[i]);


        }
    }

    public int Height()
    {
        return height;
    }
}
