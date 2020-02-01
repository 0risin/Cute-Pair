using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlaySound : MonoBehaviour
{
    [SerializeField]
    AudioManager audioManager;

    void playRun()
    {
        audioManager.playRunSound();
    }
}
