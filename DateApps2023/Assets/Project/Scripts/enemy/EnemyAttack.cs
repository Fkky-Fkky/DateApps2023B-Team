//’S“–ŽÒ:ŠÛŽqã·
using UnityEngine;

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
