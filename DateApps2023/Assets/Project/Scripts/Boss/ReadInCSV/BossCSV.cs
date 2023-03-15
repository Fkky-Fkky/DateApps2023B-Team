using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// ボスのデータをCSVファイル読み込み
/// </summary>
public class BossCSV : MonoBehaviour
{
    [SerializeField]
    private int bossMaxCount = 11;

    private TextAsset csvFile       = null;

    private List<string[]> bossDate = new List<string[]>();

    private int height = 0;
    private int i      = 1;

    /// <summary>
    /// CSVデータの列数
    /// </summary>
    private enum DATA_ROW
    {
        ZERO,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX
    }

    public int[] AppearanceLane       = null;
    public int[] BossHp               = null;
    public float[] AppearanceTime     = null;
    public float[] AttackIntervalTime = null;
    public float[] PositionZ          = null;
    public float[] BossSpeed          = null;
    public string[] BossType          = null;

    const int LIMIT_INDEX   = -1;
    const int INITIAL_VALUE =  1;

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
    /// <summary>
    /// CSVファイル読み込み
    /// </summary>
    private void CsvReader()
    {
        csvFile = Resources.Load("CSV/BossSample") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > LIMIT_INDEX)
        {
            string line = reader.ReadLine();
            bossDate.Add(line.Split(','));
            height++;
        }
    }
    /// <summary>
    /// CSVファイルからデータを変換 bossDate[行][列]
    /// </summary>
    public void BossCSVLoad()
    {
        CsvReader();

        for (i = INITIAL_VALUE; i < height; i++)
        {
            BossType[i]           = bossDate[i][(int)DATA_ROW.ZERO];
            AppearanceTime[i]     = float.Parse(bossDate[i][(int)DATA_ROW.ONE]);
            AttackIntervalTime[i] = float.Parse(bossDate[i][(int)DATA_ROW.TWO]);
            AppearanceLane[i]     = int.Parse(bossDate[i][(int)DATA_ROW.THREE]);
            PositionZ[i]          = float.Parse(bossDate[i][(int)DATA_ROW.FOUR]);
            BossHp[i]             = int.Parse(bossDate[i][(int)DATA_ROW.FIVE]);
            BossSpeed[i]          = float.Parse(bossDate[i][(int)DATA_ROW.SIX]);

        }
    }

}