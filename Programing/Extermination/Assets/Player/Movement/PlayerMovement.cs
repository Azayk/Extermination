﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float cooldownTime = 5f; // Время задержки между двумя нажатиями

    private bool canMove = true;

    public float speed = 12f;
    public float ultraspeed = 60f;

    public float gravity = -30f;
    public float ultragravity = 300f;
    public float jumpHeight = 3f;
    public float utrajumpHeight = 13f;
    public float ulta = 300;

    public Transform groundCheck;
    public float groundDictance = 0.4f;
    public LayerMask groundMask;
    public LayerMask deadZoneMask;

    [Header("Keybinds")]
    public KeyCode ultrajump;

    Vector3 velocity;

    bool isGrounded;
    bool isDeadZone;
    public PlayerHealth _playerHealth;

    // Update is called once per frame
    void Update()
    {
        //CameraShake();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDictance, groundMask);
        isDeadZone = Physics.CheckSphere(groundCheck.position, groundDictance, deadZoneMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded)
        {
            gravity = -60;
            CameraShaker.Instance.ShakeOnce(.2f, .1f, .1f, 1f);

        }

        if (isDeadZone)
        {
            _playerHealth.PlayerIsDead();
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt((jumpHeight + utrajumpHeight) * -2f * gravity);
        }

        if (Input.GetKey(ultrajump) && isGrounded)
        {
            controller.Move(move * ultraspeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            gravity = ultragravity;

        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void FixUpdate()
    {
        CameraShake();
    }

    private void CameraShake()
    {
        CameraShaker.Instance.ShakeOnce(.2f, 1f, .1f, 1f);
    }

}
