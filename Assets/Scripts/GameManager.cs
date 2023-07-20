using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Pacman;
    public GameObject[] Ghosts;
    //public GameObject Pellet;
    public Movement movement;
    public int score {get; private set;}
    public int lives {get; private set;}
    public Text startGameText;
    public Text gameOverText;
    public Text livesText;
    public Text scoreText;
    //private bool isKeyPressed = false;         //check if any key pressed
   
    // Start is called before the first frame update
    void Start()
    {     
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        //checkIfAnyKeyPressed();   //start game if player pressed any key
    }
    
    private void NewGame()
    {
        SetScore(0);
        Setlives(3); 
        //ResetState();
    }

    private void ResetState()
    {
        /*for (int i = 0; i < Ghosts.Length; i++) 
        {
            Ghosts[i].GetComponent<Ghost>().ResetState();
        }*/
        Pacman.GetComponent<Pacman>().ResetState();
    }

    private void GameOver()
    {
        gameOverText.GetComponent<Text>().enabled = true;
        Pacman.gameObject.SetActive(false);
        for(int i=0; i<4 ; i++)
            Ghosts[i].gameObject.SetActive(false);
        //Invoke(nameof(ResetState), 5f);
    }

    private void NewRound()
    {
        ResetState();
        NewGame();
    }

    /*
    private void checkIfAnyKeyPressed()
    {
        if(Input.anyKeyDown && !isKeyPressed)
        {   
            Debug.Log("KEY PRESSED!");
            Pacman.GetComponent<Pacman>().enabled = true;
            Pacman.GetComponent<Movement>().enabled =  true;

            for(int i=0; i<4; i++)
            {
                Ghosts[i].GetComponent<Movement>().enabled = true;
            }
            isKeyPressed = true;
        }
    }*/

    void SetScore(int score)   // set pacman's score
    {
        this.score = score;
        scoreText.text = this.score.ToString();
    }

    void Setlives(int lives)   // set pacman's lives
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    bool isPacmanDead()   // Is pacman dead?
    {   
        if(this.lives == 0)   // if pacman dead play death animation
            return true;
  
        else         // if pacman alive
            return false;
    }

    public void PacmanEaten()
    {
        Setlives(this.lives - 1);

        if(isPacmanDead())
        {
            Pacman.GetComponent<Pacman>().DisableGetIput(true);
            Pacman.GetComponent<Movement>().disableMovement(true);
            Pacman.GetComponent<Pacman>().playDeathAnimation();
            Invoke(nameof(GameOver), 5.0f);
        }
            
        else if(!isPacmanDead())
        {
            Pacman.GetComponent<Pacman>().DisableGetIput(true);
            Pacman.GetComponent<Movement>().disableMovement(true);
            //Pacman.GetComponent<Movement>().enabled = false;        //disable script
            //Pacman.GetComponent<Pacman>().enabled = false;         //disable script
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
        isActive = pellet.gameObject.GetComponent<Pellet>().isActive;
        SetActivePellet(pellet.gameObject, isActive, 10000);
    }

    public async void LargePelletEaten(LargePellet largePellet)     // if large pellet is eaten
    {
        bool isActive;
        int delay = 0;
        largePellet.gameObject.SetActive(false);
        SetScore(score + 50);
        
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
        SetActivePellet(largePellet.gameObject, isActive, delay);   
    }

    public void SetActivePellet(GameObject pellet, bool isActive, int delay)      //set active pellet again
    {
        if(pellet.tag == "Pellet")
            pellet.GetComponent<Pellet>().SetPelletState(isActive, delay);
        
        else if(pellet.tag == "Large Pellet")
            pellet.GetComponent<LargePellet>().SetPelletState(isActive, delay);
        
    }
}