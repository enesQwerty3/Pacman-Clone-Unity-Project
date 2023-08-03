using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageRight : MonoBehaviour
{
    //[SerializeField] private Vector2 connection_left = Vector2.zero;
    [SerializeField] private GameObject Connection_Left;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Pacman" || other.gameObject.tag == "Ghost")
        {
            //Debug.Log("Collision with pacman!");
            other.transform.position = Connection_Left.transform.position;
        }
    }
}
