using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


//[RequireComponent(typeof(NavMeshAgent))]
public class enemy : MonoBehaviour
{
    // 追いかける対象の座標情報
    [SerializeField] private Transform playerTransform;

    int move = 0;
    float attck_time = 0;

    //BoxCollider boxCol;

    Animator animator;

    int attck = Animator.StringToHash("attck");
    int idle = Animator.StringToHash("idle");
    int attck_idle = Animator.StringToHash("attck_idle");
    
    // エージェントをキャッシュしておく用
    private NavMeshAgent _agent;

    void Start()
    {
        animator = GetComponent<Animator>();

       
       
        _agent = GetComponent<NavMeshAgent>();
        animator.SetTrigger("idle");
    }
    void Update()
    {
        if (move == 0)
        {
            _agent.destination = playerTransform.position;
        }
        //if(move==1)
        //{
        //    Debug.Log("a");
            
        //}

        attck_time += Time.deltaTime;

        if (attck_time >= 0.5&&move==1)
        {
            animator.SetTrigger("attck");
            Debug.Log("i");
        }

        if (attck_time >= 1.5 && move == 1)
        {
            Debug.Log("u");
            move = 0;
        
        }
    }

    void OnTriggerEnter(Collider collision)//Trigger
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
                move = 1;
            attck_time = 0;
            animator.SetTrigger("attckidle");
        }
    }

    //void OnTriggerEnter(Collider collision)
    //{
    //    Debug.Log("Hit"); // ログを表示する
    //}
}