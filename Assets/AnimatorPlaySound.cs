using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlaySound : MonoBehaviour
{
    [SerializeField]
    AudioManager audioManager;

    private void Start()
    {
        audioManager = transform.parent.GetComponent<AudioManager>();
    }

    void playRun()
    {
        audioManager.playRunSound();
    }
}
