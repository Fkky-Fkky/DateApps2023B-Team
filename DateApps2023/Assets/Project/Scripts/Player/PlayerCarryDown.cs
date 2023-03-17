//担当者:吉田理紗
using UnityEngine;
using UnityEngine.InputSystem;

namespace Resistance
{
    /// <summary>
    /// プレイヤーの運搬に関するクラス
    /// </summary>
    public class PlayerCarryDown : MonoBehaviour
    {
        private PlayerMove playermove = null;
        private CarryEnergy energyItem = null;
        private CarryCannon cannonItem = null;

        private GameObject carryItem = null;
        private Rigidbody rb = null;
        private BoxCollider myCol = null;

        private int myPlayerNo = 5;
        private int myGroupNo = 1;

        private bool isCarry = false;
        private bool isCanUsed = false;
        private bool isCarryDamage = false;

        void Start()
        {
            playermove = GetComponentInParent<PlayerMove>();
            energyItem = null;
            cannonItem = null;

            carryItem = null;
            rb = GetComponentInParent<Rigidbody>();
            myCol = GetComponent<BoxCollider>();

            myGroupNo = 1;
            isCarry = false;
            isCanUsed = false;
            isCarryDamage = false;
        }

        void Update()
        {
            if (!isCarryDamage)
            {
                if (Gamepad.all[myPlayerNo].bButton.wasPressedThisFrame)
                {
                    OnPressCarryButton();
                }
                if (Gamepad.all[myPlayerNo].bButton.wasReleasedThisFrame)
                {
                    OnReleaseCarryButton();
                }
            }

            if (isCarry)
            {
                myCol.enabled = false;
            }
            if (carryItem == null)
            {
                isCarry = false;
                isCanUsed = false;
                myCol.enabled = true;
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (isCarry)
            {
                return;
            }
            if (collision.gameObject.CompareTag("item")
                    || collision.gameObject.CompareTag("Cannon"))
            {
                isCanUsed = true;
                carryItem = collision.gameObject;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (isCarry)
            {
                return;
            }
            if (collision.gameObject.CompareTag("item")
                    || collision.gameObject.CompareTag("Cannon"))
            {
                isCanUsed = false;
                carryItem = null;
            }
        }

        /// <summary>
        /// プレイヤーが運搬ボタンを押した際の処理を行う
        /// </summary>
        void OnPressCarryButton()
        {
            if (isCarry)
            {
                return;
            }
            if (isCanUsed)
            {
                CheckItemTag();
            }
        }

        /// <summary>
        /// プレイヤーが運搬ボタンを離した際の処理を行う
        /// </summary>
        void OnReleaseCarryButton()
        {
            if (isCarry)
            {
                CarryCancel();
            }
        }

        /// <summary>
        /// 持とうとしているオブジェクトの種類を判定する
        /// </summary>
        void CheckItemTag()
        {
            if (carryItem.CompareTag("item"))
            {
                energyItem = carryItem.GetComponent<CarryEnergy>();
                energyItem.GetGrabPoint(this.gameObject);
                myGroupNo = energyItem.GroupNumber;
                isCarry = true;
                isCanUsed = false;
                playermove.GetItem(myGroupNo);
            }
            if (carryItem.CompareTag("Cannon"))
            {
                if (carryItem.GetComponent<CannonShot>().IsShotting)
                {
                    return;
                }
                cannonItem = carryItem.GetComponent<CarryCannon>();
                cannonItem.GetGrabPoint(this.gameObject);
                myGroupNo = cannonItem.GroupNumber;
                isCarry = true;
                isCanUsed = false;
                playermove.GetItem(myGroupNo);
            }
        }

        /// <summary>
        /// プレイヤーが運搬を終了する際の処理を行う
        /// </summary>
        public void CarryCancel()
        {
            playermove.RemoveItem();
            rb = GetComponentInParent<Rigidbody>();

            isCarry = false;
            isCanUsed = false;
            carryItem = null;
            energyItem = null;
            cannonItem = null;
            myCol.enabled = true;
        }

        /// <summary>
        /// 自身のプレイヤー番号を取得する
        /// </summary>
        /// <param name="parentNumber">プレイヤー番号</param>
        public void GetPlayerNo(int parentNumber)
        {
            myPlayerNo = parentNumber;
        }

        /// <summary>
        /// プレイヤーがダメージを受けた際に呼び出す
        /// </summary>
        public void OnCarryDamage()
        {
            isCarryDamage = true;
        }

        /// <summary>
        /// プレイヤーがダメージから回復した際に呼び出す
        /// </summary>
        public void OffCarryDamage()
        {
            isCarryDamage = false;
        }
    }
}