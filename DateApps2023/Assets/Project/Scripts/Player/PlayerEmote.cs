using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEmote : MonoBehaviour
{
    /// <summary>
    /// �v���C���[�̃G���[�g���o���X�N���v�g
    /// L�F�w���v�@R�F�i�C�X
    /// </summary>
    #region
    [SerializeField]
    private Sprite emoteIconL = null;

    [SerializeField]
    private Sprite emoteIconR = null;

    [SerializeField]
    private EmoteData myEmoteData = null;

    [SerializeField]
    private float emoteTime = 2.0f;

    private SpriteRenderer spriteRenderer = null;
    private Transform cameraPos = null;

    private int myPlayerNo = 5;
    private float time = 0.0f;
    private float scaleTime = 0.0f;

    private float startTime = 0.4f;
    private float endTime = 0.4f;
    private float moveY = 0.2f;
    private float smallTime = 0.4f;
    private float bigTime = 0.4f;
    private float sizeChange = 0.2f;
    private float startSizeChange = 0.2f;

    private bool isEmote = false;
    private bool isSmall = false;
    private bool isBig = false;

    private Vector3 defaultPos = Vector3.zero;
    private Vector3 movePos = Vector3.zero;
    private Vector3 defaultSize = Vector3.zero;
    private Vector3 setSize = Vector3.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraPos = Camera.main.transform;

        time = 0.0f;
        scaleTime = 0.0f;

        startTime = myEmoteData.StartTime;
        endTime = myEmoteData.EndTime;
        moveY = myEmoteData.MoveY;
        smallTime = myEmoteData.SmallTime;
        bigTime = myEmoteData.BigTime;
        sizeChange = myEmoteData.SizeChange;
        startSizeChange = myEmoteData.StartSizeChange;

        isEmote = false;
        isSmall = false;
        isBig = false;

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

    /// <summary>
    /// �v���C���[���Ή�����{�^�������������𔻒肷��
    /// </summary>
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

    /// <summary>
    /// ���Ԃɉ����ĕ\���������g�̃T�C�Y���g��k������
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

    /// <summary>
    /// �G���[�g��\�����I������ۂɌĂяo��
    /// </summary>
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

    /// <summary>
    /// �G���[�g�̕\�����I���O�ɌĂяo��
    /// </summary>
    void BeforeEndEmote()
    {
        gameObject.transform.localPosition += movePos * Time.deltaTime;
        isSmall = false;
        isBig = false;
        setSize -= new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
        gameObject.transform.localScale = setSize;
    }

    /// <summary>
    /// �G���[�g���n�܂����ۂɌĂяo��
    /// </summary>
    void StartEmote()
    {
        gameObject.transform.localPosition += movePos * Time.deltaTime;
        setSize += new Vector3(startSizeChange, startSizeChange, startSizeChange) * Time.deltaTime;
        gameObject.transform.localScale = setSize;
    }

    /// <summary>
    /// ���g�̃v���C���[�ԍ����O������擾����
    /// </summary>
    /// <param name="setNumber"></param>
    public void GetPlayerNo(int setNumber)
    {
        myPlayerNo = setNumber;
    }
}
