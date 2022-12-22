using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubehantei : MonoBehaviour
{
    private GameObject destroyItem = null;

    [SerializeField]
    private CreateRandomPosition createrandomposition = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            destroyItem = other.gameObject;
            destroyItem.GetComponent<hantei>().DestroyMe();
            createrandomposition.Settower_bild_flag();
        }
    }
}
