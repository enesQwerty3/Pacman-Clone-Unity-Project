using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2 spawnPosition = new Vector2(0f, -9.5f);
    [Range(0f, 15f)]  
    [SerializeField] private float speed = 10f;
    public float speedMultiplier = 1f;     //pellet speed boost multiplier
    private Vector2 initialDirection = Vector2.right;
    public Vector2 direction {get ; private set;}
    private Vector2 nextDirection = Vector2.zero;
    [SerializeField] private LayerMask wallLayerMask;
    Rigidbody2D rb;
    private bool isMovementDisabled;
    
    // Start is called before the first frame update
    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        transform.position = spawnPosition;
        direction = initialDirection;
        isMovementDisabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {                               //Try to move in the next direction while it's
        QueueNextDirection();       //queued to make ss more responsive  
                               
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {             
        if(!isMovementDisabled)                                                   //Move pacman's rigidbody to a position continuously    
            rb.MovePosition(rb.position + direction * speed * speedMultiplier * Time.deltaTime);   
    }

    public void SetDirection(Vector2 direction)
    {
        if(DetectCollision(direction))      //Check if there is a collider
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

    public void disableMovement(bool state)
    {
        if(state)
            isMovementDisabled = true;
        else
            isMovementDisabled = false;
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = spawnPosition;
        rb.isKinematic = false;
        disableMovement(false);
        enabled = true;        //enable script
    }
}
