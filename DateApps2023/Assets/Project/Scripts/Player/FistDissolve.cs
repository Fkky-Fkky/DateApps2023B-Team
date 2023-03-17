//担当者:吉田理紗
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// プレイヤーのパンチ表示に関する処理を行うクラス
    /// </summary>
    public class FistDissolve : MonoBehaviour
    {
        [SerializeField]
        private float startTime = 0.5f;

        [SerializeField]
        private float endTime = 1.0f;

        [SerializeField]
        private float intervalTime = 0.2f;

        [SerializeField]
        private float pushForward = 0.8f;

        private new Renderer renderer = null;

        private float time = 0.0f;
        private float value = 0.0f;

        private const float MAX_VALUE = 1.0f;

        private bool isStartDissolve = false;
        private bool isEndDissolve = false;
        private bool isIntervalDissolve = false;

        // Start is called before the first frame update
        void Start()
        {
            renderer = GetComponent<Renderer>();
            time = 0.0f;
            value = 0.0f;

            isStartDissolve = true;
            isEndDissolve = false;
            isIntervalDissolve = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isStartDissolve)
            {
                StartDissolve();
            }
            if (isIntervalDissolve)
            {
                IntervalDissolve();
            }
            if (isEndDissolve)
            {
                EndDissolve();
            }
        }

        /// <summary>
        /// パンチの表示を開始する処理を行う
        /// </summary>
        void StartDissolve()
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

        /// <summary>
        /// 開始時と終了時の間の処理を行う
        /// </summary>
        void IntervalDissolve()
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

        /// <summary>
        /// パンチの表示を終了する処理を行う
        /// </summary>
        void EndDissolve()
        {
            time += Time.deltaTime;
            renderer.material.SetFloat("_DisAmount", value + time / endTime);
            if (time >= endTime)
            {
                time = 0.0f;
                value = MAX_VALUE;
                renderer.material.SetFloat("_DisAmount", value);
                Destroy(gameObject);
            }
        }
    }
}