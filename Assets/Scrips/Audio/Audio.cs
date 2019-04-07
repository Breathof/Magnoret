using System;
using UnityEngine;
public class Audio: MonoBehaviour
{

    public AudioClip[] Sounds;
    public AudioSource Source;


    public Audio()
    {
    }

    public void PlaySound(int index)
    {
        Source.clip = Sounds[index];
        Source.Play();
    }
}
