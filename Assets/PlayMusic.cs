using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{

    void Awake()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }
    }


    public AudioSource audioSource;

    private static bool isPlaying = false;
}
