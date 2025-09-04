using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleContorller : MonoBehaviour
{
    [SerializeField] private AudioClip titleBgm;
    [SerializeField] private float fadeSeconds = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        if(AudioManager.Instance && titleBgm)
        {
            AudioManager.Instance.FadeTo(titleBgm, fadeSeconds);
            AudioManager.Instance.SetMusicVolume(0.2f);
        }
        
    }

}
