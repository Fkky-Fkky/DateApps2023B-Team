//担当者:丸子羚
using UnityEngine;

/// <summary>
/// エネミーを生み出すジェネレータークラス
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform rangeA = null;

    [SerializeField]
    private Transform rangeB = null;

    [SerializeField]
    private Transform rangeC = null;

    [SerializeField]
    private Transform rangeD = null;

    [SerializeField]
    private int spiderSpawnCoolTime = 10;

    public GameObject Spider = null;

    private int random = 0;

    private int spawnPoint = 0;

   private float x = 0;

    private float z = 0;

    private float spiderSpawnTime = 0;

    private const int SPAWN_POSITION = -8;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(1, 3);
        spawnPoint = random;
    }

    // Update is called once per frame
    void Update()
    {
        spiderSpawnTime += Time.deltaTime;

        SummonSpider();
    }
    /// <summary>
    /// エネミーを生み出す
    /// </summary>
    private void SummonSpider()
    {
        if (spiderSpawnTime >= spiderSpawnCoolTime && spawnPoint == 1)
        {
            spiderSpawnTime = 0;

            x = Random.Range(rangeA.position.x, rangeB.position.x);

            z = Random.Range(rangeA.position.z, rangeB.position.z);
            Instantiate(Spider, new Vector3(x, SPAWN_POSITION, z), Spider.transform.rotation);
            random = Random.Range(1, 3);
            spawnPoint = random;
        }

        if (spiderSpawnTime >= spiderSpawnCoolTime && spawnPoint >= 2)
        {
            spiderSpawnTime = 0;

            x = Random.Range(rangeC.position.x, rangeD.position.x);

            z = Random.Range(rangeC.position.z, rangeD.position.z);
            Instantiate(Spider, new Vector3(x, SPAWN_POSITION, z), Spider.transform.rotation);
            random = Random.Range(1, 3);
            spawnPoint = random;
        }
    }
}
