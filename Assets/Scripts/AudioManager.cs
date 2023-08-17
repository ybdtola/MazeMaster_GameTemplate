using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake() 
    { 
    
        if(instance == null){
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme");
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null) return;
        s.source.Play();
    }
}
