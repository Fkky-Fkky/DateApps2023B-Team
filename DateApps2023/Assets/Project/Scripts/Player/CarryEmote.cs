using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarryEmote : MonoBehaviour
{
    /// <summary>
    /// 運搬中の人数が標準人数以下の場合にエモート(汗)の処理
    /// 基本的に外部のスクリプトから呼び出す
    /// </summary>
    #region
    [SerializeField]
    private Sprite carryEmoteIcon = null;

    [SerializeField]
    private EmoteData myEmoteData = null;

    private SpriteRenderer spriteRenderer = null;
    private Transform cameraPos = null;

    private float time = 0;
    private float scaleTime = 0;

    private float startTime = 0.4f;
    private float moveY = 0.2f;
    private float smallTime = 0.4f;
    private float bigTime = 0.4f;
    private float sizeChange = 0.2f;
    private float startSizeChange = 0.2f;

    private bool isEmote = false;
    private bool isSmall = false;
    private bool isBig = false;
    private bool isEnd = false;

    private Vector3 defaultPos = Vector3.zero;
    private Vector3 movePos = Vector3.zero;
    private Vector3 defaultSize = Vector3.zero;
    private Vector3 setSize = Vector3.zero;

    const float MIRROR_ROT_Y = -180.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraPos = Camera.main.transform;

        time = 0.0f;
        scaleTime = 0.0f;

        startTime = myEmoteData.StartTime;
        moveY = myEmoteData.MoveY;
        smallTime = myEmoteData.SmallTime;
        bigTime = myEmoteData.BigTime;
        sizeChange = myEmoteData.SizeChange;
        startSizeChange = myEmoteData.StartSizeChange;

        isEmote = false;
        isSmall = false;
        isBig = false;
        isEnd = false;

        defaultPos = new Vector3(0.0f, gameObject.transform.localPosition.y, 0.0f);
        movePos.y = moveY;
        defaultSize = gameObject.transform.localScale;
        setSize = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmote)
        {
            time += Time.deltaTime;
            this.transform.LookAt(transform.position + cameraPos.rotation * Vector3.forward, cameraPos.rotation * Vector3.up);
            if(transform.parent.gameObject.transform.rotation.y >= 0)
            {
                transform.Rotate(new Vector3(0.0f, MIRROR_ROT_Y, 0.0f));
            }

            ChangeSize();

            if (!isEnd)
            {
                OnStartTime();
            }
        }
    }

    /// <summary>
    /// エモートの開始を外部から呼び出す
    /// </summary>
    public void CallStartCarryEmote()
    {
        isEmote = true;
        spriteRenderer.sprite = carryEmoteIcon;
        isBig = true;
        isEnd = false;
    }

    /// <summary>
    /// エモートの終了を外部から呼び出す
    /// </summary>
    public void CallEndCarryEmote()
    {
        isEmote = false;
        time = 0;
        scaleTime = 0;
        spriteRenderer.sprite = null;
        isSmall = false;
        isBig = false;
        gameObject.transform.localPosition = defaultPos;
        gameObject.transform.localScale = defaultSize;
        setSize = defaultSize;
    }

    /// <summary>
    /// エモートが開始された後の数秒間で行う処理
    /// </summary>
    void OnStartTime()
    {
        if (time <= startTime)
        {
            gameObject.transform.localPosition += movePos * Time.deltaTime;
            setSize += new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
            gameObject.transform.localScale = setSize;
        }
    }

    /// <summary>
    /// 時間に応じて表示した自身のサイズを拡大縮小する
    /// </summary>
    void ChangeSize()
    {
        scaleTime += Time.deltaTime;

        if (!isSmall && isBig)
        {
            setSize += new Vector3(sizeChange, sizeChange, sizeChange) * Time.deltaTime;
            gameObject.transform.localScale = setSize;

            if (scaleTime >= smallTime)
            {
                scaleTime = 0;
                isSmall = true;
                isBig = false;
            }
        }
        else if (isSmall && !isBig)
        {
            setSize -= new Vector3(sizeChange, sizeChange, sizeChange) * Time.deltaTime;
            gameObject.transform.localScale = setSize;
            if (scaleTime >= bigTime)
            {
                scaleTime = 0;
                isSmall = false;
                isBig = true;
            }
        }
    }
}
