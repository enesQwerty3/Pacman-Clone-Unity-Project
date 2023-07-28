using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    [SerializeField] private Transform target;
    private Vector2 direction = Vector2.zero;
    public bool eaten {get; private set;}

    public override void Enable(float duration)
    {
        base.Enable(duration);
        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;
        Invoke(nameof(Flash), duration / 2f);
    }
    public override void Disable()
    {
        base.Disable();
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
    private void Eaten()
    {
        eaten = true;
        gameObject.transform.position = ghost.home.inside.position;
        ghost.home.Enable(duration);
        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;   
            white.enabled = true;   
        }
    }

    private void OnEnable()
    {
        //ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        //ghost.movement.speedMultiplier = 1f;
        eaten = false;
    }


    /*private void OnDisable() 
    {
        ghost.scatter.Enable();    
    }

    private void OnEnable()  // !!
    {
        ghost.scatter.Disable();
        ghost.chase.Disable();    
    }*/

    private void OnTriggerEnter2D(Collider2D other) 
    {
       if(other.tag == "Node" && enabled)
       {
            Node node = other.GetComponent<Node>();
            float maxDistanceBetween = float.MinValue;

            foreach(Vector2 avaliableDirection in node.avalibleDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(avaliableDirection.x, avaliableDirection.y);
                float distanceBetween = (target.position - newPosition).sqrMagnitude;
                if(distanceBetween > maxDistanceBetween)
                {
                    maxDistanceBetween = distanceBetween;
                    direction = avaliableDirection;
                }
                ghost.GhostMovement.SetDirection(direction);
            }  
       }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pacman")
        {
            if (enabled) 
                Eaten();
        }
    }
}
