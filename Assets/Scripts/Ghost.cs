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
    public int points = 100;
    //public Animator animator;

    private Vector2[] Points = new Vector2[4];
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
        //ResetState();   
    }

    // Update is called once per frame
    /*
    void Update()
    {
        Points[0] = new Vector2(gameObject.transform.position.x - 2f, gameObject.transform.position.y);
        Points[1] = new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y);
        Points[2] = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2f);
        Points[3] = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 2f);
    }

    void OnDrawGizmosSelected()
    {
        // Draws four lines making a square
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(Points[0].x, Points[0].y, 0), new Vector3(Points[1].x, Points[1].y, 0));
        Gizmos.DrawLine(new Vector3(Points[2].x, Points[2].y, 0), new Vector3(Points[3].x, Points[3].y, 0));
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (frightened.enabled)    //
                GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().GhostEaten(this);

                
            else //
                GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().PacmanEaten();
        }
    }
    
    public void ResetState()
    {
        gameObject.SetActive(true);
        GhostMovement.ResetState();
        frightened.Disable();    //
        chase.Disable();
        scatter.Enable();
        // /*
        if (home != initialBehavior)  
            home.Disable();           //*/
        if (initialBehavior != null)
            initialBehavior.Enable();
    }
}
