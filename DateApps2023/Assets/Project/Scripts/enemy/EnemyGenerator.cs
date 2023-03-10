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
    private int spiderSpawnTime = 10;

    public GameObject Spider = null;

    private float x = 0;

    private float z = 0;

    private float spiderTime = 0;

    private int random = 0;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        spiderTime += Time.deltaTime;

        SummonSpider();
    }
    /// <summary>
    /// エネミーを生み出す
    /// </summary>
    private void SummonSpider()
    {
        if (spiderTime >= spiderSpawnTime && random == 1)
        {
            spiderTime = 0;

            x = Random.Range(rangeA.position.x, rangeB.position.x);

            z = Random.Range(rangeA.position.z, rangeB.position.z);
            Instantiate(Spider, new Vector3(x, -8, z), Spider.transform.rotation);
            random = Random.Range(1, 3);
        }

        if (spiderTime >= spiderSpawnTime && random >= 2)
        {
            spiderTime = 0;

            x = Random.Range(rangeC.position.x, rangeD.position.x);

            z = Random.Range(rangeC.position.z, rangeD.position.z);
            Instantiate(Spider, new Vector3(x, -8, z), Spider.transform.rotation);
            random = Random.Range(1, 3);
        }
    }
}
