using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEmote : MonoBehaviour
{
    #region
    private int myPlayerNo;
    private float time = 0;
    private float scaleTime = 0;

    private SpriteRenderer spriteRenderer;
    private Transform cameraPos;

    [SerializeField]
    private Sprite emoteIconL = null;

    [SerializeField]
    private Sprite emoteIconR = null;

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

    private bool isEmote = false;
    private bool isSmall = false;
    private bool isBig = false;

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
        if (!isEmote)
        {
            PressEmote();
        }
        else
        {
            time += Time.deltaTime;
            this.transform.LookAt(transform.position + cameraPos.rotation * Vector3.forward, cameraPos.rotation * Vector3.up);

            ChangeSize();
            
            if (time >= emoteTime)
            {
                EndEmote();
            }
            else if(time < emoteTime && time >= emoteTime - endTime)
            {
                BeforeEndEmote();
            }
            else if(time <= startTime)
            {
                StartEmote();
            }
        }
    }

    void PressEmote()
    {
        if (Gamepad.all[myPlayerNo].leftShoulder.wasPressedThisFrame)
        {
            isEmote = true;
            spriteRenderer.sprite = emoteIconL;
            isBig = true;
        }
        else if (Gamepad.all[myPlayerNo].rightShoulder.wasPressedThisFrame)
        {
            isEmote = true;
            spriteRenderer.sprite = emoteIconR;
            isBig = true;
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

    void EndEmote()
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

    void BeforeEndEmote()
    {
        gameObject.transform.localPosition += movePos * Time.deltaTime;
        isSmall = false;
        isBig = false;
        setSize -= new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
        gameObject.transform.localScale = setSize;
    }

    void StartEmote()
    {
        gameObject.transform.localPosition += movePos * Time.deltaTime;
        setSize += new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
        gameObject.transform.localScale = setSize;
    }

    public void GetPlayerNo(int setNumber)
    {
        myPlayerNo = setNumber;
    }
}
