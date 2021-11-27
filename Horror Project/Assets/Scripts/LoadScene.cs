using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject != null) 
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
