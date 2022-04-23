using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;

    public AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        i = this;
    }

    // Update is called once per frame
    public void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
