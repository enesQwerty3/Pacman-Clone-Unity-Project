using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Pacman;
    public Ghost[] Ghosts;
    public Movement movement;
    public int score {get; private set;}
    public int lives {get; private set;}
    public Text readyText;
    public Text gameOverText;
    public Text livesText;
    public Text scoreText;
    public Text highscoreText;
    public int highscore;
    public Transform pellets;
    private int pelletCount = 0;
    private string gameMode;
   
    // Start is called before the first frame update
    void Start()
    {   
        gameMode = PlayerPrefs.GetString("gameMode");  
        NewGame();
    }

    // Update is called once per frame
    /*
    void Update()
    {

    }
    */
    private void NewGame()
    {
        SetScore(0);
        Setlives(3);
        SetHighscore(); 
        if(gameMode == "Classic")
            CheckIfAllPelletsEaten(true);
        readyText.enabled = true;
        Invoke(nameof(ResetState), 3f);
    }

    private void ResetState()
    {
        for (int i = 0; i < Ghosts.Length; i++) 
        {
            Ghosts[i].ResetState();
        }
        Pacman.GetComponent<Pacman>().ResetState();
        readyText.enabled = false;
    }

    private void GameOver()
    {
        gameOverText.GetComponent<Text>().enabled = true;
        Pacman.gameObject.SetActive(false);
        for(int i=0; i<4 ; i++)
            Ghosts[i].gameObject.SetActive(false);
        Invoke("ReturnToMenu" ,3f);
    }

    private void NewRound()
    {
        ResetState();
        NewGame();
    }

    void SetScore(int _score)   // set pacman's score
    {
        score = _score;
        if(score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);    //set player score to highscore PlayerPrefs
            SetHighscore();         //change highscore text in game
        }    
        scoreText.text = score.ToString();
    }

    public void SetHighscore()
    {
        //PlayerPrefs.GetString("playerName");
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    void Setlives(int lives)   // set pacman's lives
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    bool IsPacmanDead()   // Is pacman dead?
    {   
        if(this.lives == 0)   // if pacman dead play death animation
            return true;
  
        else         // if pacman alive
            return false;
    }

    public void PacmanEaten()
    {
        Setlives(this.lives - 1);
        if(IsPacmanDead())
        {
            Pacman.GetComponent<Pacman>().DisableGetIput(true);
            Pacman.GetComponent<Movement>().disableMovement(true);
            for(int i = 0; i < 4; i++)
                Ghosts[i].GhostMovement.disableGhostMovement(true);
            Pacman.GetComponent<Pacman>().playDeathAnimation();
            Invoke(nameof(GameOver), 5.0f);
        }
        else if(!IsPacmanDead())
        {
            Pacman.GetComponent<Pacman>().DisableGetIput(true);
            Pacman.GetComponent<Movement>().disableMovement(true);
            for(int i = 0; i < 4; i++)
                Ghosts[i].GhostMovement.disableGhostMovement(true);
            Invoke(nameof(ResetState), 3f);
        }         
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points;
        SetScore(this.score + points);
    }

    public void PelletEaten(Pellet pellet)      // if pellet is eaten
    {
        bool isActive;
        pellet.gameObject.SetActive(false);
        SetScore(this.score + 25);
        if(gameMode == "Classic")
            CheckIfAllPelletsEaten();
        isActive = pellet.gameObject.GetComponent<Pellet>().isActive;
        if(gameMode == "Endless")
            SetActivePellet(pellet.gameObject, isActive, 10000);
    }

    public async void LargePelletEaten(LargePellet largePellet)     // if large pellet is eaten
    {
        for(int i = 0; i < 4; i++)        //activate ghost frightened behaviour for all ghosts
        {
            Ghosts[i].frightened.Enable(largePellet.duration);
        } 

        bool isActive;
        int delay = 0;
        largePellet.gameObject.SetActive(false);
        SetScore(score + 50);
        if(gameMode == "Classic")
            CheckIfAllPelletsEaten();
        if(movement.speedMultiplier != 1.25f)
        {
            movement.speedMultiplier = 1.25f;
            await Task.Delay(8000);
            Debug.Log("movement speed multiplier delay has been expired!");
            movement.speedMultiplier = 1f;
            delay = 2000;

        }

        else
            delay = 10000;
            
        isActive = largePellet.gameObject.GetComponent<LargePellet>().isActive;
        if(gameMode == "Endless")
            SetActivePellet(largePellet.gameObject, isActive, delay);   
    }

    public void SetActivePellet(GameObject pellet, bool isActive, int delay)      //set active pellet again
    {
        if(pellet.tag == "Pellet")
            pellet.GetComponent<Pellet>().SetPelletState(isActive, delay);
        
        else if(pellet.tag == "Large Pellet")
            pellet.GetComponent<LargePellet>().SetPelletState(isActive, delay);
    }

    public bool CheckIfAllPelletsEaten(bool countPellets = false)
    {
        if(pelletCount == 0 && countPellets)
        {
            foreach(Transform pellet in pellets)
            {
                if(pellet.gameObject.activeInHierarchy)
                    pelletCount +=1;
            }
            return false;
        }

        else
        {
            pelletCount -=1;
            if(pelletCount == 0)
            {
                GameOver();
                return true;
            }
            return false;
        }
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}