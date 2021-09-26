using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{

    public GameObject lightObj;
    private GameObject battery;

    public float energyLost;
    private float maxEnergy = 100f;
    private float currentEnergy;
    private float usedEnergy;

    private bool flashEnable;

    private int batteries;
    private GameObject batteryObj;

    //public AudioSource flashlightClick;
    //public AudioSource batteryPick;


    void Start()
    {
        currentEnergy = maxEnergy;
        maxEnergy = 100 * batteries;
    }

    void Update()
    {
        maxEnergy = 100 * batteries;
        currentEnergy = maxEnergy;
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashEnable =! flashEnable;
        }

        if (flashEnable)
        {
            lightObj.SetActive(true);

            if (currentEnergy <= 0)
            {
                lightObj.SetActive(false);
                batteries = 0;
            }
            else
            {
                currentEnergy -= energyLost * Time.deltaTime;
                usedEnergy += energyLost * Time.deltaTime;
            }

            if (usedEnergy >= 100)
            {
                batteries -= 1;
                usedEnergy = 0;
            }
            
        }
        else
        {
            lightObj.SetActive(false);
        }
        
        Debug.Log("Batteries: " + batteries);
        Debug.Log("Used Energy: " + usedEnergy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            batteryObj = other.gameObject;
            batteries += 1;
            
            Destroy(batteryObj);
        }
    }
}
