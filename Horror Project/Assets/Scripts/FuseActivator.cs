using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuseActivator : MonoBehaviour
{
    private FuseCollecter fuseCollecter;

    private int requiredFuses = 3;

    public GameObject fusesActiveObj;
    public Text fusesText;
    
    void Start()
    {
        fuseCollecter = GameObject.FindGameObjectWithTag("Player").GetComponent<FuseCollecter>();
    }

    private void OnTriggerEnter(Collider other)
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
                    //Power on
                }
            }
            else
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
