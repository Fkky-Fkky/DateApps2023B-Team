using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SE�̃}�l�[�W���[�N���X
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
    /// ��C�̃r�[��SE��Ԃ�
    /// </summary>
    /// <param name="beamType">�r�[���̎��</param>
    /// <returns>�w�肳�ꂽ�r�[����AudioClip</returns>
    public AudioClip GetCannonBeamSe(int beamType)
    {
        return cannonBeams[beamType];
    }
}
