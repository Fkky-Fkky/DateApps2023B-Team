using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//
// 
//
public class BossController : MonoBehaviour
{
    public Slider slider;

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
    }

    private void OnTriggerEnter(Collider other)
  {
      if(other.gameObject.tag == "FailedLine")
      {
          SceneManager.LoadScene(sceneName);
      }
  }

}
