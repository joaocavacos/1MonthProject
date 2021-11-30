using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{   
    public GameObject gameOver;

    public void GameOverActivate(){
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RetryorQuit(string sceneName){
        SceneManager.LoadSceneAsync(sceneName);
    }
}
