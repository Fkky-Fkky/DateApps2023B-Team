using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictItem : MonoBehaviour
{
    MeshRenderer myMesh;

    [SerializeField]
    private float transparentSpeed = 0.01f;

    [SerializeField]
    private float transparentValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        myMesh = this.gameObject.GetComponent<MeshRenderer>();
        myMesh.material.color = new Color(myMesh.material.color.r, myMesh.material.color.g, myMesh.material.color.b, transparentValue);
        StartCoroutine("Transparent");
    }

    public void DestroyPredict()
    {
        Destroy(this.gameObject);
    }

   IEnumerator Transparent()
    {
        while (true)
        {
            for (int i = 0; i < 255 - transparentValue; i++)
            {
                myMesh.material.color = myMesh.material.color + new Color32(0, 0, 0, 1);
                //if(myMesh.material.color.a == 255 - transparentValue)
                //{
                //    break;
                //}
                yield return new WaitForSeconds(transparentSpeed);
            }
            for (int i = 0; i < 255 - transparentValue; i++)
            {
                myMesh.material.color = myMesh.material.color - new Color32(0, 0, 0, 1);

                yield return new WaitForSeconds(transparentSpeed);
            }
        }
    }
}
