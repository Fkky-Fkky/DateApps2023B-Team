using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAttack : MonoBehaviour
{
    private int myPlayerNo = 5;
    private BoxCollider boxCol= null;

    [SerializeField]
    private float hitTime = 0.25f;

    private PlayerMove playerMove;

    Animator animator;
    float time = 0;

    private bool myAttack = false;

    //[SerializeField]
    //public enemy enemy_hit;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;

        animator = GetComponentInParent<Animator>();

        playerMove = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all[myPlayerNo].aButton.wasPressedThisFrame)
        {
            if (!myAttack)
            {
                FistAttack();
            }
        }
      
        if(myAttack)
        {
            time += Time.deltaTime;
            if (time >= hitTime)
            {
                EndAttack();
                time = 0;
            }
        }
    }

    private void FistAttack()
    {
        animator.SetBool("Attack", true);
        boxCol.enabled = true;
        playerMove.StartAttack();
        myAttack = true;
    }

    private void EndAttack()
    {
        animator.SetBool("Attack", false);
        boxCol.enabled = false;
        playerMove.EndAttack();
        myAttack = false;
    }

    public void GetPlayerNo(int parentNumber)
    {
        myPlayerNo = parentNumber;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
           
            if (!rb)
                return;

            //Vector3 pw = new Vector3(0, 30.0f, 0.0f);
            //rb.AddForce(pw, ForceMode.Impulse);
            rb.AddForce(this.transform.forward * 10f, ForceMode.VelocityChange);

            NavMeshAgent nav = other.GetComponent<NavMeshAgent>();
            if (!nav)
                return;

            nav.enabled = false;
        }
    }
    
}
