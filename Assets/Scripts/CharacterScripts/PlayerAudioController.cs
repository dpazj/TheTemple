using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource source;

    public AudioClip gem;
    public AudioClip gemShard;
    public AudioClip jump;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playJump()
    {
        source.volume = 0.1f;
        playSound(jump);
    }
    public void playGem()
    {
        source.volume = 0.1f;
        playSound(gem);
    }
    public void playGemShard()
    {
        source.volume = 0.025f;
        playSound(gemShard);
    }

    private void playSound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }


}