using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Static variable to track whether the game is currently paused
    public static bool GameIsPause = false;

    //UI elements for the pause menu and player UI
    public GameObject pauseMenuUI;
    public GameObject playerUI;

    //Name of the main menu scene to load
    public string mainMenuName;

    void Update()
    {
        //Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //If the game is currently paused, call Resume; otherwise, call Pause
            if (GameIsPause)
                Resume();
            else
                Pause();
        }
    }

    // Method to resume the game
    public void Resume()
    {
        //Deactivate the pause menu UI and activate the player UI
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);

        //Sets the game time scale to normal
        Time.timeScale = 1f;
        
        //Update the pause state
        GameIsPause = false;

        //lock the cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Method to pause the game
    void Pause()
    {
        //Activate the pause menu UI and deactivate the player UI
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);

        //Freeze the game by setting the time scale to 0
        Time.timeScale = 0f;

        //Update the pause state
        GameIsPause = true;

        //Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Method to load the main menu scene
    public void LoadMenu()
    {
        //Reset the pause state and time scale
        GameIsPause = false;
        Time.timeScale = 1f;

        // Load the specified main menu scene
        SceneManager.LoadScene(mainMenuName);
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
