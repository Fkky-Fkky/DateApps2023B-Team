using UnityEngine;

public class Operetar : MonoBehaviour
{
    Animator animator;

    enum GAME_STATE
    {
        TUTORIAL,

        GAME,
    }

    GAME_STATE gameState = GAME_STATE.TUTORIAL;

    [SerializeField] private Op_text optext;

    [SerializeField] private EnergyGenerator energy;

    [SerializeField] private CannonManager cannon;

    [SerializeField] private BossCSVGenerator csv;

    [SerializeField] private BossManager boss;

    private bool startFlag = false;

    private bool operetarTextFlag = false;

    private bool onOperetarTextFlag = false;

    private float time = 0;

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
            time += UnityEngine.Time.deltaTime;

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
        energy.TutorialEnd();
        animator.SetTrigger("tutorial_end");
        gameState = GAME_STATE.GAME;
        startFlag = true;
    }

    //�`���[�g���A��
    public bool Getstartflag()
    {
        return startFlag;
    }

    //1��ڂ̃G�l���M�[��������
    void FirstEnergyGenerate()
    {
        energy.FirstGenerate();
    }

    //2��ڂ̃G�l���M�[��������
    void SecondEnergyGenerate()
    {
        
         energy.SecondGenerate();
    }

    #endregion

    #region �Q�[�����̃I�y�q�̃Z���t

    void Game()
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
                BossAttckCharge();
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

            if (time >= 2)
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
    public void BossAttckCharge()
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