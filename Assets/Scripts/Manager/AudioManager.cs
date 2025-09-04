using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer mixer;                
    public AudioMixerGroup musicGroup;

    public float musicVolume = 0.15f;

    private AudioSource first;
    private AudioSource second;
    private AudioSource active;
    private AudioSource idle;

    private Coroutine fadeCo;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        first = gameObject.AddComponent<AudioSource>();
        second = gameObject.AddComponent<AudioSource>();

        foreach (var music in new[] { first, second })
        {
            music.loop = true;
            music.playOnAwake = false;
            music.outputAudioMixerGroup = musicGroup;
        }

        active = first;
        idle = second;
        SetMusicVolume(0.2f);

    }

 

    public void FadeTo(AudioClip clip, float fade)
    {
        if (clip == null) return;
        if (active.clip == clip && active.isPlaying) return;

        if(fadeCo != null) { StopCoroutine(fadeCo); fadeCo = null; }
        if (active.clip == null || fade <= 0f)
        {
            active.clip = clip;
            active.loop = true;
            active.volume = musicVolume;
            active.Play();

            idle.Stop();
            idle.clip = null;
            idle.volume = 0f;
            return;


        }

        fadeCo = StartCoroutine(CoCrossfade(clip, fade));
    }

    IEnumerator CoCrossfade(AudioClip nextClip, float fade)
    {
        idle.clip = nextClip;
        idle.volume = 0f;
        idle.loop = true;
        idle.Play();

        float t = 0f;
        float startVol = active.volume;
        while (t < fade)
        {
            t += Time.deltaTime;
            float k = t / fade;
            active.volume = Mathf.Lerp(startVol, 0f, k);
            idle.volume = Mathf.Lerp(0f, musicVolume, k);
            yield return null;
        }

        active.Stop();
        AudioSource temp = active;
        active = idle;
        idle = temp;
        idle.volume = 0f;
        idle.clip = null;
        fadeCo = null;
    }

    public void SetMusicVolume(float vol01)
    {
        musicVolume = Mathf.Clamp01(vol01);
        if(active != null && active.isPlaying)
        {
            active.volume = musicVolume;
        }
        if(idle != null && idle.isPlaying && fadeCo == null)
        {
            idle.volume = musicVolume;
        }
    }

}
