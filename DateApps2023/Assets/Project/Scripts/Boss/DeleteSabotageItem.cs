using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSabotageItem : MonoBehaviour
{
    private GameObject destroyItem = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CloneSabotageItem"))
        {
            destroyItem = other.gameObject;
            destroyItem.GetComponent<SabotageItem>().DestroyMe();
        }
    }
}
