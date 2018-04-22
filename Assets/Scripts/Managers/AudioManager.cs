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
        Death_00
    }

    void Start()
    {
        // DontDestroyOnLoad(gameObject);
        // if (FindObjectsOfType(GetType()).Length > 1)
        // {
        //     Destroy(gameObject);
        // }
        // PlaySound(AudioType.Main, true);
        // PlaySound(AudioType.Loop, true);
        GameController.OnBallShot += PlayWhispSound;
        GameController.OnEndGame += PlayDeath00;
        GameController.OnStartGame += PlayParirTime;
    }

    private void PlaySound(AudioType audioType, bool loop = false)
    {
        if (!_audios[(int)audioType].audioClips[0].isPlaying)
        {
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

    // private void PlayDoor()
    // {
    //     PlaySound(AudioType.Door);
    // }


    [SerializeField]
    private AudioStruct[] _audios;

}