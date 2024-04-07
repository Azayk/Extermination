using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float utrajumpHeight = 13f;

    bool dabljumpchek = false;
    public Transform groundCheck;
    public float groundDictance = 0.4f;
    public LayerMask groundMask;

    [Header("Keybinds")]
    public KeyCode ultrajump;

    public float ultragravity;

    Vector3 velocity;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDictance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(isGrounded)
        {
            gravity = -30;
            dabljumpchek = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        else if (Input.GetKey(ultrajump) && isGrounded)
        {
            dabljumpchek = true;
            velocity.y = Mathf.Sqrt((jumpHeight + utrajumpHeight) * -2f * gravity);     
        }

        if(Input.GetKeyDown(ultrajump) && dabljumpchek && !isGrounded)
        {
            gravity = ultragravity;
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
