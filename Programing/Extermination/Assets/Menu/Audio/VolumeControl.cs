using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public Slider SliderAudio;

    private float _volumeValue;
    private const float _asd = 20f;

    private void Awake()
    {
        SliderAudio.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        _volumeValue = Mathf.Log10(value) * _asd;
        mixer.SetFloat(volumeParameter, _volumeValue);
    }

    // Start is called before the first frame update
    void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(SliderAudio.value) * _asd);
        SliderAudio.value = Mathf.Pow(10f, _volumeValue / _asd);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, _volumeValue);
    }
}
