using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public Slider slider;

    int boss_hp = 3;

    [SerializeField]
    private Transform target;
    private float targetRange;

    [SerializeField]
    TextMeshProUGUI BossDistanceTMP;

    [SerializeField]
    private string sceneName = "New Scene";

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 3;
    }

    // Update is called once per frame
    void Update()
    {
        targetRange = gameObject.transform.position.z - target.position.z;
        BossDistanceTMP.text = "BOSS:" + ((int)targetRange/1000).ToString("0")+"."+ ((int)targetRange % 1000).ToString("000") + "km";
        slider.value = targetRange / 1000;

        if(boss_hp==0)
        {
            Destroy(gameObject);
            Debug.Log("qqqqqqq");
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            boss_hp -= 1;
            Debug.Log("aijwdijdij");
        }
    }

    private void OnTriggerEnter(Collider other)
  {
      if(other.gameObject.tag == "FailedLine")
      {
          SceneManager.LoadScene(sceneName);
      }
  }

    public void bosshp()
    {
        boss_hp -= 1;
    }

}
