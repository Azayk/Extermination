
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider sensitivitySlider; // Ссылка на слайдер с настройками чувствительности
    public MouseLook mouseLookScript; // Прямая ссылка на скрипт MouseLook

    private const string SensitivityKey = "MouseSensitivity"; // Ключ для сохранения значения чувствительности

    void Start()
    {
        // Проверяем, был ли назначен скрипт MouseLook
        if (mouseLookScript != null)
        {
            // Загружаем сохраненное значение чувствительности из PlayerPrefs
            float savedSensitivity = PlayerPrefs.GetFloat(SensitivityKey, mouseLookScript.mouseSensitivity);

            // Устанавливаем значение слайдера
            sensitivitySlider.value = savedSensitivity;

            // Устанавливаем чувствительность в скрипте MouseLook
            mouseLookScript.mouseSensitivity = savedSensitivity;

            // Добавляем обработчик события изменения значения слайдера
            sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
        }
        else
        {
            Debug.LogError("MouseLook script reference not set!");
        }
    }

    // Метод обновления чувствительности мыши
    void UpdateSensitivity(float newValue)
    {
        // Обновляем значение mouseSensitivity в скрипте MouseLook
        if (mouseLookScript != null)
        {
            mouseLookScript.mouseSensitivity = newValue;

            // Сохраняем новое значение в PlayerPrefs
            PlayerPrefs.SetFloat(SensitivityKey, newValue);
        }
    }
}