using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundOnAwake : MonoBehaviour
{
    public  List<AudioClip> audioClips;
    private AudioSource thisAudioSource;
    // Start is called before the first frame update
    
    void Awake(){
        thisAudioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        AudioClip audioClip = audioClips[Random.Range(0, audioClips.Count)];
        //play random sound from the list
        thisAudioSource.PlayOneShot(audioClip);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
