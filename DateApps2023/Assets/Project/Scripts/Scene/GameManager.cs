using UnityEngine;
using UnityEngine.SceneManagement;

namespace Resistance
{
    /// <summary>
    /// ƒƒCƒ“‰æ–Ê‚Ì‘JˆÚ‚ÉŠÖ‚·‚éƒNƒ‰ƒX
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private BossManager bossManager = null;

        [SerializeField]
        private Resistance.AudioMaster audioMaster = null;

        [SerializeField]
        private Operator myOperator = null;

        [SerializeField]
        private Animator FadeOutAnimator = null;

        [SerializeField]
        private int MoveKillCount = 10;

        [SerializeField]
        private float AfterTime = 2.0f;

        [SerializeField]
        private float AnimTime = 1.0f;

        private float time = 0.0f;
        private float sceneMoveTime = 0.0f;

        private bool isFade = false;
        private bool isAudioFade = false;
        public bool IsGameOver { get { return bossManager.IsGameOver(); } }
        public bool IsGameStart { get { return myOperator.GetStartFlag(); } }

        const float SCENE_MOVE_TIME = 5.0f;

        private void Start()
        {
            time = 0.0f;
            sceneMoveTime = 0.0f;
            isFade = false;
            isAudioFade = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (BossCount.GetKillCount() >= MoveKillCount)
            {
                if (!isAudioFade)
                {
                    audioMaster.OnEndScene();
                    isAudioFade = true;
                }
                KillAllBoss();
            }

            if (IsGameOver)
            {
                sceneMoveTime += Time.deltaTime;
            }

            if (sceneMoveTime > SCENE_MOVE_TIME)
            {
                SceneManager.LoadScene("GameoverScene");
            }
        }

        /// <summary>
        /// İ’è‚µ‚½‰öb‚Ì”‚ğ“|‚µ‚½Û‚ÉŒÄ‚Ño‚·
        /// </summary>
        void KillAllBoss()
        {
            if (!isFade)
            {
                time += Time.deltaTime;
                if (time >= AnimTime)
                {
                    FadeOutAnimator.SetTrigger("FadeOut");
                    isFade = true;
                    time = 0.0f;
                }
            }
            else
            {
                time += Time.deltaTime;
                if (time >= AfterTime)
                {
                    SceneManager.LoadScene("ClearScene");
                }
            }
        }
    }
}