using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public Sound[] sounds;
    public Sound bgm;

    private void Awake()
    {
        if (null == instance)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        Play("StoryBGM");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

        if (name == "GamePlayBGM" || name == "StoryBGM")
        {
            bgm = s;
            s.source.loop = true;
        }
    }

    public void StopBGM()
    {
        bgm.source.Stop();
    }

    public void PauseBGM()
    {
        if (bgm.source.volume == 0.1f)
        {
            bgm.source.volume = 0.5f;
        }
        else
        {
            bgm.source.volume = 0.1f;
        }
    }
}
