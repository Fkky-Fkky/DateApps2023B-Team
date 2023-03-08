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
    #region オペレーターチュートリアル
    /// <summary>
    /// チュートリアルで呼ぶアニメーターのトリガー
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
    ///チュートリアルでの怪獣生成 1回目
    /// </summary>
    private void First()
    {
        csv.FirstBossGanaretar();
    }
    /// <summary>
    /// チュートリアルでの怪獣生成　2回目
    /// </summary>
    private void Second()
    {
        csv.SecondBossGanaretar();
    }
    /// <summary>
    /// チュートリアル終了
    /// </summary>
    void TutorialEnd()
    {
        animator.SetTrigger("tutorial_end");
        gameState = GAME_STATE.GAME;
        startFlag = true;
    }
    /// <summary>
    /// チュートリアル終了のフラグの受け渡し
    /// </summary>
    public bool GetStartFlag()
    {
        return startFlag;
    }
    /// <summary>
    /// チュートリアル中の1回目のエネルギー物資生成
    /// </summary>
    void FirstEnergyGenerate()
    {
        energy.GenerateEnergy();
    }
    /// <summary>
    /// チュートリアル中の2回目のエネルギー物資生成
    /// </summary>
    void SecondEnergyGenerate()
    {
         energy.GenerateEnergy();
    }
    #endregion

    #region ゲーム中のオペレーターのセリフ
    /// <summary>
    /// ゲーム中のオペレーターの動き
    /// </summary>
    private void Game()
    {
        if (operatorTextFlag)
        {
            BossType();

            BossMove();
        }
        //オペレーターのテキストクールタイム
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
    #region ボスの情報受け取り

    /// <summary>
    /// ボスの種類の情報受け取り
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
    /// ボスのビームチャージ、接近、討伐の情報受け取り
    /// </summary>
    private void BossMove()
    {
        //ボスの攻撃チャージ
        if (boss.Charge())
        {
            BossAttackCharge();
            operatorTextFlag = false;
        }
        //ボス接近時
        if (boss.Danger())
        {
            Approach();
            operatorTextFlag = false;
        }
        //ボス討伐
        if (boss.IsBossKill())
        {
            BossKill();
            operatorTextFlag = false;
        }
    }
    #endregion

    #region アニメーターのトリガー呼び出し
   
    /// <summary>
    ///通常ボス出現時のアニメーター呼び出し 
    /// </summary>
    public void SummonBoss()
    { 
        animator.SetTrigger("boss");  
    }
    /// <summary>
    ///ミニボス出現時のアニメーター呼び出し 
    /// </summary>
    public void SummonMiniBoss()
    {
        animator.SetTrigger("miniboss");
    }
    /// <summary>
    /// ビッグボス出現時のアニメーター呼び出し
    /// </summary>
    public void SummonBigBoss()
    {
        animator.SetTrigger("bigboss");  
    }
    /// <summary>
    ///ボス撃破時のアニメーター呼び出し
    /// </summary>
    public void BossKill()
    {
        animator.SetTrigger("kill");  
    }
    /// <summary>
    /// ボス接近時のアニメーター呼び出し
    /// </summary>
    public void Approach()
    {
        animator.SetTrigger("Approach");
    }
    /// <summary>
    /// ボスの攻撃のチャージのアニメーター呼び出し
    /// </summary>
    public void BossAttackCharge()
    {
        animator.SetTrigger("charge");
    }
    #endregion
}
#endregion