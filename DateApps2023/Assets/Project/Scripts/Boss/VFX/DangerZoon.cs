using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoon : MonoBehaviour
{
    [SerializeField]
    private Renderer dangerRenderer = null;

    public BossAttack BossAttack = null;

    private float time       = 0.0f;
    private float effectTime = 0.0f;

    private float flashTime          = 0.0f;
    private const float flashTimeMax = 0.3f;

    void Start()
    {
        time = 0.0f;

        effectTime = BossAttack.BeamTimeMax();

    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > effectTime)
        {
            Destroy(gameObject);
            time = 0.0f;
        }

        if (time > effectTime - 4.0f)
        {
            flashTime += Time.deltaTime;

            var repeatValue = Mathf.Repeat(flashTime, flashTimeMax);

            dangerRenderer.enabled = repeatValue >= flashTimeMax * 0.5f;
        }
    }
}