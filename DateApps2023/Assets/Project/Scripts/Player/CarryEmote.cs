using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarryEmote : MonoBehaviour
{
    #region
    private int myPlayerNo;
    private float time = 0;
    private float scaleTime = 0;

    private SpriteRenderer spriteRenderer;
    private Transform cameraPos;

    [SerializeField]
    private Sprite carryEmoteIcon = null;

    [SerializeField]
    private float startTime = 0.4f;

    [SerializeField]
    private float endTime = 0.4f;

    [SerializeField]
    private float moveY = 0.2f;

    [SerializeField]
    private float smallTime = 0.4f;

    [SerializeField]
    private float bigTime = 0.4f;

    [SerializeField]
    private float sizeChange = 0.2f;

    [SerializeField]
    private float startSizeChange = 0.2f;

    private bool isEmote = false;
    private bool isSmall = false;
    private bool isBig = false;
    private bool isEnd = false;

    private Vector3 defaultPos;
    private Vector3 movePos = Vector3.zero;
    private Vector3 defaultSize;
    private Vector3 setSize;

    const float MIRROR_ROT_Y = -180.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraPos = Camera.main.transform;

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

            if (isEnd)
            {
                OnEndTime();
            }
            else
            {
                OnStartTime();
            }
        }
    }

    public void CallStartCarryEmote()
    {
        isEmote = true;
        spriteRenderer.sprite = carryEmoteIcon;
        isBig = true;
        isEnd = false;
    }

    public void CallEndCarryEmote()
    {
        isEnd = true;
        time = 0.0f;
    }

    void OnStartTime()
    {
        if (time <= startTime)
        {
            gameObject.transform.localPosition += movePos * Time.deltaTime;
            setSize += new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
            gameObject.transform.localScale = setSize;
        }
    }

    void OnEndTime()
    {
        if (time <= endTime)
        {
            gameObject.transform.localPosition += movePos * Time.deltaTime;
            isSmall = false;
            isBig = false;
            setSize -= new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
            gameObject.transform.localScale = setSize;
        }
        else
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
    }

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

    public void GetPlayerNo(int setNumber)
    {
        myPlayerNo = setNumber;
    }
}
