// Written by Marc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    //on awake make the cursor visible
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //Restarts the game
    public void RestartGame()
    {
        SceneManager.LoadScene("GameLoop");
    }
    //Sets the game to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
