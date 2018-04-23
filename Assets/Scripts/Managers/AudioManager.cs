using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [System.Serializable]
    public struct AudioStruct
    {
        public AudioType audioType;
        public AudioSource[] audioClips;
    }

    public enum AudioType
    {
        Default,
        Main,
        Gameover,
        Loop,
        Whisp00,
        Whisp01,
        Parir,
        Death_00,
        Step00,
        LavaBall,
        LavaDino,
        Fanfare,
        BallFall,
        Button,
        Bounce
    }

    void Start()
    {
        PlaySound(AudioType.Loop, true);
        GameController.OnBallShot += PlayWhispSound;
        GameController.OnEndGame += PlayDeath00;
        GameController.OnStartGame += PlayParirTime;
        GameController.OnPlayerRunning += PlayRunning;
        GameController.OnPlayerStop += StopRunning;
        GameController.OnPlayerWin += PlayFanfare;
        GameController.OnBallFall += PlayBallFall;
        GameController.OnBallLava += PlayBallLava;
        GameController.OnCharacterLava += PlayCharacterLava;
        GameController.OnButtonClicked += PlayButton;
        GameController.OnBallHit += PlayBounce;
    }

    private void PlaySound(AudioType audioType, bool loop = false, float pitch = 0.0f, float variation = 0.0f)
    {
        if (!_audios[(int)audioType].audioClips[0].isPlaying)
        {
            if (pitch != 0.0f)
            {
                _audios[(int)audioType].audioClips[0].pitch = Random.Range(pitch - variation, pitch + variation);
            }
            _audios[(int)audioType].audioClips[0].loop = loop;
            _audios[(int)audioType].audioClips[0].Play();
        }
    }

    private void PlayWhispSound()
    {
        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            Invoke("PlayWhisp00", 0.5f);
        }
        else
        {
            Invoke("PlayWhisp01", 0.5f);
        }
    }

    private void PlayRunning()
    {
        PlaySound(AudioType.Step00, false, 1.0f, 0.25f);
    }

    private void StopRunning()
    {

    }

    private void PlayDeath00()
    {
        PlaySound(AudioType.Death_00);
    }

    private void PlayWhisp00()
    {
        PlaySound(AudioType.Whisp00);
    }

    private void PlayWhisp01()
    {
        PlaySound(AudioType.Whisp01);
    }

    private void PlayParirTime()
    {
        Invoke("PlayParir", 1.0f);
    }

    private void PlayParir()
    {
        PlaySound(AudioType.Parir);
    }
    private void PlayLavaDino()
    {
        PlaySound(AudioType.LavaDino);
    }
    private void PlayLavaBall()
    {
        PlaySound(AudioType.LavaBall);
    }
    private void PlayFanfare()
    {
        PlaySound(AudioType.Fanfare);
    }

    private void PlayBallFall()
    {
        PlaySound(AudioType.BallFall);
    }

    private void PlayBallLava()
    {
        PlaySound(AudioType.LavaBall);
    }

    private void PlayCharacterLava()
    {
        PlaySound(AudioType.LavaDino);
    }

    private void PlayButton()
    {
        PlaySound(AudioType.Button);
    }

    private void PlayBounce()
    {
        PlaySound(AudioType.Bounce, false, 2.0f, 0.3f);
    }

    void OnDestroy()
    {
        GameController.OnBallShot -= PlayWhispSound;
        GameController.OnEndGame -= PlayDeath00;
        GameController.OnStartGame -= PlayParirTime;
        GameController.OnPlayerRunning -= PlayRunning;
        GameController.OnPlayerStop -= StopRunning;
        GameController.OnPlayerWin -= PlayFanfare;
        GameController.OnBallFall -= PlayBallFall;
        GameController.OnBallLava -= PlayBallLava;
        GameController.OnCharacterLava -= PlayCharacterLava;
        GameController.OnButtonClicked -= PlayButton;
        GameController.OnBallHit -= PlayBounce;
    }

    [SerializeField]
    private AudioStruct[] _audios;

}