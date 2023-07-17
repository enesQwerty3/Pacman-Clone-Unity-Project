using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Movement movement {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        this.movement = GetComponent<Movement>();    
    }

    // Update is called once per frame
    void Update()           
    {
        GetInputKey();
    }

    void GetInputKey()      // get direction input from player and change pacman's direction by SetDirection()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            movement.SetDirection(Vector2.up);
        
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            movement.SetDirection(Vector2.down);
        
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            movement.SetDirection(Vector2.left);
        
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            movement.SetDirection(Vector2.right);

        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);     //calculate angle before change the direction
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); //change pacman's rotation according to direction changes
    }
}
