using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTime : MonoBehaviour
{

    void Start()
    {
        Invoke("PlaySound", time);
    }

    void PlaySound()
    {
        source.Play();
    }


    [SerializeField]
    private float time;

    [SerializeField]
    private AudioSource source;
}
