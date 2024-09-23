using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 600f;
    public float xRotation = 0f;

    private bool isMouseLookEnabled = true; // Флаг, чтобы определить, включено ли управление мышью

    void Start()
    {
        // Заблокировать курсор
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", mouseSensitivity);
    }

    void Update()
    {
        if (isMouseLookEnabled)
        {
            // Получаем ввод от мыши и поворачиваем игрока
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.parent.Rotate(Vector3.up * mouseX);
        }
    }
    public void ToggleMouseLook(bool isEnabled)
    {
        enabled = isEnabled;
    }
}
