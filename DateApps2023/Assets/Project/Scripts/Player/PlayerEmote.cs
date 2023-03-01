using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEmote : MonoBehaviour
{
    #region
    private int myPlayerNo;
    float time = 0;
    float scaleTime = 0;

    private SpriteRenderer spriteRenderer;
    private Transform cameraPos;

    [SerializeField]
    private Sprite LShoulderIcon = null;

    [SerializeField]
    private Sprite RShoulderIcon = null;

    [SerializeField]
    private float emoteTime = 2.0f;

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

    private bool IsEmote = false;
    private bool IsSmall = false;
    private bool IsBig = false;

    private Vector3 defaultPos;
    private Vector3 movePos = Vector3.zero;
    private Vector3 defaultSize;
    private Vector3 setSize;
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
        if (!IsEmote)
        {
           PressEmoteButton();
        }
        else
        {
            InEmoteTime();
        }
    }

    void PressEmoteButton()
    {
        if (Gamepad.all[myPlayerNo].leftShoulder.wasPressedThisFrame)
        {
            IsEmote = true;
            spriteRenderer.sprite = LShoulderIcon;
            IsBig = true;
        }
        else if (Gamepad.all[myPlayerNo].rightShoulder.wasPressedThisFrame)
        {
            IsEmote = true;
            spriteRenderer.sprite = RShoulderIcon;
            IsBig = true;
        }
    }

    void InEmoteTime()
    {
        time += Time.deltaTime;
        this.transform.LookAt(transform.position + cameraPos.rotation * Vector3.forward, cameraPos.rotation * Vector3.up);

        ChangeSize();

        if (time >= emoteTime)
        {
            InEndEmote();
        }
        else if (time < emoteTime && time >= emoteTime - endTime)
        {
            InBeforeEndEmote();
        }
        else if (time <= startTime)
        {
            InStartEmote();
        }
    }

    void InEndEmote()
    {
        IsEmote = false;
        time = 0;
        scaleTime = 0;
        spriteRenderer.sprite = null;
        IsSmall = false;
        IsBig = false;
        gameObject.transform.localPosition = defaultPos;
        gameObject.transform.localScale = defaultSize;
        setSize = defaultSize;
    }

    void InBeforeEndEmote()
    {
        gameObject.transform.localPosition += movePos * Time.deltaTime;
        IsSmall = false;
        IsBig = false;
        setSize -= new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
        gameObject.transform.localScale = setSize;
    }

    void InStartEmote()
    {
        gameObject.transform.localPosition += movePos * Time.deltaTime;
        setSize += new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
        gameObject.transform.localScale = setSize;
    }

    void ChangeSize()
    {
        scaleTime += Time.deltaTime;

        if (!IsSmall && IsBig)
        {
            setSize += new Vector3(sizeChange, sizeChange, sizeChange) * Time.deltaTime;
            gameObject.transform.localScale = setSize;

            if (scaleTime >= smallTime)
            {
                scaleTime = 0;
                IsSmall = true;
                IsBig = false;
            }
        }
        else if (IsSmall && !IsBig)
        {
            setSize -= new Vector3(sizeChange, sizeChange, sizeChange) * Time.deltaTime;
            gameObject.transform.localScale = setSize;
            if (scaleTime >= bigTime)
            {
                scaleTime = 0;
                IsSmall = false;
                IsBig = true;
            }
        }
    }

    public void GetPlayerNo(int setNumber)
    {
        myPlayerNo = setNumber;
    }
}
