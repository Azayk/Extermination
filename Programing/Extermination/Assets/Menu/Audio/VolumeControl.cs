using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public Slider SliderAudio;

    private const float _asd = 20f;

    private void Awake()
    {
        SliderAudio.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void OnEnable()
    {
        float savedVolume = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(SliderAudio.value) * _asd);
        SliderAudio.value = Mathf.Pow(10f, savedVolume / _asd);
        mixer.SetFloat(volumeParameter, savedVolume);

        ApplyVolumeToAllAudioSources(savedVolume);
    }

    private void HandleSliderValueChanged(float value)
    {
        float volumeValue = Mathf.Log10(value) * _asd;
        mixer.SetFloat(volumeParameter, volumeValue);
        PlayerPrefs.SetFloat(volumeParameter, volumeValue);

        ApplyVolumeToAllAudioSources(volumeValue);
    }

    private void ApplyVolumeToAllAudioSources(float volumeValue)
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = Mathf.Clamp01(Mathf.Pow(10f, volumeValue / _asd));
        }
    }
}