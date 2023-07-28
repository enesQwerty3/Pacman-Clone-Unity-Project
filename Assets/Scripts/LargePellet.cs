using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class LargePellet : MonoBehaviour
{
    public bool isActive {get; private set;}
    public float duration = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Pacman")
        {
            //Debug.Log("Collision with pacman!");
            isActive = false;
            FindObjectOfType<GameManager>().LargePelletEaten(this);
        }
    }
    
    public void SetPelletState(bool isActive , int delay)
    {
        this.isActive = isActive;
        if (!this.isActive)
            SetActivePellet(delay);
    }
    private async void SetActivePellet(int delay)
    {
        await Task.Delay(delay);
        gameObject.SetActive(true);
        isActive = true;
    }
}
