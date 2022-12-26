using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private int myPlayerNo = 5;
    private BoxCollider boxCol= null;

    [SerializeField]
    private float hitTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all[myPlayerNo].aButton.wasPressedThisFrame)
        {
            StartCoroutine(FistAttack());
        }
    }

    IEnumerator FistAttack()
    {
        boxCol.enabled = true;
        yield return new WaitForSeconds(hitTime);
        boxCol.enabled = false;

        yield return null;
    }

    public void GetPlayerNo(int parentNumber)
    {
        myPlayerNo = parentNumber;
    }
}
