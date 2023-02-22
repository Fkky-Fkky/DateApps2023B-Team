using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DamageCSV : MonoBehaviour
{
    private TextAsset csvFile;
    private List<string[]> damageDate = new List<string[]>();

    public int small;
    public int medium;
    public int large;

    private int height = 0;
    int i = 1;

    private void Awake()
    {
        DamageCSVLoad();
    }

    private void CsvReader()
    {
        csvFile = Resources.Load("CSV/EnergyDamage") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            damageDate.Add(line.Split(','));
            height++;
        }
    }

    public void DamageCSVLoad()
    {
        CsvReader();

        for (i = 1; i < height; i++)
        {
            small = int.Parse(damageDate[i][0]);
            medium = int.Parse(damageDate[i][1]);
            large = int.Parse(damageDate[i][2]);
        }
    }
}