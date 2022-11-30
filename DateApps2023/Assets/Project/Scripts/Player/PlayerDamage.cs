using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerDamage : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider boxCol;
    private CapsuleCollider capsuleCol;
    float time = 0;
    private bool currentDamage;

    [SerializeField]
    private float stanTime = 5.0f;

    private MeshRenderer mesh;
    private Color defaultMesh;
    private float defaultPosY = 54.0f;
    private bool doCouroutine = false;

    private PlayerMove playerMove;
    private PlayerCarryDown playerCarryDown;

    private GameObject sabotageObject;
    private GameObject player;

    private void Start()
    {
        boxCol = this.gameObject.GetComponent<BoxCollider>();
        capsuleCol = this.gameObject.GetComponent<CapsuleCollider>();

        mesh = this.gameObject.GetComponent<MeshRenderer>();
        mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 0);
        defaultMesh = mesh.material.color;
        defaultPosY = this.gameObject.transform.position.y;

        playerMove = this.gameObject.GetComponent<PlayerMove>();
        playerCarryDown = this.gameObject.GetComponentInChildren<PlayerCarryDown>();

        player = this.gameObject;
    }

    private void Update()
    {
        if (currentDamage)
        {
            if (!doCouroutine)
            {
                StartCoroutine("MaterialTransparent");
                doCouroutine = true;
            }

            time += Time.deltaTime;

            this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    defaultPosY,
                    this.gameObject.transform.position.z);

            if (time > stanTime)
            {
                time = 0;
                rb = this.gameObject.AddComponent<Rigidbody>();
                rb = this.gameObject.GetComponent<Rigidbody>();
                //boxCol.enabled = true;
                capsuleCol.enabled = true;
                if (doCouroutine)
                {
                    StopCoroutine("MaterialTransparent");
                    mesh.material.color = defaultMesh;
                    this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    defaultPosY,
                    this.gameObject.transform.position.z);
                    doCouroutine = false;
                }
                Debug.Log("EndDamage");
                playerMove.playerMoveDamage = false;
                playerCarryDown.carryDamage = false;
                playerMove.InGroup = false;
                playerMove.EnterItem = false;
                currentDamage = false;

            }
        }
    }

    public void CallDamage()
    {
        Debug.Log("CallDamage");
        boxCol.enabled = false;
        capsuleCol.enabled = false;
        playerMove.playerMoveDamage = true;
        playerMove.InGroup = false;
        playerMove.EnterItem = false;
        playerCarryDown.carryDamage = true;
        currentDamage = true;
    }

    public void AvoidObject()
    {
        var heading = player.transform.position - sabotageObject.transform.position;
        this.gameObject.transform.position += new Vector3(heading.x * 1.5f, 0.0f, heading.z * 1.5f);

    }

    IEnumerator MaterialTransparent()
    {
        while (true)
        {
            for (int i = 0; i < 100; i++)
            {
                mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 1);
            }

            yield return new WaitForSeconds(0.2f);

            for (int k = 0; k < 100; k++)
            {
                mesh.material.color = mesh.material.color + new Color32(0, 0, 0, 1);
            }

            yield return new WaitForSeconds(0.2f);

        }
    }
    
    public void SetSabotageObject(GameObject setObject)
    {
        sabotageObject = setObject;
    }

}
