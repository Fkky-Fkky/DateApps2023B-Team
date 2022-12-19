using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class text : MonoBehaviour
{
    Animator animator;

    int Operatorout = Animator.StringToHash("out");
    int Operatorin = Animator.StringToHash("in");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
            animator.SetTrigger(Operatorout);

        if (Keyboard.current.sKey.wasPressedThisFrame)
            animator.SetTrigger(Operatorin);
    }
}
