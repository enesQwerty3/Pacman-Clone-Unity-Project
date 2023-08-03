using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    void Start() 
    {
        if(PlayerPrefs.HasKey("gameMode"))
            PlayerPrefs.DeleteKey("gameMode");    
    }
    public void PlayEndlessGame()
    {
        SceneManager.LoadScene("EndlessGame");
        PlayerPrefs.SetString("gameMode", "EndlessGame");
    }

    public void PlayClassicGame()
    {
        SceneManager.LoadScene("ClassicGame");
        PlayerPrefs.SetString("gameMode", "ClassicGame");
    }
    
    public void QuitGame()
    {
        Debug.Log("quit!");
        Application.Quit();
    }
}
