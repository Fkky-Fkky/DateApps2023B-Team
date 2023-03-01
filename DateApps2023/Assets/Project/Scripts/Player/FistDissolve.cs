using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistDissolve : MonoBehaviour
{
    #region
    private new Renderer renderer;

    [SerializeField]
    private float startTime = 0.5f;

    [SerializeField]
    private float endTime = 1.0f;

    [SerializeField]
    private float intervalTime = 0.2f;

    private float time = 0.0f;
    private float value = 0.0f;

    private bool isStartDissolve = false;
    private bool isEndDissolve = false;
    private bool isIntervalDissolve = false;

    [SerializeField]
    private float pushForward = 0.8f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        renderer= GetComponent<Renderer>();
        isEndDissolve = false;
        isIntervalDissolve = false;
        isStartDissolve = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartDissolve)
        {
            InStartDissolve();
        }
        if (isIntervalDissolve)
        {
            InIntervalDissolve();
        }
        if(isEndDissolve)
        {
            InEndDissolve();
        }
    }

    void InStartDissolve()
    {
        time += Time.deltaTime;
        transform.position += pushForward * Time.deltaTime * transform.up / startTime;
        if (time >= startTime)
        {
            time = 0.0f;
            value = 0;
            renderer.material.SetFloat("_DisAmount", value);
            isStartDissolve = false;
            isIntervalDissolve = true;
            isEndDissolve = false;
        }
    }

    void InIntervalDissolve()
    {
        time += Time.deltaTime;
        if (time >= intervalTime)
        {
            time = 0.0f;
            isStartDissolve = false;
            isIntervalDissolve = false;
            isEndDissolve = true;
        }
    }

    void InEndDissolve()
    {
        time += Time.deltaTime;
        renderer.material.SetFloat("_DisAmount", value + time / endTime);
        if (time >= endTime)
        {
            time = 0.0f;
            value = 1.0f;
            renderer.material.SetFloat("_DisAmount", value);
            Destroy(gameObject);
        }
    }

    public void OnEndDissolve()
    {
        isIntervalDissolve = false;
        isEndDissolve = true;
        endTime = startTime;
    }
}
