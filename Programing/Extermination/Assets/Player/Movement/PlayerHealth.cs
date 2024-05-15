using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource deathMusic;
    public AudioSource playMusic;

    public float PIZDA = 6f;
    public float value = 50f;
    public RectTransform valueRectTransform;
    public bool pause = false;
    public GameObject gameplayUI;
    public GameObject gameOverScreen;

    public GameObject gun1;
    public GameObject gun2;

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
        while (true)
        {
            // Уменьшаем значение
            value -= PIZDA;
            DrawHealthBar();

            // Проверяем, если значение стало меньше или равно 0, вызываем метод PlayerIsDead
            if (value <= 0f)
            {
                PlayerIsDead();
                yield break; // Прерываем выполнение корутины
            }

            // Ждем 2 секунды перед следующей итерацией
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
            Debug.Log("Player health after damage: " + value);

            if (value <= 0f)
            {
                // Обработка смерти игрока
                Debug.Log("Player died.");
                PlayerIsDead();
            }
        }

        DrawHealthBar();
    }

    public void PlayerIsDead()
    {
        PauseGame();
        pause = true;

        gameplayUI.SetActive(false);
        gameOverScreen.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Inventar>().enabled = false;
        gun2.SetActive(false);
        gun1.SetActive(false);
    }

    // Метод для восстановления здоровья
    public void AddHealth(float amount)
    {
        if (value < 100)
        {
            value += 10;
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

        deathMusic.Stop();
        playMusic.Play();
    }
}
