using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    [SerializeField] private Transform target;
    private Vector2 direction = Vector2.zero;

    private void OnDisable() 
    {
        ghost.scatter.Enable();    
    }

    /*private void OnEnable()  // !!
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
}
