using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Code : MonoBehaviour
{
    public GameObject codeCanvas;
    
    public string codeCombination;

    public TMP_InputField codeInputField;
    public GameObject fence;
    public AudioSource openSound, wrongSound;
    
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private TMP_Text codeCheckText;

    public void SubmitCode()
    {
        if (codeCombination == codeInputField.text)
        {
            fence.SetActive(false);
            openSound.Play();
            codeCanvas.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;//Code is correct
        }
        else
        {
            codeCheckText.text = "Try again"; //Code is wrong
            wrongSound.Play();
            codeCheckText.color = Color.red;
        }
    }

    public void CloseSubmit()
    {
        codeCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptText.text = "Insert code (E)";
            if (Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                codeCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptText.text = "";
        }
    }
}
