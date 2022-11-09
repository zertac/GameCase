using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SfxHelper : MonoBehaviour
{
    // SFXHelper instance
    public static SfxHelper Instance;
    // Array of audio clips
    public List<AudioData<string, AudioClip>> AudioClips;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // play audio clip once
    public void Play(string key)
    {
        var clip = AudioClips.First(x => x.Key == key).Value;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
