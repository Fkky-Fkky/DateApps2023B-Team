using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    void OnTriggerEnter(Collider collision)//Trigger
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.OnattackCollider();
        }
    }
}
