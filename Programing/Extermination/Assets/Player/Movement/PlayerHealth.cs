using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource deathMusic;
    public AudioSource playMusic;

    public float value = 50f;
    public RectTransform valueRectTransform;
    public bool pause = false;
    public GameObject gameplayUI;
    public GameObject gameOverScreen;

    private float _maxValue;

    private void Start()
    {
        ResumeGame();
        playMusic.Play();
        _maxValue = value;
        DrawHealthBar();

        // Запускаем корутину, которая будет уменьшать значение каждую секунду
        StartCoroutine(DecreaseHealthOverTime());
    }

    // Корутина для уменьшения значения каждую секунду
    private IEnumerator DecreaseHealthOverTime()
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
                PlayerIsDead();
                
                
            }

            DrawHealthBar();
        }
            
    }

    public void PlayerIsDead()
    {
        PauseGame();
        pause = true;

        gameplayUI.SetActive(false);
        gameOverScreen.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Inventar>().enabled = false;
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

    public void PauseGame()
    {
        Time.timeScale = 0f;

        playMusic.Stop();
        deathMusic.Play();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;


        // Возобновление игровых процессов и управления
        deathMusic.Stop();
        playMusic.Play();
    }
}
