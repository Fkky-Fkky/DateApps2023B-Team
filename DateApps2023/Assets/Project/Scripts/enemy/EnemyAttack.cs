//担当者:丸子羚
using UnityEngine;
//小型エネミーの攻撃の判定のクラス
public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.OnAttackCollider();
        }
    }
}
