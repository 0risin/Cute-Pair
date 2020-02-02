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
    }
    public void playJumpSound()
    {
        source.clip = jump;
        source.Play();

        source.volume = .8f;
    }
    public void playPushSound()
    {
        source.clip = push;
        source.Play();

        source.volume = .9f;
    }
    public void playGrabSound()
    {
        source.clip = grab;
        source.Play();

        source.volume = .8f;
    }
    public void playThrowingSound()
    {
        source.clip = throwing;
        source.Play();

        source.volume = .7f;
    }
    public void playLandSound()
    {
        source.clip = land;
        source.Play();

        source.volume = .8f;
    }
    public void playStunSound()
    {
        source.clip = stun;
        source.Play();

        source.volume = .8f;
    }
}
