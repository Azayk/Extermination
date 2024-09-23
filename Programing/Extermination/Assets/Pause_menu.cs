using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{
    public GameObject pauseMenuCanvas; // Ссылка на Canvas для меню паузы
    public GameObject canvasToDisable; // Ссылка на Canvas, который нужно отключить при вызове паузы
    public Button resumeButton; // Кнопка для возобновления игры
    public Button mainMenuButton; // Кнопка для возвращения в главное меню

    private static Pause_menu instance;
    private bool isPaused = false;
    private float originalTimeScale;
    private bool isCursorLocked = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Объект не будет уничтожен при загрузке новой сцены
        }
        else
        {
            Destroy(gameObject); // Уничтожаем объект, если он уже существует в другой сцене
        }
    }

    void Start()
    {
        // Деактивируем Canvas при старте игры
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
        originalTimeScale = Time.timeScale;

        // Назначаем функции кнопок
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(MainMenu);
        }
    }

    void Update()
    {
        // Проверяем нажатие клавиши Escape для вызова/отзыва паузы
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void ResumeGame()
    {
        if (isPaused) // Проверяем, находится ли игра на паузе
        {
            TogglePause(); // Если да, то сначала отключаем паузу
        }

        // Затем, если игра не на паузе, захватываем мышь
        if (!isPaused)
        {
            LockCursor();
        }
    }

    public void MainMenu()
    {
        Time.timeScale = originalTimeScale; // Восстанавливаем исходное время игры перед переходом на другую сцену
        SceneManager.LoadScene(0);
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Если входим в режим паузы, сохраняем текущее значение времени и устанавливаем его в 0
            originalTimeScale = Time.timeScale;
            Time.timeScale = 0f;

            // Включаем Canvas меню паузы
            if (pauseMenuCanvas != null)
            {
                pauseMenuCanvas.SetActive(true);
            }

            // Отключаем другой Canvas, если он задан
            if (canvasToDisable != null)
            {
                canvasToDisable.SetActive(false);
            }

            // Освобождаем мышь
            UnlockCursor();
        }
        else
        {
            // Если выходим из режима паузы, возвращаем время к исходному значению
            Time.timeScale = originalTimeScale;

            // Выключаем Canvas меню паузы
            if (pauseMenuCanvas != null)
            {
                pauseMenuCanvas.SetActive(false);
            }

            // Включаем другой Canvas, если он задан
            if (canvasToDisable != null)
            {
                canvasToDisable.SetActive(true);
            }

            // Захватываем мышь
            LockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        isCursorLocked = !isPaused;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }
}
