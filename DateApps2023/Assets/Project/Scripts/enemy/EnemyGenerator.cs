//�S����:�ێq�
using UnityEngine;

/// <summary>
/// �G�l�~�[�𐶂ݏo���W�F�l���[�^�[�N���X
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

    private int random = 0;

    private int spawnPoint = 0;

    private int spawnPositionY = -8;

   private float x = 0;

    private float z = 0;

    private float spiderTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(1, 3);
        spawnPoint = random;
    }

    // Update is called once per frame
    void Update()
    {
        spiderTime += Time.deltaTime;

        SummonSpider();
    }
    /// <summary>
    /// �G�l�~�[�𐶂ݏo��
    /// </summary>
    private void SummonSpider()
    {
        if (spiderTime >= spiderSpawnTime && spawnPoint == 1)
        {
            spiderTime = 0;

            x = Random.Range(rangeA.position.x, rangeB.position.x);

            z = Random.Range(rangeA.position.z, rangeB.position.z);
            Instantiate(Spider, new Vector3(x, spawnPositionY, z), Spider.transform.rotation);
            random = Random.Range(1, 3);
            spawnPoint = random;
        }

        if (spiderTime >= spiderSpawnTime && spawnPoint >= 2)
        {
            spiderTime = 0;

            x = Random.Range(rangeC.position.x, rangeD.position.x);

            z = Random.Range(rangeC.position.z, rangeD.position.z);
            Instantiate(Spider, new Vector3(x, spawnPositionY, z), Spider.transform.rotation);
            random = Random.Range(1, 3);
            spawnPoint = random;
        }
    }
}
