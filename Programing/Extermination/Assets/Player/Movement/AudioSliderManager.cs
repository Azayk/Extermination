using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioSliderManager : MonoBehaviour
{
    public Slider audioSlider;

    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        // Находим все звуковые источники на сцене
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        // Добавляем каждый звуковой источник в список
        foreach (AudioSource source in allAudioSources)
        {
            audioSources.Add(source);
        }

        // Обновляем слайдер
        UpdateSlider();
    }

    void Update()
    {
        FixUpdate();
    }

    void FixUpdate()
    {
        // Проверяем, добавились ли новые звуковые источники
        AudioSource[] currentAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in currentAudioSources)
        {
            if (!audioSources.Contains(source))
            {
                audioSources.Add(source);
                UpdateSlider();
            }
        }

        // Проверяем, удалились ли звуковые источники
        List<AudioSource> sourcesToRemove = new List<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source == null)
            {
                sourcesToRemove.Add(source);
                UpdateSlider();
            }
        }

        // Удаляем звуковые источники, которые больше не существуют
        foreach (AudioSource source in sourcesToRemove)
        {
            audioSources.Remove(source);
        }
    }

    // Обновляем слайдер аудио
    void UpdateSlider()
    {
        audioSlider.minValue = 0;
        audioSlider.maxValue = audioSources.Count - 1;
        audioSlider.wholeNumbers = true;
        audioSlider.value = 0;
        audioSlider.onValueChanged.AddListener(delegate { ChangeAudioVolume(); });
    }

    // Метод изменения громкости аудио
    void ChangeAudioVolume()
    {
        int index = Mathf.RoundToInt(audioSlider.value);
        if (index >= 0 && index < audioSources.Count)
        {
            // Изменяем громкость выбранного аудио
            audioSources[index].volume = audioSlider.value;
        }
    }
}