using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EasterEgg : MonoBehaviour
{

    public TextMeshProUGUI promptText;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            promptText.text = "HAHAHA you can't finish the game too early, face your fears!";
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            promptText.text = "";
        }
    }
}
