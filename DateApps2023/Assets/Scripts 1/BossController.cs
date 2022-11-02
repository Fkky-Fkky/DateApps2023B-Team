using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject boss;
    [SerializeField]
    private bool knockBackFlag = false;

    [SerializeField]
    private Transform target;
    private float targetRange;

    [SerializeField]
    TextMeshProUGUI BossDistanceTMP;

    [SerializeField]
    [Tooltip("ボス移動速度")]
    private float moveSpeed = 5.0f;

    [SerializeField]
    [Tooltip("m換算？")]
    private float knockBackPower = 300.0f;

    [SerializeField]
    private string sceneName = "New Scene";

    [SerializeField]
    private float stopTime = 5.0f;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        targetRange = gameObject.transform.position.z - target.position.z;
        BossDistanceTMP.text = "BOSS:" + ((int)targetRange/1000).ToString("0")+"."+ ((int)targetRange % 1000).ToString("000") + "km";


        if (!knockBackFlag)
        {
            time += Time.deltaTime;
            if(time <= stopTime)
            {
                boss.transform.position = new Vector3(
                    0.0f,
                    boss.transform.position.y,
                    boss.transform.position.z);
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else
            {
                rb.velocity -= new Vector3(0.0f, 0.0f, moveSpeed * Time.deltaTime);

            }

        }
        else
        {
            time = 0;

            boss.transform.position = new Vector3(
                0.0f,
                boss.transform.position.y,
                boss.transform.position.z + knockBackPower * Time.deltaTime);
       


        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FailedLine")
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void KnockbackTrue()
    {
        knockBackFlag = true;
    }

    public void KnockbackFalse()
    {

        knockBackFlag = false;

    }
}
