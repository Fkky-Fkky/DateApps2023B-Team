//�S����:�ێq�
using UnityEngine;
//���^�G�l�~�[�̍U���̔���̃N���X
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
