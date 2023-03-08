using UnityEngine;

public class Operator : MonoBehaviour
{
    [SerializeField] private Op_text optext = null;

    [SerializeField] private EnergyGenerator energy = null;

    [SerializeField] private CannonManager cannon = null;

    [SerializeField] private BossCSVGenerator csv = null;

    [SerializeField] private BossManager boss = null;

    enum GAME_STATE
    {
        TUTORIAL,

        GAME,
    }

    GAME_STATE gameState = GAME_STATE.TUTORIAL;

    Animator animator = null;

    private bool startFlag = false;

    private bool operatorTextFlag = false;

    private  float time = 0;
    
    private const float OPERATOR_TEXT_COOLTIME = 2;

    private const float RESET = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Tutorial();

        if (gameState == GAME_STATE.GAME)
        {
            Game();
        }
    }
    #region �I�y���[�^�[�`���[�g���A��
    /// <summary>
    /// �`���[�g���A���ŌĂԃA�j���[�^�[�̃g���K�[
    /// </summary>
    private void Tutorial()
    {
        if (gameState == GAME_STATE.TUTORIAL)
        {
            if (cannon.IsFirstCharge())
            {
                animator.SetTrigger("Button_ON");
            }

            if (BossCount.GetKillCount() == 1)
            {
                animator.SetTrigger("firing");
            }

            if (BossCount.GetKillCount() == 2)
            {
                animator.SetTrigger("boss_second_kill");
            }
        }
    }

    /// <summary>
    ///�`���[�g���A���ł̉��b���� 1���
    /// </summary>
    private void First()
    {
        csv.FirstBossGanaretar();
    }
    /// <summary>
    /// �`���[�g���A���ł̉��b�����@2���
    /// </summary>
    private void Second()
    {
        csv.SecondBossGanaretar();
    }
    /// <summary>
    /// �`���[�g���A���I��
    /// </summary>
    void TutorialEnd()
    {
        animator.SetTrigger("tutorial_end");
        gameState = GAME_STATE.GAME;
        startFlag = true;
    }
    /// <summary>
    /// �`���[�g���A���I���̃t���O�̎󂯓n��
    /// </summary>
    public bool GetStartFlag()
    {
        return startFlag;
    }
    /// <summary>
    /// �`���[�g���A������1��ڂ̃G�l���M�[��������
    /// </summary>
    void FirstEnergyGenerate()
    {
        energy.GenerateEnergy();
    }
    /// <summary>
    /// �`���[�g���A������2��ڂ̃G�l���M�[��������
    /// </summary>
    void SecondEnergyGenerate()
    {
         energy.GenerateEnergy();
    }
    #endregion

    #region �Q�[�����̃I�y���[�^�[�̃Z���t
    /// <summary>
    /// �Q�[�����̃I�y���[�^�[�̓���
    /// </summary>
    private void Game()
    {
        if (operatorTextFlag)
        {
            BossType();

            BossMove();
        }
        //�I�y���[�^�[�̃e�L�X�g�N�[���^�C��
        if (!operatorTextFlag)
        {
            time += Time.deltaTime;
        }

        if (time >= OPERATOR_TEXT_COOLTIME)
        {
            operatorTextFlag = true;
            time = RESET;
        }
    }
    #region �{�X�̏��󂯎��

    /// <summary>
    /// �{�X�̎�ނ̏��󂯎��
    /// </summary>
    private void BossType()
    {
        switch (boss.BossType())
        {
            case 1:
                SummonBoss();
                operatorTextFlag = false;
                break;

            case 2:
                SummonMiniBoss();
                operatorTextFlag = false;
                break;

            case 3:
                SummonBigBoss();
                operatorTextFlag = false;
                break;
        }
    }

    /// <summary>
    /// �{�X�̃r�[���`���[�W�A�ڋ߁A�����̏��󂯎��
    /// </summary>
    private void BossMove()
    {
        //�{�X�̍U���`���[�W
        if (boss.Charge())
        {
            BossAttackCharge();
            operatorTextFlag = false;
        }
        //�{�X�ڋߎ�
        if (boss.Danger())
        {
            Approach();
            operatorTextFlag = false;
        }
        //�{�X����
        if (boss.IsBossKill())
        {
            BossKill();
            operatorTextFlag = false;
        }
    }
    #endregion

    #region �A�j���[�^�[�̃g���K�[�Ăяo��
   
    /// <summary>
    ///�ʏ�{�X�o�����̃A�j���[�^�[�Ăяo�� 
    /// </summary>
    public void SummonBoss()
    { 
        animator.SetTrigger("boss");  
    }
    /// <summary>
    ///�~�j�{�X�o�����̃A�j���[�^�[�Ăяo�� 
    /// </summary>
    public void SummonMiniBoss()
    {
        animator.SetTrigger("miniboss");
    }
    /// <summary>
    /// �r�b�O�{�X�o�����̃A�j���[�^�[�Ăяo��
    /// </summary>
    public void SummonBigBoss()
    {
        animator.SetTrigger("bigboss");  
    }
    /// <summary>
    ///�{�X���j���̃A�j���[�^�[�Ăяo��
    /// </summary>
    public void BossKill()
    {
        animator.SetTrigger("kill");  
    }
    /// <summary>
    /// �{�X�ڋߎ��̃A�j���[�^�[�Ăяo��
    /// </summary>
    public void Approach()
    {
        animator.SetTrigger("Approach");
    }
    /// <summary>
    /// �{�X�̍U���̃`���[�W�̃A�j���[�^�[�Ăяo��
    /// </summary>
    public void BossAttackCharge()
    {
        animator.SetTrigger("charge");
    }
    #endregion
}
#endregion