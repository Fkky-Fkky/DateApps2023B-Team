using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private Enemy1 enemy;
    void OnTriggerEnter(Collider collision)//Trigger
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.OnAttackCollider();
        }
    }
}
