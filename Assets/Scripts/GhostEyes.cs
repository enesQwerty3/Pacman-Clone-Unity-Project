using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public SpriteRenderer spriteRenderer {get; private set;}
    private GhostMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<GhostMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.direction == Vector2.up)
            spriteRenderer.sprite = up;

        else if(movement.direction == Vector2.down)
            spriteRenderer.sprite = down;

        else if(movement.direction == Vector2.left)
            spriteRenderer.sprite = left;

        else if(movement.direction == Vector2.right)
            spriteRenderer.sprite = right; 
    }
}
