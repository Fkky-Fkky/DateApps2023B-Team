//担当者:吉田理紗
using TMPro;
using UnityEngine;

namespace Resistance
{
    /// <summary>
    /// 運搬中のグループ配下のオブジェクト処理に関するクラス
    /// </summary>
    public class GroupManager : MonoBehaviour
    {
        [SerializeField]
        private int carryTextOrderInLayer = 0;

        [SerializeField]
        private Sprite arrowSprite = null;

        private Rigidbody rb = null;
        private GroupMove groupMove = null;
        private TextMeshPro carryText = null;
        private Outline outline = null;
        private SpriteRenderer directioningArrow = null;

        private float defaultMass = 1.0f;

        private const int ONLY_CHILDCOUNT = 1;
        private const float ADD_MASS = 10.0f;

        private const float ENERGY_ARROW_ROTATE_Y = 90.0f;
        private const float CANNON_ARROW_ROTATE_Y = -90.0f;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.Sleep();
            rb.useGravity = false;
            defaultMass = rb.mass;

            groupMove = GetComponent<GroupMove>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.CompareTag("Player"))
                    {
                        transform.GetChild(i).gameObject.GetComponent<PlayerDamage>().JudgeCapture(other.gameObject);
                    }
                }
            }

            if (other.gameObject.CompareTag("BossAttack"))
            {
                DamageChild();
            }
        }

        /// <summary>
        /// グループ配下のオブジェクトが一つだけ残っていないかを判定する
        /// 残っていた場合はタグで区別してオブジェクトの関数を呼び出す
        /// </summary>
        public void CheckOnlyChild()
        {
            if (transform.childCount <= ONLY_CHILDCOUNT)
            {
                ItemOutGroup();
                AllFragFalse();
            }
        }

        /// <summary>
        /// 運搬するアイテムの情報を取得する
        /// アイテムがグループ配下に入る際に呼び出す
        /// </summary>
        /// <param name="itemSize">アイテムのサイズ(重さ)</param>
        /// <param name="gameObject">アイテムのゲームオブジェクト</param>
        public void GetItemSize(int itemSize, GameObject gameObject)
        {
            directioningArrow = gameObject.GetComponentInChildren<SpriteRenderer>();
            directioningArrow.sprite = arrowSprite;
            carryText = gameObject.GetComponentInChildren<TextMeshPro>();
            carryText.gameObject.GetComponent<MeshRenderer>().sortingOrder = carryTextOrderInLayer;
            outline = gameObject.GetComponentInChildren<Outline>();
            outline.enabled = false;
            if (gameObject.CompareTag("item"))
            {
                groupMove.SetItenSizeCount(itemSize, ENERGY_ARROW_ROTATE_Y);
            }
            else if (gameObject.CompareTag("Cannon"))
            {
                rb.mass *= ADD_MASS;
                groupMove.SetItenSizeCount(itemSize, CANNON_ARROW_ROTATE_Y);
            }
            groupMove.CheckPlayerCount();
        }

        /// <summary>
        /// 進行方向の矢印の向きを設定する
        /// </summary>
        /// <param name="direction">向きのベクトル</param>
        public void SetDirection(Vector3 direction)
        {
            directioningArrow.gameObject.transform.localRotation = Quaternion.LookRotation(direction);
        }

        /// <summary>
        /// グループがグループ配下のオブジェクトを離す際に呼び出す
        /// </summary>
        public void ReleaseChild()
        {
            groupMove.SetNullPlayer();
            AllFragFalse();
        }

        /// <summary>
        /// 運搬中のグループがダメージを受けた際に呼び出す
        /// グループ配下のオブジェクトを離す
        /// </summary>
        private void DamageChild()
        {
            groupMove.SetNullPlayer();
            ItemOutGroup();
            AllFragFalse();
        }

        /// <summary>
        /// グループ配下のアイテムのグループからぬける関数を呼び出す
        /// </summary>
        void ItemOutGroup()
        {
            if (transform.childCount >= ONLY_CHILDCOUNT)
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.CompareTag("item"))
                    {
                        directioningArrow.sprite = null;
                        directioningArrow = null;
                        transform.GetChild(i).gameObject.GetComponent<CarryEnergy>().OutGroup();
                    }
                    else if (transform.GetChild(i).gameObject.CompareTag("Cannon"))
                    {
                        directioningArrow.sprite = null;
                        directioningArrow = null;
                        transform.GetChild(i).gameObject.GetComponent<CarryCannon>().OutGroup();
                    }
                }
            }
        }

        /// <summary>
        /// 運搬中のグループが運搬を終了する際に呼び出す
        /// </summary>
        void AllFragFalse()
        {
            groupMove.FalseGamepad();
            outline.enabled = true;
            outline = null;
            groupMove.CheckPlayerCount();
            carryText.text = null;
            carryText = null;
            rb.mass = defaultMass;
            rb.velocity = Vector3.zero;
        }

        /// <summary>
        /// 運搬中のテキスト表示に関する処理を行う
        /// </summary>
        /// <param name="playerCount">グループ配下のプレイヤーの人数</param>
        /// <param name="needCarryCount">運搬に必要なプレイヤーの人数</param>
        public void CheckCarryText(int playerCount, int needCarryCount)
        {
            if (carryText != null)
            {
                carryText.text = playerCount.ToString("0") + "/" + needCarryCount.ToString("0");
                CheckNeedCarryText(playerCount, needCarryCount);
            }

        }

        /// <summary>
        /// 運搬中のUI表示に関する処理を行う
        /// </summary>
        /// <param name="playerCount">グループ配下のプレイヤーの人数</param>
        /// <param name="needCarryCount">運搬に必要なプレイヤーの人数</param>
        void CheckNeedCarryText(int playerCount, int needCarryCount)
        {
            if (playerCount >= needCarryCount)
            {
                carryText.color = Color.white;
                for (int i = 0; i < groupMove.ChildPlayer.Length; i++)
                {
                    if (groupMove.ChildPlayer[i] != null)
                    {
                        groupMove.ChildPlayer[i].GetComponent<PlayerMove>().EndCarryEmote();
                    }
                }
            }
            else if (playerCount <= 0)
            {
                carryText.text = null;
            }
            else
            {
                carryText.color = Color.red;
                for (int i = 0; i < groupMove.ChildPlayer.Length; i++)
                {
                    if (groupMove.ChildPlayer[i] != null)
                    {
                        groupMove.ChildPlayer[i].GetComponent<PlayerMove>().StartCarryEmote();
                    }
                }
            }
        }
    }
}