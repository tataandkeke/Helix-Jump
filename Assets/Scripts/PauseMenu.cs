using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public void Pause()
    {

        PauseMenuUI.SetActive(true);    //this is to activate the PauseMenu that is attached to the public gameobject
        Time.timeScale = 0f;            //this is to control the speed of which time passes, set to 0f completely freezes the game
        GameIsPaused = true;           //sets the gameispaused to true to prevent looping
    }
    
    public void Resume()
    {
        Debug.Log("Pressed");
        PauseMenuUI.SetActive(false);  //this deactivates the PauseMenu when its done   
        Time.timeScale = 1f;           //this sets the game time speed to normal speed
        GameIsPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;               //this is to set the time back to normal
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
