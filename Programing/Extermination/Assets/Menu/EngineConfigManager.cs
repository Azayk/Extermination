using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EngineConfigManager : MonoBehaviour
{
    public UnityEngine.UI.Text fileValueText; // Явно указываем пространство имен UnityEngine.UI
    public UnityEngine.UI.Button resetButton; // Явно указываем пространство имен UnityEngine.UI
    private string filePath = "Assets/Configs/engine_config.txt";

    void Start()
    {
        // При запуске игры считываем значение из файла и отображаем его на экране
        DisplayFileValue();

        // Подписываемся на событие нажатия кнопки
        resetButton.onClick.AddListener(ResetRecord);
    }

    // Метод для отображения значения из файла
    void DisplayFileValue()
    {
        if (File.Exists(filePath))
        {
            string fileContent = File.ReadAllText(filePath);
            fileValueText.text = fileContent;
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
        }
    }

    // Метод для сброса значения в файле
    void ResetRecord()
    {
        // Пишем значение 0 в файл
        WriteToFile(filePath, "0");

        // После сброса перезагружаем отображаемое значение
        DisplayFileValue();
    }

    // Метод для записи значения в файл
    void WriteToFile(string path, string value)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(value);
        }
    }
}
