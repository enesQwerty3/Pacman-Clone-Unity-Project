using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GhostMovement GhostMovement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }   
    public GhostFrightened frightened { get; private set; }
    public GhostBehavior initialBehavior;
    public Transform pacman;
    public int points = 200;

    private void Awake()
    {
        GhostMovement = GetComponent<GhostMovement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            /*if (frightened.enabled)
                GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().GhostEaten(this);
                //FindObjectOfType<GameManager>().GhostEaten(this);
                
            else 
                GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().PacmanEaten();
                //FindObjectOfType<GameManager>().PacmanEaten();*/
                GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().PacmanEaten();
        }
    }
    
    public void ResetState()
    {
        gameObject.SetActive(true);
        GhostMovement.ResetState();
        /*
        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        if (home != initialBehavior) {
            home.Disable();
        }
        
        if (initialBehavior != null) {
            initialBehavior.Enable();
        }*/
    }
}
