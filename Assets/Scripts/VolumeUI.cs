using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (slider != null)
        {
            slider.value = AudioManager.Instance.musicVolume;

            slider.onValueChanged.AddListener(SetVolume);       // 슬라이더.온밸루채인지드 : 슬라이더 값이 변할때 자동으로 호출되는 이벤트
        }
    }

    private void SetVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    } 
}
