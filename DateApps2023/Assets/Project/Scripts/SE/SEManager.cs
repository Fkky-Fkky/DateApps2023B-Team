using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SEのマネージャークラス
/// </summary>
public class SEManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip playerPunchHit = null;

    [SerializeField]
    private AudioClip playerStan = null;

    [SerializeField]
    private AudioClip playerAttack = null;

    [SerializeField]
    private AudioClip bossBeamCharge = null;

    [SerializeField]
    private AudioClip bossBeam = null;

    [SerializeField]
    private AudioClip bossFinalAttack = null;

    [SerializeField]
    private AudioClip cannonConnect = null;

    [SerializeField]
    private AudioClip energyCharge = null;

    [SerializeField]
    private List<AudioClip> cannonBeams = new List<AudioClip>();

    public AudioClip PlayerPunchHitSe { get { return playerPunchHit; } }
    public AudioClip PlayerStanSe { get { return playerStan; } }
    public AudioClip PlayerAttackSe { get { return playerAttack; } }
    public AudioClip BossBeamCharge { get { return bossBeamCharge; } }
    public AudioClip BossBeamSe { get { return bossBeam; } }
    public AudioClip BossFinalAttack { get { return bossFinalAttack; } }
    public AudioClip CannonConnectSe { get { return cannonConnect; } }
    public AudioClip EnergyChargeSe { get { return energyCharge;} }

    /// <summary>
    /// 大砲のビームSEを返す
    /// </summary>
    /// <param name="beamType">ビームの種類</param>
    /// <returns>指定されたビームのAudioClip</returns>
    public AudioClip GetCannonBeamSe(int beamType)
    {
        return cannonBeams[beamType];
    }
}
