using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //method to load the next scene in the build settings i.e. the game
    public void PlayGame ()
    {
        //increments the build index of the scene to load the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //method that quits the game
    public void QuitGame()
    {
        //this does not work in unity, only in built games
        Application.Quit();
    }

}
