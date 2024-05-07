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
        TogglePause();
        if (!isPaused) // Захватываем мышь только если игра не на паузе
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

        // Устанавливаем время игры в зависимости от того, находится ли игра на паузе
        Time.timeScale = isPaused ? 0f : originalTimeScale;

        // Включаем или выключаем Canvas меню паузы
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(isPaused && SceneManager.GetActiveScene().buildIndex != 0);
        }

        // Выключаем другой Canvas, если он задан
        if (canvasToDisable != null)
        {
            canvasToDisable.SetActive(!isPaused);
        }

        // Захватываем/освобождаем мышь в зависимости от состояния паузы
        if (isPaused)
        {
            UnlockCursor();
        }
        else
        {
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
