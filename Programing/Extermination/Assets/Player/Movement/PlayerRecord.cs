using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerRecord : MonoBehaviour
{
    public UnityEngine.UI.Text uiText; // Явно указываем пространство имен UnityEngine.UI
    public UnityEngine.UI.Text uiGameOverText; // Явно указываем пространство имен UnityEngine.UI
    public float value = 0f;
    private string filePath = "Assets/Configs/engine_config.txt";

    // Update is called once per frame
    void Update()
    {
        // Чтение значения из файла
        float fileValue = ReadFromFile(filePath);

        // Если значение в файле меньше текущего значения рекорда, обновляем значение в файле
        if (value > fileValue)
        {
            WriteToFile(filePath, value);
        }

        // Обновление UI текста
        string valueText = value.ToString();
        uiText.text = valueText;
        uiGameOverText.text = valueText;
    }

    // Метод для чтения значения из файла
    private float ReadFromFile(string path)
    {
        float result = 0f;
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 0)
            {
                float.TryParse(lines[0], out result);
            }
        }
        return result;
    }

    // Метод для записи значения в файл
    private void WriteToFile(string path, float value)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(value.ToString());
        }
    }
}