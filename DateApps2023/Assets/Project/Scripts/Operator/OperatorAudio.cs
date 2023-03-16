//�S����:�ێq�
using UnityEngine;

/// <summary>
/// �I�y���[�^�[�̃{�C�X�̃N���X
/// </summary>
public class OperatorAudio : MonoBehaviour
{
    [SerializeField] 
    AudioClip[] tutorialVoice = null;

    [SerializeField]
    AudioClip[] gameVoice = null;

    private AudioSource source = null;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponents<AudioSource>()[0];
    }
    
    /// <summary>
    ///�I�y���[�^�[�̃{�C�X�̍Đ����~�߂�֐� 
    /// </summary>
    void VoiceStop()
    {
        source.Stop();
    }

    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice1()
    {
        source.PlayOneShot(tutorialVoice[0]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice2()
    {
        source.PlayOneShot(tutorialVoice[1]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice3()
    {
        source.PlayOneShot(tutorialVoice[2]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice4()
    {
        source.PlayOneShot(tutorialVoice[3]);
    }

    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice5()
    {
        source.PlayOneShot(tutorialVoice[4]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice6()
    {
        source.PlayOneShot(tutorialVoice[5]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice7()
    {
        source.PlayOneShot(tutorialVoice[6]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice8()
    {
        source.PlayOneShot(tutorialVoice[7]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice9()
    {
        source.PlayOneShot(tutorialVoice[8]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice10()
    {
        source.PlayOneShot(tutorialVoice[9]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice11()
    {
        source.PlayOneShot(tutorialVoice[10]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice12()
    {
        source.PlayOneShot(tutorialVoice[11]);
    } /// <summary>
      /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
      /// </summary>
    void OpVice13()
    {
        source.PlayOneShot(tutorialVoice[12]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void OpVice14()
    {
        source.PlayOneShot(tutorialVoice[13]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void GameVice1()
    {
        source.PlayOneShot(gameVoice[0]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void GameVice2()
    {
        source.PlayOneShot(gameVoice[1]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void GameVice3()
    {
        source.PlayOneShot(gameVoice[2]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void GameVice4()
    {
        source.PlayOneShot(gameVoice[3]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void GameVice5()
    {
        source.PlayOneShot(gameVoice[4]);
    }
    /// <summary>
    /// �I�y���[�^�[�̃{�C�X�̃A�j���[�^�[�Ăяo���̃g���K�[
    /// </summary>
    void GameVice6()
    {
        source.PlayOneShot(gameVoice[5]);
    }
}
