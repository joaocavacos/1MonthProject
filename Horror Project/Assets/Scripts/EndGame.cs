using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    //use dialog to end the game
    //when reaching grave click to "dig grave (E)" and start the dialog
    //after the dialog go to the main menu to finish the game.

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float typeSpeed;
    private int index;
    public GameObject continueButton;
    public TextMeshProUGUI continueText;
    public GameObject dialog;
    public GameObject player;
    public AudioSource typingSound, nextSound;
    public TMP_Text promptText;

    //Final Jumpscare

    public AudioSource jumpscareSound;
    public GameObject scareCamera;
    private Quaternion currentRotation;
    private GameObject playerObj;
    
    private void Start() {
        playerObj = player.GetComponent<GameObject>();
    }
    
    private void Update() {
        if(textDisplay.text == sentences[index]){
            continueButton.SetActive(true);
            typingSound.Stop();
        }
        Debug.Log(index + "/" + (sentences.Length - 1));

        if(playerObj != null){
            promptText.text = "Dig grave and end it (E)";

            if(Input.GetKeyDown(KeyCode.E)){
                Debug.Log("Clicked E");
                dialog.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                StartCoroutine(Type());
            }
        }
       
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
            StartCoroutine(finalJumpscare());
        }

        if(index == sentences.Length - 1){
            continueText.text = "LOOK";
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && playerObj == null){
            playerObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") && playerObj != null && playerObj == other.gameObject){

            playerObj = null;
            promptText.text = "";
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private IEnumerator finalJumpscare()
    {
        dialog.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        player.transform.Rotate(-20f, 100f, 0f);
        yield return new WaitForSeconds(.5f);
        jumpscareSound.Play();
        scareCamera.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("MainMenu");
    }
}
