using UnityEngine;

/// <summary>
/// エネルギー物資の落下処理クラス
/// </summary>
public class FallEnergy : MonoBehaviour
{
    private bool isEnergyResourceLanded = false;
    private Vector3 position = Vector3.zero;

    private const float FALL_SPEED = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
       position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnergyResourceLanded)
        {
            return;
        }
        Fall();
    }

    /// <summary>
    /// エネルギー物資を落下させる
    /// </summary>
    private void Fall()
    {
        position.y = Mathf.Max(position.y - FALL_SPEED * Time.deltaTime, 0.0f);
        transform.position = position;
        if (transform.position.y <= 0.0f)
        {
            isEnergyResourceLanded = true;
        }
    }
}
