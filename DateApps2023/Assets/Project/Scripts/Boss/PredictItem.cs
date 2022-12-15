using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictItem : MonoBehaviour
{
    MeshRenderer myMesh;

    [SerializeField]
    private float transparentSpeed = 1.0f;

    [SerializeField]
    private float transparentValue = 0;

    [SerializeField]
    private float minAlpha = 0.0f;
    [SerializeField]
    private float maxAlpha = 1.0f;

    private bool fadeFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        myMesh = this.gameObject.GetComponent<MeshRenderer>();
        myMesh.material.color = new Color(myMesh.material.color.r, myMesh.material.color.g, myMesh.material.color.b, transparentValue);
    }

    private void Update()
    {
        float alpha = myMesh.material.color.a;

        if (fadeFlag)
        {
            alpha += Time.deltaTime * transparentSpeed;
            if (alpha >= maxAlpha)
            {
                fadeFlag = false;
            }
        }
        else
        {
            alpha -= Time.deltaTime * transparentSpeed;
            if (alpha <= minAlpha)
            {
                fadeFlag = true;
            }
        }
        myMesh.material.color = new Color(myMesh.material.color.r, myMesh.material.color.g, myMesh.material.color.b, alpha);

    }
}
