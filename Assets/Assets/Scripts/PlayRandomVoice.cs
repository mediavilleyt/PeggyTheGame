using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomVoice : MonoBehaviour
{
    public AudioSource[] audioSources;

    public void PlayRandom()
    {
        int index = Random.Range(0, audioSources.Length);
        audioSources[index].Play();
    }
}
