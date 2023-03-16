//�S����:���c��
using UnityEngine;

/// <summary>
/// �U��������G���A���f�X�g���C
/// </summary>
public class DangerZoon : MonoBehaviour
{
    [SerializeField]
    private Renderer dangerRenderer = null;

    public BossAttack BossAttack = null;

    private float time       = 0.0f;
    private float effectTime = 0.0f;

    private float flashTime = 0.0f;
    

    const float FLASH_TIME_MAX    = 0.3f;
    const float REMAINING_SECONDS = 4.0f;
    const float HALF              = 0.5f;

    void Start()
    {
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

        if (time > effectTime - REMAINING_SECONDS)
        {
            flashTime += Time.deltaTime;

            var repeatValue = Mathf.Repeat(flashTime, FLASH_TIME_MAX);

            dangerRenderer.enabled = repeatValue >= FLASH_TIME_MAX * HALF;
        }
    }
}