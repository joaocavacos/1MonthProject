using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Flashlight : MonoBehaviour
{

    #region Variables

    private GameObject batteryObj;
    
    [SerializeField] private Light flashLight;

    [SerializeField] private int maxBatteries = 3;
    [SerializeField] private int currentBatteries = 1;

    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float energyUsed;

    [SerializeField] private float energyConsumption = 0.5f;

    [SerializeField] private float maxIntensity;

    [SerializeField] private Text batteryText;
    [SerializeField] private Text pickupText;

    private bool flashEnabled;

    [SerializeField] private AudioSource toggleSound;
    [SerializeField] private AudioSource reloadSound;

    [SerializeField] private Image batteryVisual;
    [SerializeField] private Sprite[] batterySprites = new Sprite[6];

    #endregion


    void Start()
    {
        currentEnergy = maxEnergy;
        maxEnergy = 100 * currentBatteries;
    }

    void Update()
    {
        maxEnergy = 100 * currentBatteries;
        currentEnergy = maxEnergy;

        batteryText.text = $"{currentBatteries}/{maxBatteries}";
        
        UpdateBatteryImage();
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashEnabled =! flashEnabled;
            toggleSound.Play();
        }

        if (flashEnabled) ToggleOn();
        else ToggleOff();
        
        AddBatteries();
    }

    public void ToggleOn()
    {
        if (currentBatteries > 0)
        {
            flashLight.enabled = true;
                
            float intensityPercentage = currentEnergy / maxEnergy;
            float intensityFactor = maxIntensity * intensityPercentage; //Calculate the intensity decrease
            flashLight.intensity = intensityFactor;
            
            //Debug.Log("Intensity: " + flashLight.intensity);

            currentEnergy -= energyConsumption * Time.deltaTime;
            energyUsed += energyConsumption * Time.deltaTime;
        }
        else
        {
            flashLight.enabled = false;
            currentBatteries = 0;
        }

        if (energyUsed >= 100)
        {
            reloadSound.Play();
            currentBatteries -= 1;
            energyUsed = 0;
        }
    }

    public void ToggleOff() //When toggle off
    {
        flashLight.enabled = false;
    }

    public void AddBatteries() //Battery Picker and checker
    {
        if (batteryObj != null)
        {
            pickupText.text = "Pick battery (E)";
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentBatteries++;
                pickupText.text = "";
                Destroy(batteryObj);
                batteryObj = null;
            }
        }

        if (currentBatteries >= maxBatteries) currentBatteries = maxBatteries;
    }

    public void UpdateBatteryImage()
    {
        if (energyUsed >= 100) batteryVisual.sprite = batterySprites[5];
        if (energyUsed >= 80 && energyUsed <= 99) batteryVisual.sprite = batterySprites[4];
        if (energyUsed >= 60 && energyUsed <= 79) batteryVisual.sprite = batterySprites[3];
        if (energyUsed >= 40 && energyUsed <= 59) batteryVisual.sprite = batterySprites[2];
        if (energyUsed >= 20 && energyUsed <= 39) batteryVisual.sprite = batterySprites[1];
        if (energyUsed >= 1 && energyUsed <= 19) batteryVisual.sprite = batterySprites[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery") && batteryObj == null)
        {
            batteryObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Battery") && batteryObj != null && batteryObj == other.gameObject)
        {
            batteryObj = null;
            pickupText.text = "";
        }
    }
}
