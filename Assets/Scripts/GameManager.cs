using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public GameObject Pacman;
    public GameObject[] Ghosts;
    public GameObject Pellet;
    public Movement movement;
    public int score {get; private set;}
    public int lives {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void SetScore(int score)
    {
        this.score += score;
        Debug.Log(this.score);
    }

    void SetLive(int lives)
    {
        this.lives = lives;
    }

    bool isDead()
    {   
        if(this.lives == 0)
        {
            Pacman.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            Pacman.GetComponent<Animator>().SetTrigger("isDead");
            return true;
        }
        else
        {
            Pacman.GetComponent<Animator>().ResetTrigger("isDead");
            Pacman.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            return false;
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        bool isActive;
        pellet.gameObject.SetActive(false);
        SetScore(25);
        isActive = pellet.gameObject.GetComponent<Pellet>().isActive;
        SetActivePellet(pellet.gameObject, isActive, 10000);
    }

    public async void LargePelletEaten(LargePellet largePellet)
    {
        bool isActive;
        int delay = 0;
        largePellet.gameObject.SetActive(false);
        SetScore(50);
        
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

    public void SetActivePellet(GameObject pellet, bool isActive, int delay)
    {
        if(pellet.tag == "Pellet")
            pellet.GetComponent<Pellet>().SetPelletState(isActive, delay);
        
        else if(pellet.tag == "Large Pellet")
            pellet.GetComponent<LargePellet>().SetPelletState(isActive, delay);
        
    }
}
