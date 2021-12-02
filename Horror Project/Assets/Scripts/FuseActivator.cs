using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuseActivator : MonoBehaviour
{
    private FuseCollecter fuseCollecter;

    private int requiredFuses = 3;

    public GameObject fusesActiveObj;
    public GameObject lightsObj;
    public TMP_Text fusesText;
    
    bool fusesActive;
    
    void Start()
    {
        fuseCollecter = GameObject.FindGameObjectWithTag("Player").GetComponent<FuseCollecter>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fuseCollecter.currentFuses >= requiredFuses)
            {
                fusesText.text = "Insert all fuses (E)";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    fusesActiveObj.SetActive(true);
                    fuseCollecter.currentFuses = 0;
                    fusesActive = true;
                    lightsObj.SetActive(true);
                    //Power on
                }
            }
            else if(fuseCollecter.currentFuses < requiredFuses && fusesActive == false)
            {
                fusesText.text = "You need to collect more fuses...";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fusesText.text = "";
        }
    }
}
