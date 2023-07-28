using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public float speedMultiplier = 1f;
    private Vector2 GhostInitialDirection = Vector2.left;
    public Vector2 direction {get ; private set;}
    private Vector2 nextDirection = Vector2.zero;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private Vector2 startingPosition;
    public Rigidbody2D rb;
    private bool disableMovement = false;
    
    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        direction = GhostInitialDirection;
    }

    // Update is called once per frame
    void Update()
    {
        QueueNextDirection();        //queue ghost's next direction
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    private void FixedUpdate()
    {
        //enable and disable movement
        if(!disableMovement)
            rb.MovePosition(rb.position + direction * speed * speedMultiplier * Time.deltaTime);
    }
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if(forced || DetectCollision(direction))      //Check if there is a collider
            nextDirection = direction;
        
        else                               //Check if there is no collider
        {
            this.direction = direction; 
            nextDirection = Vector2.zero;
        }
    }

    private void QueueNextDirection()
    {
        if (nextDirection != Vector2.zero) 
            SetDirection(nextDirection);
    }

    bool DetectCollision(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0, direction, 1.5f, wallLayerMask);
        if(hit.collider != null)
            return true;
        
        else
            return false;
    }

    public void disableGhostMovement(bool state)
    {
        if(state)
            disableMovement = true;

        else
            disableMovement = false;
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = GhostInitialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rb.isKinematic = false;
        enabled = true;
        disableGhostMovement(false);
    }

}
