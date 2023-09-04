using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOfGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startMenu;
    public bool gameStarted = false;
    void Start()
    {
        startMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startMenu.SetActive(false);
    }
}
