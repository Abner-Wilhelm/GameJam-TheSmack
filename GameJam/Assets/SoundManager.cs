using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioClip explosion;
    public AudioClip stampSound;
    public AudioClip elevatorNoises;
    public AudioClip music;
    public AudioClip transitionSound;
    public AudioClip roomTitle;
}
