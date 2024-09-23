using UnityEngine;
using UnityEngine.UI;

public class SliderValueToVariable : MonoBehaviour
{
    public Slider slider; // Ссылка на компонент слайдера

    private const string SensitivityKey = "MouseSensitivity"; // Ключ для сохранения значения чувствительности

    void Start()
    {
        // Загружаем сохраненное значение чувствительности из PlayerPrefs
        float savedSensitivity = PlayerPrefs.GetFloat(SensitivityKey, slider.value);

        // Устанавливаем начальное значение слайдера
        slider.value = savedSensitivity;
    }

    void Update()
    {
        // Сохраняем значение слайдера в PlayerPrefs
        PlayerPrefs.SetFloat(SensitivityKey, slider.value);
    }
}
