using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pausing : MonoBehaviour
{
    private bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject startMenu;
    bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameStarted = startMenu.GetComponent<StartOfGame>().gameStarted;
            if (gamePaused)
            {
                StartGame();
            }
            else
            {
                StopGame();
            }
            if(gameStarted)
                SwitchCursorMode();
        }
    }

    void StopGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    void StartGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void SwitchCursorMode()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }




    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
