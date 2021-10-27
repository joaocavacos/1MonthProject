using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Code : MonoBehaviour
{
    public GameObject codeCanvas;
    
    public string codeCombination;

    public TMP_InputField codeInputField;
    
    private Material codeCorrect;
    public GameObject codeObj;
    
    [SerializeField] private Text promptText;
    [SerializeField] private TMP_Text codeCheckText;


    void Start()
    {
        codeCorrect = codeObj.GetComponent<Renderer>().material;
        codeCorrect.color = Color.red;
    }

    public void SubmitCode()
    {
        if (codeCombination == codeInputField.text)
        {
            codeCorrect.color = Color.green;
            //play correct sound
            codeCanvas.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;//Code is correct
        }
        else
        {
            codeCheckText.text = "Try again"; //Code is wrong
            //play wrong sound
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
