using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{
    private string _selectedScene;
    void Start() 
    {
        if(PlayerPrefs.HasKey("gameMode"))
            PlayerPrefs.DeleteKey("gameMode");    
    }

    public void SelectScene(Text selectedScene)
    {
        _selectedScene = selectedScene.text.ToString();
        Debug.Log("_selectedScene: " + _selectedScene);
    }

    public void Play()
    {
        SceneManager.LoadScene(_selectedScene);
        PlayerPrefs.SetString("gameMode", _selectedScene);
    }
    
    public void QuitGame()
    {
        Debug.Log("quit!");
        Application.Quit();
    }
}
