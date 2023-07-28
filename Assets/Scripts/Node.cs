using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask wallLayerMask;
    public List<Vector2> avalibleDirections {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        avalibleDirections = new List<Vector2>();
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }
    
    private void CheckAvailableDirection(Vector2 direction)
    {
        //
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, wallLayerMask);
        if(hit.collider == null)
            avalibleDirections.Add(direction);
    }
}
