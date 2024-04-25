using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float value = 50f;
    public RectTransform valueRectTransform;
    private float _maxValue;

    private void Start()
    {
        _maxValue = value;
        DrawHealthBar();

        // Запускаем корутину, которая будет уменьшать значение каждую секунду
        //StartCoroutine(DecreaseHealthOverTime());
    }

    // Корутина для уменьшения значения каждую секунду
    private IEnumerator DecreaseHealthOverTime()
    {
        if (value > 0f)
        {
            // Бесконечный цикл, который будет выполняться, пока объект активен
            while (true)
            {
                // Уменьшаем значение на 1
                value -= 1;
                DrawHealthBar();

                // Проверяем, если значение стало меньше или равно 0, останавливаем корутину
                if (value <= 0f)
                {
                    yield break; // Прерываем выполнение корутины
                }

                // Ждем 1 секунду перед следующей итерацией
                yield return new WaitForSeconds(2f);
            }
        }
        
    }

    // Метод для отображения полоски здоровья
    private void DrawHealthBar()
    {
        valueRectTransform.anchorMax = new Vector2(1, value / _maxValue);
    }

    // Метод для нанесения урона
    public void DealDamage(float damage)
    {
        if (value > 0f)
        {
            value -= damage;
            
            if (value <= 0f)
            {
                // Обработка смерти игрока
            }
            DrawHealthBar();
        }
            
    }

    // Метод для восстановления здоровья
    public void AddHealth(float amount)
    {
        if (value < 100)
        {
            value += amount;
            value = Mathf.Clamp(value, 0, _maxValue);
            DrawHealthBar();
        }
    }
}
