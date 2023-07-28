using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    public void PlayEndlessGame()
    {
        SceneManager.LoadScene("EndlessGame");
    }

    /*
    public void PlayClassicGame()
    {
        SceneManager.LoadScene("ClassicGame");
    }
    */


    /*
    public void SelectPlayer()
    {

    }
    */
    public void QuitGame()
    {
        Debug.Log("quit!");
        Application.Quit();
    }

}
