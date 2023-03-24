//担当者:武田碧

using System.Collections.Generic;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// ボスの生成
    /// </summary>
    public class BossCSVGenerator : MonoBehaviour
    {
        [SerializeField]
        private int bossCountMax = 10;
        [SerializeField]
        private float sidePos = 100.0f;
        [SerializeField]
        private GameObject nomalBoss = null;
        [SerializeField]
        private GameObject miniBoss = null;
        [SerializeField]
        private GameObject bigBoss = null;
        [SerializeField]
        private BossManager bossManager = null;
        [SerializeField]
        private Operator myOperator = null;
        [SerializeField]
        private BossCSV bossCSV = null;

        private int bossCountOne = 1;
        private int messageCount = 0;
        private int bossType = 0;
        private int bossLane = 0;
        private int bossHpDate = 0;

        private float leftPosX = 0.0f;
        private float rightPosX = 0.0f;
        private float time = 0.0f;
        private float dangerOffTime = 0.0f;
        private float killOffTime = 0.0f;
        private float bossTypeOffTime = 0.0f;
        private float attackIntervalDate = 0.0f;
        private float posZ = 0.0f;
        private float moveSpeedDate = 0.0f;

        private string bossTypeDate = null;

        private List<BossDamage> bossList = new List<BossDamage>();
        private List<BossMove> bossMoveList = new List<BossMove>();
        private List<BossAttack> bossAttackList = new List<BossAttack>();

        private GameObject boss = null;
        private BossCount bossCount = null;

        /// <summary>
        /// ボスの種類
        /// </summary>
        private enum BOSS_TYPE
        {
            None,
            NOMAL,
            MINI,
            BIG
        }
        /// <summary>
        /// 出現レーン
        /// </summary>
        private enum LANE
        {
            LEFT = 1,
            CENTER = 2,
            RIGHT = 3
        }

        /// <summary>
        /// ボスが倒れた
        /// </summary>
        public bool IsKill { get; private set; }
        /// <summary>
        /// ボスがかなり近いとき
        /// </summary>
        public bool IsDanger { get; private set; }
        /// <summary>
        /// チャージ開始
        /// </summary>
        public bool IsCharge { get; private set; }
        /// <summary>
        /// ゲームオーバー
        /// </summary>
        public bool IsGameOver { get; private set; }

        const int MESSAGE_COUNT_MAX   = 1;
        const int FIRST_BOSS_LANE     = 2;
        const int SECOND_BOSS_LANE    = 3;
        const int THIRD_BOSS_LANE     = 1;

        const int TUTORIAL_FIRST_BOSS_HP  = 4;
        const int TUTORIAL_SECOND_BOSS_HP = 3;
        const int TUTORIAL_THIRD_BOSS_HP  = 5;

        const int TUTORIAL_FIRST_BOSS_SPEED = 3;
        const int TUTORIAL_SECOND_BOSS_SPEED = 4;

        const float CENTER_POS_X               = 0.0f;
        const float TUTORIAL_BOSS_POS_Z        = 345.0f;
        const float TUTORIAL_SECOND_BOSS_POS_Z = 300.0f;
        const float TUTORIAL_ATTACKINTERVAL    = 10000.0f;
        const float FALL_POSITION              = 500.0f;
        const float MESSAGE_OFF_TIME_MAX       = 0.05f;
        const float BOSS_TYPE_OFF_TIME_MAX     = 0.03f;

        const string TUTORIAL_BOSS_TYPE = "Nomal";

        void Start()
        {
            bossCount = GetComponent<BossCount>();
            IsKill = false;
            IsGameOver = false;
            IsDanger = false;
            leftPosX = -sidePos;
            rightPosX = sidePos;
        }

        void Update()
        {
            for (int i = 0; i < bossList.Count; i++)
            {
                if (bossList[i].IsFellDown)
                {
                    if (messageCount < MESSAGE_COUNT_MAX)
                    {
                        IsKill = true;
                        messageCount++;
                    }
                    bossCount.SetBossKillCount();
                    bossMoveList.RemoveAt(i);
                    bossList.RemoveAt(i);
                }
            }

            //スタートフラグ
            if (!myOperator.GetStartFlag())
            {
                return;
            }

            if (bossCountOne <= bossCountMax)
            {
                time += Time.deltaTime;
                if (time >= bossCSV.AppearanceTime[bossCountOne])
                {
                    bossTypeDate       = bossCSV.BossType[bossCountOne];
                    bossLane           = bossCSV.AppearanceLane[bossCountOne];
                    attackIntervalDate = bossCSV.AttackIntervalTime[bossCountOne];
                    posZ               = bossCSV.PositionZ[bossCountOne];
                    bossHpDate         = bossCSV.BossHp[bossCountOne];
                    moveSpeedDate      = bossCSV.BossSpeed[bossCountOne];

                    BossGanarater();
                }
            }


            for (int i = 0; i < bossMoveList.Count; i++)
            {
                if (bossMoveList[i].IsHazard)
                {
                    IsDanger = true;
                }
            }

            for (int i = 0; i < bossAttackList.Count; i++)
            {
                if (bossAttackList[i].IsCharge)
                {
                    IsCharge = true;
                    break;
                }
                IsCharge = false;
            }

            for (int i = 0; i < bossMoveList.Count; i++)
            {
                if (bossMoveList[i].IsGameOver)
                {
                    IsGameOver = true;
                    break;
                }
                IsGameOver = false;
            }

            MessageCancel();
        }

        /// <summary>
        /// オペレーターが同じことを喋らないようにする
        /// </summary>
        private void MessageCancel()
        {
            if (IsKill)
            {
                killOffTime += Time.deltaTime;
                if (killOffTime >= MESSAGE_OFF_TIME_MAX)
                {
                    messageCount = 0;
                    IsKill = false;

                    killOffTime = 0.0f;
                }
            }

            if (IsDanger)
            {
                dangerOffTime += Time.deltaTime;
                if (dangerOffTime >= MESSAGE_OFF_TIME_MAX)
                {
                    IsDanger = false;
                    dangerOffTime = 0.0f;
                }
            }

            if (bossType != (int)BOSS_TYPE.None)
            {
                bossTypeOffTime += Time.deltaTime;
                if (bossTypeOffTime >= BOSS_TYPE_OFF_TIME_MAX)
                {
                    bossType = (int)BOSS_TYPE.None;
                    bossTypeOffTime = 0.0f;
                }
            }

        }
        /// <summary>
        /// ボスの生成
        /// </summary>
        private void BossGanarater()
        {
            switch (bossLane)
            {
                case (int)LANE.LEFT:
                    if (!bossManager.IsLeftLine)
                    {
                        BossAppearanceDate(leftPosX);
                    }
                    break;
                case (int)LANE.CENTER:
                    if (!bossManager.IsCenterLine)
                    {
                        BossAppearanceDate(CENTER_POS_X);
                    }
                    break;
                case (int)LANE.RIGHT:
                    if (!bossManager.IsRightLine)
                    {
                        BossAppearanceDate(rightPosX);
                    }
                    break;
            }
        }
        /// <summary>
        /// ボスのデータを入れる
        /// </summary>
        /// <param name="bossPositionX">出現するX軸</param>
        private void BossAppearanceDate(float bossPositionX)
        {
            BossType();
            boss.transform.position = new Vector3(bossPositionX, FALL_POSITION, posZ);
            bossList.Add(boss.GetComponent<BossDamage>());
            bossMoveList.Add(boss.GetComponent<BossMove>());
            bossAttackList.Add(boss.GetComponent<BossAttack>());
            if (myOperator.GetStartFlag())
            {
                bossCountOne++;
            }
            time = 0.0f;
        }
        /// <summary>
        /// 出現させるボスの種類
        /// </summary>
        private void BossType()
        {
            switch (bossTypeDate)
            {
                case "Nomal":
                    boss = Instantiate(nomalBoss);
                    bossType = (int)BOSS_TYPE.NOMAL;
                    break;
                case "Mini":
                    boss = Instantiate(miniBoss);
                    bossType = (int)BOSS_TYPE.MINI;
                    break;
                case "Big":
                    boss = Instantiate(bigBoss);
                    bossType = (int)BOSS_TYPE.BIG;
                    break;
            }
        }

        /// <summary>
        /// 攻撃する時間間隔を返す
        /// </summary>
        /// <returns>攻撃する時間間隔</returns>
        public float AttackIntervalTime()
        {
            return attackIntervalDate;
        }
        /// <summary>
        /// ボスの体力を返す
        /// </summary>
        /// <returns>ボスの体力</returns>
        public int BossHP()
        {
            return bossHpDate;
        }
        /// <summary>
        /// ボスの移動スピードを返す
        /// </summary>
        /// <returns>ボスの移動スピード</returns>
        public float BossMoveSpeed()
        {
            return moveSpeedDate;
        }
        /// <summary>
        /// ボスの種類のデータを返す
        /// </summary>
        /// <returns>ボスの種類のデータ</returns>
        public int BossTypeDate()
        {
            return bossType;
        }
        /// <summary>
        /// チュートリアル用のボス1
        /// </summary>
        public void FirstBossGanaretar()
        {
            bossTypeDate       = TUTORIAL_BOSS_TYPE;
            bossLane           = FIRST_BOSS_LANE;
            attackIntervalDate = TUTORIAL_ATTACKINTERVAL;
            posZ               = TUTORIAL_BOSS_POS_Z;
            bossHpDate         = TUTORIAL_FIRST_BOSS_HP;
            moveSpeedDate      = TUTORIAL_FIRST_BOSS_SPEED;

            BossGanarater();
        }

        /// <summary>
        /// チュートリアル用のボス2
        /// </summary>
        public void SecondBossGanaretar()
        {
            bossTypeDate       = TUTORIAL_BOSS_TYPE;
            bossLane           = SECOND_BOSS_LANE;
            attackIntervalDate = TUTORIAL_ATTACKINTERVAL;
            posZ               = TUTORIAL_SECOND_BOSS_POS_Z;
            bossHpDate         = TUTORIAL_SECOND_BOSS_HP;
            moveSpeedDate      = TUTORIAL_SECOND_BOSS_SPEED;

            BossGanarater();
        }
        /// <summary>
        /// チュートリアル用のボス3
        /// </summary>
        public void ThirdBossGanaretar()
        {
            bossTypeDate       = TUTORIAL_BOSS_TYPE;
            bossLane           = THIRD_BOSS_LANE;
            attackIntervalDate = TUTORIAL_ATTACKINTERVAL;
            posZ               = TUTORIAL_SECOND_BOSS_POS_Z;
            bossHpDate         = TUTORIAL_THIRD_BOSS_HP;
            moveSpeedDate      = TUTORIAL_SECOND_BOSS_SPEED;

            BossGanarater();
        }

    }
}