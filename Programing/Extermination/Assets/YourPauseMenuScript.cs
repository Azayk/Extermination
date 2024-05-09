using UnityEngine;
using UnityEngine.EventSystems;

public class YourPauseMenuScript : MonoBehaviour
{
    void Start()
    {
        // Создание EventSystem в случае его отсутствия
        if (FindObjectOfType<EventSystem>() == null)
        {
            gameObject.AddComponent<EventSystem>();
        }
    }
}
