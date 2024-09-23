using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Slider volumeSlider; // Ссылка на ползунок для управления громкостью музыки
    public AudioSource musicSource; // Ссылка на AudioSource с музыкой

    void Start()
    {
        // Устанавливаем начальное значение ползунка громкости на основе текущей громкости музыки
        if (musicSource != null)
        {
            volumeSlider.value = musicSource.volume;
        }

        // Добавляем обработчик события изменения значения ползунка
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float volume)
    {
        // Изменяем громкость музыки на значение, выбранное пользователем на ползунке
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
}
