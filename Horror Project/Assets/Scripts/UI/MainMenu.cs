using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject creditsMenu;

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void CancelMain()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void PlaySound(AudioSource audio)
    {
        audio.Play();
    }
}
