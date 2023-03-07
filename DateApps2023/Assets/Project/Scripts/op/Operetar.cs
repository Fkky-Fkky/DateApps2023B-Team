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
        #region オペレーターチュートリアル
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
    //怪獣生成
    private void First()
    {
        csv.FirstBossGanaretar();
    }
    //2回目の怪獣生成
    private void Second()
    {
        csv.SecondBossGanaretar();
    }
    //チュートリアル終了のフラグ
    void TutorialEnd()
    {
        energy.Generate();
        animator.SetTrigger("tutorial_end");
        gameState = GAME_STATE.GAME;
        startFlag = true;
    }

    //チュートリアル
    public bool GetStartFlag()
    {
        return startFlag;
    }

    //1回目のエネルギー物資生成
    void FirstEnergyGenerate()
    {
        energy.Generate();
    }

    //2回目のエネルギー物資生成
    void SecondEnergyGenerate()
    {
        
         energy.Generate();
    }

    #endregion

    #region ゲーム中のオペレーターのセリフ

    private void Game()
    {
        if (operetarTextFlag && onOperetarTextFlag == false)
        {
            //中型ボス
            if (boss.BossType() == 1)
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                SummonBoss();
            }
            //小型ボス
            if (boss.BossType() == 2)
            {
               operetarTextFlag = false; 
               onOperetarTextFlag = true;
               SummonMiniBoss();
            }
            //大型ボス
            if (boss.BossType() == 3)
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                SummonBigBoss();
            }
            //ボスの攻撃チャージ
            if (boss.Charge())
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                BossAttackCharge();
            }
            //ボス接近時
            if (boss.Danger())
            {
                operetarTextFlag = false;
                onOperetarTextFlag = true;
                Approach();
            }
            //ボス討伐
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

    //通常ボス出現時
    public void SummonBoss()
    { 
        animator.SetTrigger("boss");  
    }

    //ミニボス出現時
    public void SummonMiniBoss()
    {
        animator.SetTrigger("miniboss");
    }

    //ビッグボス出現時
    public void SummonBigBoss()
    {
        animator.SetTrigger("bigboss");  
    }

    //ボス撃破時
    public void BossKill()
    {
        animator.SetTrigger("kill");  
    }
    //ボス接近時
    public void Approach()
    {
        animator.SetTrigger("Approach");
    }

    //ボスの攻撃のチャージ
    public void BossAttackCharge()
    {
        animator.SetTrigger("charge");
    }

    //エネルギー物資出現時
    public void EnergyCharge()
    {
        animator.SetTrigger("energycharge");
    }
}
#endregion