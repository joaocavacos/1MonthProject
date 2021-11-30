using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KeyLabSystem : MonoBehaviour
{

    [SerializeField] private TMP_Text collectText;
    [SerializeField] private int currentKeys = 0;
    [SerializeField] private AudioSource keyCollect;
    public string sceneName;
    private GameObject keyObj;

    bool isInHouse = false;

    void Update()
    {
        //Check for number of keys and load new scene
        CheckKeyCount();

        //Collect key
        if (keyObj != null)
        {
            collectText.text = "Collect House Key (E)";

            if (Input.GetKeyDown(KeyCode.E))
            {
                keyCollect.Play();
                currentKeys++;
                collectText.text = "";
                Destroy(keyObj);
                keyObj = null;
            }
        }
    }

    public void CheckKeyCount()
    {
        if(isInHouse)
        {
            if(currentKeys <= 0)
            {
                collectText.text = "I need to find a key to get in...";
            }
            else
            {
                collectText.text = "Go inside (E)";

                if(Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadSceneAsync(sceneName); //enter house
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key") && keyObj == null)
        {
            keyObj = other.gameObject;
        }

        if(other.CompareTag("House"))
        {
            isInHouse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pill") && keyObj != null && keyObj == other.gameObject)
        {
            keyObj = null;
            collectText.text = "";
        }

        if(other.CompareTag("House"))
        {
            isInHouse = false;
            collectText.text = "";
        }
    }
}
