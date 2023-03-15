using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// エネルギーのダメージをCSVファイルから読み込み
/// </summary>
public class DamageCSV : MonoBehaviour
{
    private TextAsset csvFile = null;

    private List<string[]> damageDate = new List<string[]>();

    private int height = 0;
    private int i      = 1;

    public int Small = 0;
    public int Medium = 0;
    public int Large = 0;

    /// <summary>
    /// CSVデータの列数
    /// </summary>
    private enum DATA_ROW
    {
        ZERO,
        ONE,
        TWO
    }

    const int LIMIT_INDEX = -1;

    private void Awake()
    {
        DamageCSVLoad();
    }
    /// <summary>
    /// CSVファイル読み込み
    /// </summary>
    private void CsvReader()
    {
        csvFile = Resources.Load("CSV/EnergyDamage") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > LIMIT_INDEX)
        {
            string line = reader.ReadLine();
            damageDate.Add(line.Split(','));
            height++;
        }
    }
    /// <summary>
    /// 読み込んだデータをint型へ変換 damageDate[行][列]
    /// </summary>
    public void DamageCSVLoad()
    {
        CsvReader();

        for (i = 1; i < height; i++)
        {
            Small  = int.Parse(damageDate[i][(int)DATA_ROW.ZERO]);
            Medium = int.Parse(damageDate[i][(int)DATA_ROW.ONE]);
            Large  = int.Parse(damageDate[i][(int)DATA_ROW.TWO]);
        }
    }
}