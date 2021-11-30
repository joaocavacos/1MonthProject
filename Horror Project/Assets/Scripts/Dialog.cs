using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float typeSpeed;
    private int index;
    public GameObject continueButton;
    public GameObject dialog;
    public GameObject player;
    public AudioSource typingSound, nextSound;
    
    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(Type());
    }

    private void Update() {
        if(textDisplay.text == sentences[index]){
            continueButton.SetActive(true);
            typingSound.Stop();
        }
        Debug.Log(index + "/" + (sentences.Length - 1));
    }

    private IEnumerator Type()
    {
        typingSound.Play();
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void NextSentence()
    {
        nextSound.Play();
        continueButton.SetActive(false);

        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            //End of sentences
            textDisplay.text = "";
            dialog.SetActive(false);
            player.SetActive(true);
        }
    }
}
