using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMixer : MonoBehaviour
{
    [SerializeField] AudioClip backgroundClip;
    [SerializeField] [Range(0,1)] float backgroundVolume=0.5f;
    [SerializeField] AudioClip[] clips;
    [SerializeField] [Range(0,1)] float clipsVolume=0.7f;
    AudioSource backgroundSource;
    AudioSource source;
    private void Start()
    {
        backgroundSource=gameObject.AddComponent<AudioSource>();
        source=gameObject.AddComponent<AudioSource>();
        
        source.volume=clipsVolume;

        backgroundSource.volume=backgroundVolume;
        backgroundSource.clip=backgroundClip;
        backgroundSource.loop=true;
        backgroundSource.Play();
    }
    public void PlayClipForce(int index){
        source.clip=clips[index];
        source.Play();
    }
    public void PlayClip(int index){
        if(source.isPlaying){
            return;
        }
        source.clip=clips[index];
        source.Play();
    }
}
