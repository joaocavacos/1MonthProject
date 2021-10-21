using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject lightActivator;
    
    private Material activatorMaterial;

    [Range(1,2)]
    [SerializeField] private int boxesNeeded;
    
    [SerializeField] private float startMass;
    [SerializeField] private float currentMass;
    
    private void Start()
    {
        activatorMaterial = lightActivator.GetComponent<Renderer>().material;
        activatorMaterial.color = Color.red;
        startMass = gameObject.GetComponent<Rigidbody>().mass;
        currentMass = startMass;
    }

    private void OnTriggerEnter(Collider other) //When pressure plate has a box colliding
    {
        if (other.CompareTag("Box")) 
        {
            currentMass += 1;
            
            if (boxesNeeded == 1) //One box activation
            {
                activatorMaterial.color = Color.green;
            }else if (boxesNeeded == 2)
            {
                if(currentMass >= 3f) activatorMaterial.color = Color.green;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            activatorMaterial.color = Color.red;
            currentMass -= 1;
        }
    }
}
