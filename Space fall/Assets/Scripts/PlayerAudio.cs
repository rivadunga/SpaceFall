using UnityEngine;
using System.Collections;

/**
 * Handle the audio of the player
 */

public class PlayerAudio : MonoBehaviour
{
    public AudioClip audioMax;
    public AudioClip audioPoint;

    public void playMaxSpeedAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(audioMax, 0.5f);
    }

    public void playPointAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(audioPoint, 0.7f);
    }
}
