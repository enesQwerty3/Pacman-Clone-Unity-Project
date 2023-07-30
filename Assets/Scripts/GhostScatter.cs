using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehavior
{   
    //public LayerMask nodeLayerMask;
    private Node ClosestNode;
    private void OnDisable()  //when scatter disabled enable chase behaviour
    {
        Debug.Log("Scatter Disabled!");   
        if(!ghost.frightened.enabled)
            ghost.chase.Enable();          //
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Node" && enabled)
        {
            Node node = other.gameObject.GetComponent<Node>();
            int index = Random.Range(0, node.avalibleDirections.Count);      //choose a random direction index from avaliable directions list
            int randomIndex = 0;
            Debug.Log("Random Number: " + index);
            if(node.avalibleDirections[index] == -ghost.GhostMovement.direction && node.avalibleDirections.Count > 2)
            {
                if(index + 1 < node.avalibleDirections.Count)
                    ghost.GhostMovement.SetDirection(node.avalibleDirections[index + 1]);
                
                else
                {
                    randomIndex = Random.Range(0,2);
                    if(randomIndex == 0)
                        ghost.GhostMovement.SetDirection(node.avalibleDirections[index - 1]);
                
                    else if(randomIndex == 1)
                        ghost.GhostMovement.SetDirection(node.avalibleDirections[index - 2]);
                    
                    
                    else if(node.avalibleDirections.Count == 4)
                    {
                        randomIndex = Random.Range(0,3);
                        if(randomIndex == 0)
                            ghost.GhostMovement.SetDirection(node.avalibleDirections[index - 1]);
                
                        else if(randomIndex == 1)
                            ghost.GhostMovement.SetDirection(node.avalibleDirections[index - 2]);
                        
                        else
                            ghost.GhostMovement.SetDirection(node.avalibleDirections[index - 3]);
                    }
                }  
            }
            
            else if(node.avalibleDirections[index] == -ghost.GhostMovement.direction && node.avalibleDirections.Count <= 2)
            {
                if(index + 1 < node.avalibleDirections.Count)
                ghost.GhostMovement.SetDirection(node.avalibleDirections[1]);
        
                else
                ghost.GhostMovement.SetDirection(node.avalibleDirections[0]);
            }

            else
                ghost.GhostMovement.SetDirection(node.avalibleDirections[index]);
        }
    }

    /*private void FixedUpdate()
    {
        //checkNodesInCircle();

    }*/

    /*public void checkNodesInCircle()     //chase
    {
        Collider2D[] collidersInCircle = Physics2D.OverlapCircleAll(ghost.transform.position, 2f, nodeLayerMask);
        float minDistance = float.MaxValue;
        float distanceBetween = 0f;

        foreach(Collider2D colliders in collidersInCircle)
        {
            Debug.Log("Collider name: " + colliders.name);
            distanceBetween = (ghost.transform.position - colliders.transform.position).sqrMagnitude;
            if(distanceBetween < minDistance)
            {
                minDistance = distanceBetween;
                ClosestNode = colliders.GetComponent<Node>();
            }
        }
        int index = Random.Range(0, ClosestNode.avalibleDirections.Count);
        int randomIndex;
        
        if(ClosestNode.avalibleDirections[index] == -ghost.GhostMovement.direction && ClosestNode.avalibleDirections.Count > 2)
        {
            if(index + 1 < ClosestNode.avalibleDirections.Count)
                ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[index + 1]);
            
            else
            {
                randomIndex = Random.Range(0,2);
                if(randomIndex == 0)
                    ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[index - 1]);
            
                else if(randomIndex == 1)
                    ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[index - 2]);
                
                
                else if(ClosestNode.avalibleDirections.Count == 4)
                {
                    randomIndex = Random.Range(0,3);
                    if(randomIndex == 0)
                        ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[index - 1]);
            
                    else if(randomIndex == 1)
                        ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[index - 2]);
                    
                    else
                        ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[index - 3]);
                }
            }  
        }

        else if(ClosestNode.avalibleDirections[index] == -ghost.GhostMovement.direction && ClosestNode.avalibleDirections.Count <= 2)
        {
            if(index + 1 < ClosestNode.avalibleDirections.Count)
            ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[1]);
        
            else
            ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[0]);
        }

        else
            ghost.GhostMovement.SetDirection(ClosestNode.avalibleDirections[index]);
    }*/
}


