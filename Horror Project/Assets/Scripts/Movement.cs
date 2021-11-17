using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Movement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Stamina")]
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private float currentStamina;
    [SerializeField] private float staminaLoss;
    
    private float maxStamina = 100;
    private bool isRunning;

    [Header("Movement properties")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDist;
    
    private Vector3 movement;
    private Vector3 velocity;

    [Header("Crouching")] 
    [SerializeField] private float crouchHeight = 0.75f;
    [SerializeField] private float standHeight = 1.25f;
    [SerializeField] private float crouchSpeed;
    public bool isCrouching;
    
    /*[Header("Jumping properties")]
    [SerializeField] private float jumpHeight;*/
    
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    [Header("Head Bobbing")] 
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform camTransform;
    [SerializeField] private float frequency = 5f;
    [SerializeField] private float horizontalAmplitude = 0.1f;
    [SerializeField] private float verticalAmplitude = 0.1f;
    [SerializeField, Range(0, 1)] private float headBobSmoothing = 0.1f;
    
    private bool isWalking;
    private float walkingTime;
    private Vector3 targetCamPos;
    
    [Header("UI")] 
    [SerializeField] private AudioSource walkSound;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        walkSound.Pause();
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    void Update()
    {
        CheckGrounded();
        Move();

        /*if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }*/

        if (Input.GetKey(KeyCode.C) && isGrounded)
        {
            Crouch();
        }
        else
        {
            controller.height = standHeight;
            isCrouching = false;
        }

        HeadBobMovement();
     
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movement = transform.right * x + transform.forward * z; //Movement is relative to where you're looking

        if (movement.magnitude > 0.1f)
        {
            isWalking = true;
            walkSound.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
            walkSound.volume = UnityEngine.Random.Range(0.4f, 0.6f);
            walkSound.UnPause();
        }
        else
        {
            isWalking = false;
            walkSound.Pause();
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            if (currentStamina > 0 && currentStamina <= 100)
            {
                walkSound.pitch = UnityEngine.Random.Range(1.1f, 1.4f);
                walkSound.volume = UnityEngine.Random.Range(0.4f, 0.6f);
                walkSound.UnPause();
                controller.Move(movement * (runSpeed * Time.deltaTime)); //Run;
                currentStamina -= staminaLoss * Time.deltaTime; //loses stamina
            }
            else if (currentStamina <= 0)
            {
                controller.Move(movement * walkSpeed * Time.deltaTime);
            }

            staminaSlider.value = currentStamina;
        }
        else
        {
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

    void Crouch() //needs fixing
    {
        isCrouching = true;
        controller.height = crouchHeight;
        controller.Move(movement * crouchSpeed * Time.deltaTime);
    }

    /*void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }*/

    void HeadBobMovement()
    {
        if (!isWalking) walkingTime = 0;
        else walkingTime += Time.deltaTime;
        
        //Calculate camera's target pos
        targetCamPos = headTransform.position + CalculateHeadBobOffset(walkingTime);
        
        //Interpolate position
        camTransform.position = Vector3.Lerp(camTransform.position, targetCamPos, headBobSmoothing);
        
        //Snap to position if close
        if ((camTransform.position - targetCamPos).magnitude <= 0.001f) camTransform.position = targetCamPos;
    }

    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontalOffset = 0;
        float verticalOffset = 0;
        Vector3 offset = Vector3.zero;

        if (t > 0)
        {
            //Calculate offsets
            horizontalOffset = Mathf.Cos(t * frequency) * horizontalAmplitude;
            verticalOffset = Mathf.Sin(t * frequency * 2) * verticalAmplitude;
            
            //Combine offsets and calculate camera target pos
            offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
        }

        return offset;
    }
}