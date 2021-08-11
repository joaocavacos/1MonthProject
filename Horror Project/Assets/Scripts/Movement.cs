using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    //Stamina
    public Slider staminaSlider;
    
    public float currentStamina;
    public float staminaLoss;
    private float staminaRecoveryTime = 0;
    private float maxStamina = 100;
    
    private bool isWalking;
    private bool isExhausted;
    
    //Basic movement
    public Transform groundCheck;

    public float walkSpeed;
    public float runSpeed;
    public float gravity = -9.81f;
    public float groundDist;
    public float jumpHeight;

    public LayerMask groundMask;

    Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    void Update()
    {
        CheckGrounded();
        Move();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z; //Movement is relative to where you're looking

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isWalking = true;

            if (currentStamina > 0 && currentStamina <= 100)
            {
                controller.Move(movement * (runSpeed * Time.deltaTime)); //Run;
                currentStamina -= staminaLoss * Time.deltaTime; //loses stamina
            }
            else if (currentStamina <= 0) 
            {
                controller.Move(movement * (walkSpeed * Time.deltaTime));
            }
            
            staminaSlider.value = currentStamina;
        }
        else
        {
            isWalking = false;

            if (currentStamina < 100)
            {
                currentStamina += staminaLoss / 2 * Time.deltaTime; //Recovers half of the speed rate of the stamina loss
            }
            
            controller.Move(movement * (walkSpeed * Time.deltaTime)); //Walk
            staminaSlider.value = currentStamina;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); //Velocity for drops
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask); //Raycast to check if grounded

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

}