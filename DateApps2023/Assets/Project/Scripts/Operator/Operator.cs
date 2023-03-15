//担当者:丸子羚
using UnityEngine;

/// <summary>
/// オペレーターの管理
/// </summary>
public class Operator : MonoBehaviour
{
    [SerializeField] 
    private EnergyGenerator energy = null;

    [SerializeField] 
    private CannonManager cannon = null;

    [SerializeField]
    private BossCSVGenerator csv = null;

    [SerializeField] 
    private BossManager boss = null;

    /// <summary>
    /// チュートリアルとゲームの状態のステート
    /// </summary>
    private enum GAME_STATE
    {
        TUTORIAL,

        GAME,
    }

    GAME_STATE gameState = GAME_STATE.TUTORIAL;

    private Animator animator = null;

    private bool isGameStart = false;

    private bool isOperatorTextCooltime = false;

    private  float coolTime = 0;
    
    private const float OPERATOR_TEXT_COOLTIME = 2;

    private const float RESET = 0;

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
        isGameStart = true;
    }
    /// <summary>
    /// チュートリアル終了のフラグの受け渡し
    /// </summary>
    public bool GetStartFlag()
    {
        return isGameStart;
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

    /// <summary>
    /// ゲーム中のオペレーターの動き
    /// </summary>
    private void Game()
    {
        if (isOperatorTextCooltime)
        {
            BossType();

            BossMove();
        } 

        if (!isOperatorTextCooltime)
        {
            coolTime += Time.deltaTime;
        }

        if (coolTime >= OPERATOR_TEXT_COOLTIME)
        {
            isOperatorTextCooltime = true;
            coolTime = RESET;
        }
    }

    /// <summary>
    /// ボスの種類の情報受け取り
    /// </summary>
    private void BossType()
    {
        switch (boss.BossType())
        {
            case 1:
                SummonBoss();
                isOperatorTextCooltime = false;
                break;

            case 2:
                SummonMiniBoss();
                isOperatorTextCooltime = false;
                break;

            case 3:
                SummonBigBoss();
                isOperatorTextCooltime = false;
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
            isOperatorTextCooltime = false;
        }
        //ボス接近時
        if (boss.Danger())
        {
            Approach();
            isOperatorTextCooltime = false;
        }
        //ボス討伐
        if (boss.IsBossKill())
        {
            BossKill();
            isOperatorTextCooltime = false;
        }
    }


   
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
}