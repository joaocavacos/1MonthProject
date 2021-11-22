using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PillSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text pillText;
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private int currentPills = 5;
    [SerializeField] private AudioSource collectSound;

    private SanitySystem _sanitySystem;
    private GameObject pillObj;


    void Start()
    {
        _sanitySystem = GetComponent<SanitySystem>();
    }

    void Update()
    {
        pillText.text = "Pills: " + currentPills;
        if (currentPills > 0)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _sanitySystem.TakePill();
                currentPills -= 1;
            }
        }
        else
        {
            Debug.Log("Not enough pills");
        }
        CollectPill();
    }

    public void CollectPill()
    {
        if (pillObj != null)
        {
            promptText.text = "Take pill (E)";

            if (Input.GetKeyDown(KeyCode.E))
            {
                collectSound.Play();
                currentPills++;
                promptText.text = "";
                Destroy(pillObj);
                pillObj = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pill") && pillObj == null)
        {
            pillObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pill") && pillObj != null && pillObj == other.gameObject)
        {
            pillObj = null;
            promptText.text = "";
        }
    }
}