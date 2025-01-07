using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        // Check for active self to prevent error when object is destroyed
        if (gameObject.activeInHierarchy) 
            StartCoroutine(ExitTransition());    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction everytime the ghost hits a wall to create the
        // effect of the ghost bouncing around the home
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("wall"))  //wall layer name!!
            ghost.GhostMovement.SetDirection(-ghost.GhostMovement.direction);
        
    }

    private IEnumerator ExitTransition()
    {
        // Turn off movement while we manually animate the position
        ghost.GhostMovement.SetDirection(Vector2.up, true);
        ghost.GhostMovement.rb.isKinematic = true;
        ghost.GhostMovement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;

        // Animate to the starting point
        while (elapsed < duration)
        {
            gameObject.transform.position = (Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        // Animate exiting the ghost home
        while (elapsed < duration)
        {
            gameObject.transform.position = (Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Pick a random direction left or right and re-enable movement
        ghost.GhostMovement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        ghost.GhostMovement.rb.isKinematic = false;
        ghost.GhostMovement.enabled = true;
    }

}
