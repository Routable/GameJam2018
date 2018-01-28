using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTesting : MonoBehaviour {

    public List<AudioClip> clips;
    public AudioSource source;


    public void OnClick()
    {
        int clip = Random.Range(0, clips.Count);
        source.PlayOneShot(clips[clip]);
    }
}
