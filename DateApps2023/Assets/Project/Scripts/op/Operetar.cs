using UnityEngine;

public class Operetar : MonoBehaviour
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

    

    private bool startFlag = false;

    private bool operetarTextFlag = false;

    private bool onOperetarTextFlag = false;

    private float time = 0;

    Animator animator = null;

    private float OPERETAR_TEXT_COOLTIME = 2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region �I�y���[�^�[�`���[�g���A��
        if (gameState == GAME_STATE.TUTORIAL)
        {
            time += Time.deltaTime;

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

        if (gameState == GAME_STATE.GAME)
        {
            Game();
        }
    }
    //���b����
    private void First()
    {
        csv.FirstBossGanaretar();
    }
    //2��ڂ̉��b����
    private void Second()
    {
        csv.SecondBossGanaretar();
    }
    //�`���[�g���A���I���̃t���O
    void TutorialEnd()
    {
        energy.Generate();
        animator.SetTrigger("tutorial_end");
        gameState = GAME_STATE.GAME;
        startFlag = true;
    }

    //�`���[�g���A��
    public bool GetStartFlag()
    {
        return startFlag;
    }

    //1��ڂ̃G�l���M�[��������
    void FirstEnergyGenerate()
    {
        energy.Generate();
    }

    //2��ڂ̃G�l���M�[��������
    void SecondEnergyGenerate()
    {
        
         energy.Generate();
    }

    #endregion

    #region �Q�[�����̃I�y���[�^�[�̃Z���t

    private void Game()
    {
        if (operetarTextFlag && onOperetarTextFlag == false)
        {
            //���^�{�X
            if (boss.BossType() == 1)
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                SummonBoss();
            }
            //���^�{�X
            if (boss.BossType() == 2)
            {
               operetarTextFlag = false; 
               onOperetarTextFlag = true;
               SummonMiniBoss();
            }
            //��^�{�X
            if (boss.BossType() == 3)
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                SummonBigBoss();
            }
            //�{�X�̍U���`���[�W
            if (boss.Charge())
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                BossAttackCharge();
            }
            //�{�X�ڋߎ�
            if (boss.Danger())
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                Approach();
            }
            //�{�X����
            if (boss.IsBossKill())
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                BossKill();
            }
        }

        if (!operetarTextFlag)
        {
            time += UnityEngine.Time.deltaTime;

            if (time >= OPERETAR_TEXT_COOLTIME)
            {
                operetarTextFlag = true;
                onOperetarTextFlag = false;
            }
        }
    }

    //�ʏ�{�X�o����
    public void SummonBoss()
    { 
        animator.SetTrigger("boss");  
    }

    //�~�j�{�X�o����
    public void SummonMiniBoss()
    {
        animator.SetTrigger("miniboss");
    }

    //�r�b�O�{�X�o����
    public void SummonBigBoss()
    {
        animator.SetTrigger("bigboss");  
    }

    //�{�X���j��
    public void BossKill()
    {
        animator.SetTrigger("kill");  
    }
    //�{�X�ڋߎ�
    public void Approach()
    {
        animator.SetTrigger("Approach");
    }

    //�{�X�̍U���̃`���[�W
    public void BossAttackCharge()
    {
        animator.SetTrigger("charge");
    }

    //�G�l���M�[�����o����
    public void EnergyCharge()
    {
        animator.SetTrigger("energycharge");
    }
}
#endregion