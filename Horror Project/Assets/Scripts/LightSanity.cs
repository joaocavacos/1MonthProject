using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSanity : MonoBehaviour
{

    private SanitySystem _sanitySystem;
    
    void Start()
    {
        _sanitySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<SanitySystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _sanitySystem.DecreaseSanity(0.5f);
        }
    }
}
