using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioSource SFXSource;

    [Header("Sound Effects")]
    public AudioClip run;
    public AudioClip death;

    private AudioClip currentSFXClip;
    public static AudioClip jump;
    public static AudioClip hit;
    public static AudioClip fireball;
    public static AudioClip spawn;
    public static AudioClip explosion;
    public static AudioClip slash;
    public static AudioClip arrow;
    private static AudioSource audioSrc;

    public void Start()
    {
        jump = Resources.Load<AudioClip>("jump");
        hit = Resources.Load<AudioClip>("hit");
        fireball = Resources.Load<AudioClip>("fireball");
        spawn = Resources.Load<AudioClip>("spawn");
        explosion = Resources.Load<AudioClip>("explosion");
        slash = Resources.Load<AudioClip>("slash");
        arrow = Resources.Load<AudioClip>("arrow");
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource.isPlaying && currentSFXClip == clip)
        {
            return;
        }

        SFXSource.clip = clip;
        SFXSource.Play();
        currentSFXClip = clip;
    }

    public void StopSFX(AudioClip clip)
    {
        if (SFXSource.isPlaying && SFXSource.clip == clip)
        {
            SFXSource.Stop();
            currentSFXClip = null;
        }
    }

    public static void PlaySFX(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "hit":
                audioSrc.PlayOneShot(hit);
                break;
            case "fireball":
                audioSrc.PlayOneShot(fireball);
                break;
            case "spawn":
                audioSrc.PlayOneShot(spawn);
                break; 
            case "explosion":
                audioSrc.PlayOneShot(explosion);
                break;
            case "slash":
                audioSrc.PlayOneShot(slash);
                break;
            case "arrow":
                audioSrc.PlayOneShot(arrow);
                break;
        }
    }
}