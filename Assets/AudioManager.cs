using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip run, jump, land, push, grab, throwing,stun;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void playRunSound()
    {
        source.clip = run;
        source.Play();
        source.volume = 1f;
        Debug.Log("Playing: " + source.clip.name);  
    }
    public void playJumpSound()
    {
        source.clip = jump;
        source.Play();

        source.volume = .8f;
        Debug.Log("Playing: " + source.clip.name);
    }
    public void playPushSound()
    {
        source.clip = push;
        source.Play();

        source.volume = .9f;
        Debug.Log("Playing: " + source.clip.name);
    }
    public void playGrabSound()
    {
        source.clip = grab;
        source.Play();

        source.volume = .8f;
        Debug.Log("Playing: " + source.clip.name);
    }
    public void playThrowingSound()
    {
        source.clip = throwing;
        source.Play();

        source.volume = .7f;
        Debug.Log("Playing: " + source.clip.name);
    }
    public void playLandSound()
    {
        source.clip = land;
        source.Play();

        source.volume = .8f;
        Debug.Log("Playing: " + source.clip.name);
    }
    public void playStunSound()
    {
        source.clip = stun;
        source.Play();

        source.volume = .8f;
        Debug.Log("Playing: " + source.clip.name);
    }
}
